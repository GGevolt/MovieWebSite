using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieWebSite.Server.Repository.IRepository;
using NuGet.Protocol;
using Server.Model.AuthModels;
using Server.Model.DTO;
using Server.Model.Models;
using System.Diagnostics;

namespace MovieWebSite.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayListController(IUnitOfWork unitOfWork, SignInManager<ApplicationUser> signInManager) : Controller
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        [HttpGet, Authorize(Roles = "UserT2")]
        public async Task<IActionResult> GetUserPlayList()
        {
            try
            {
                var user = await _signInManager.UserManager.GetUserAsync(User);
                if (user == null)
                {
                    throw new Exception("💥Can't find user in get play list!");
                }
                var playListFilmId = _unitOfWork.PlayListRepository.GetAll(pl => pl.UserName == user.UserName).Select(pl => pl.FilmId);
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
        [HttpPost, Authorize(Roles = "UserT2")]
        public async Task<IActionResult> AddToPlayList(PlayListDTO playListDTO)
        {
            try
            {
                var user = await _signInManager.UserManager.GetUserAsync(User);
                if (user == null)
                {
                    throw new Exception("💥Can't find user in get play list!");
                }
                var UserPlayList = _unitOfWork.PlayListRepository.GetAll(pl => pl.UserName == user.UserName);
                PlayList existingItem = UserPlayList.FirstOrDefault(pl => pl.FilmId == playListDTO.FilmId);
                if (existingItem != null)
                {
                    Debug.WriteLine("💥Halo wrold!");
                    if (playListDTO.IsRemove)
                    {
                        _unitOfWork.PlayListRepository.Remove(existingItem);
                        _unitOfWork.Save();
                        return Ok(new { isAdded = false });
                    }
                    return Ok(new {isAdded= true});
                }
                if (playListDTO.IsAdd)
                {
                    PlayList newPlayListItem = new PlayList()
                    {
                        UserName = user.UserName,
                        FilmId = playListDTO.FilmId
                    };
                    _unitOfWork.PlayListRepository.Add(newPlayListItem);
                    _unitOfWork.Save();
                    return Ok(new { isAdded = true });
                }
                return Ok(new { isAdded = false });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"💥Something has gone wrong! {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
