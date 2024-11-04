using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieWebSite.Server.Migrations;
using MovieWebSite.Server.Repository.IRepository;
using Server.Model.AuthModels;
using Server.Model.DTO;
using Server.Model.Models;
using System.Diagnostics;

namespace MovieWebSite.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserFilmController(IUnitOfWork unitOfWork, SignInManager<ApplicationUser> signInManager) : Controller
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        [HttpGet("getplaylist"), Authorize(Roles = "UserT2")]
        public async Task<IActionResult> GetUserPlayList()
        {
            try
            {
                var user = await _signInManager.UserManager.GetUserAsync(User);
                if (user == null)
                {
                    throw new Exception("💥Can't find user in get play list!");
                }
                var playListFilmId = _unitOfWork.UserFilmRepository.GetAll(ul => ul.UserId == user.Id && ul.AddPlaylistOn != null).Select(ul => ul.FilmId);
                var playListFilms = _unitOfWork.FilmRepository.GetAll(f=> playListFilmId.Contains(f.Id));
                var result = playListFilms.Select(f => new FilmDTO
                {
                    Id = f.Id,
                    Title = f.Title,
                    FilmPath = f.FilmPath,
                    BlurHash = f.BlurHash,
                    Synopsis = f.Synopsis,
                    Director = f.Director,
                    Type = f.Type
                }).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"💥Something has gone wrong! {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("filmscore/{filmId}")]
        public IActionResult GetFilmScore(int filmId)
        {
            var filmRatings =  _unitOfWork.UserFilmRepository.GetAll(uf=> uf.FilmId == filmId && uf.Rating != null).Select(uf => uf.Rating.Value);
            if (!filmRatings.Any())
            {
                return Ok(new { score = -1 });
            }
            double averageScore = filmRatings.Average();
            return Ok(new {score = averageScore });
        }

        [HttpPost, Authorize(Roles = "UserT2, UserT1")]
        public async Task<IActionResult> UserFilmLogic(UserFilmDTO userFilmDTO)
        {
            try
            {
                var user = await _signInManager.UserManager.GetUserAsync(User);
                if (user == null)
                {
                    throw new Exception("💥Can't find user in get user film!");
                }
                bool isPlayListAdded = false;
                var filmRating = userFilmDTO.FilmRatting;
                var existingUserFilm = _unitOfWork.UserFilmRepository.Get(ul => ul.UserId == user.Id && ul.FilmId == userFilmDTO.FilmId);
                if (existingUserFilm != null)
                {
                    if (userFilmDTO.IsRemoveFromPlayList)
                    {
                        existingUserFilm.AddPlaylistOn = null;
                        _unitOfWork.UserFilmRepository.Update(existingUserFilm);
                        _unitOfWork.Save();
                    }else if (userFilmDTO.IsAddPlayList)
                    {
                        existingUserFilm.AddPlaylistOn = DateTime.Now;
                        _unitOfWork.UserFilmRepository.Update(existingUserFilm);
                        _unitOfWork.Save();
                        isPlayListAdded = true;
                    }else if (existingUserFilm.AddPlaylistOn != null)
                    {
                        isPlayListAdded = true;
                    }
                    
                    if ( userFilmDTO.FilmRatting  > -1 && existingUserFilm.Rating != userFilmDTO.FilmRatting)
                    {
                        existingUserFilm.Rating = userFilmDTO.FilmRatting;
                        _unitOfWork.UserFilmRepository.Update(existingUserFilm);
                        _unitOfWork.Save();
                    }
                    else if (existingUserFilm.Rating != null)
                    {
                        filmRating = (int)existingUserFilm.Rating;
                    }

                    if (userFilmDTO.IsViewed)
                    {
                        existingUserFilm.ViewedOn = DateTime.Now;
                        _unitOfWork.UserFilmRepository.Update(existingUserFilm);
                        _unitOfWork.Save();
                    }

                    return Ok(new { PlayListAdded = isPlayListAdded, Rating = filmRating });
                }
                UserFilm userFilm = new UserFilm()
                {
                    UserId = user.Id,
                    FilmId = userFilmDTO.FilmId
                };
                if (userFilmDTO.IsAddPlayList)
                {
                    userFilm.AddPlaylistOn = DateTime.Now;
                    _unitOfWork.UserFilmRepository.Add(userFilm);
                    _unitOfWork.Save();
                }
                else if(userFilmDTO.FilmRatting > -1)
                {
                    userFilm.Rating = userFilmDTO.FilmRatting;
                    _unitOfWork.UserFilmRepository.Add(userFilm);
                    _unitOfWork.Save();
                }
                else if (userFilmDTO.IsViewed)
                {
                    userFilm.ViewedOn = DateTime.Now;
                    _unitOfWork.UserFilmRepository.Add(userFilm);
                    _unitOfWork.Save();
                }
                return Ok(new { PlayListAdded = isPlayListAdded, Rating = filmRating });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"💥Something has gone wrong! {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
