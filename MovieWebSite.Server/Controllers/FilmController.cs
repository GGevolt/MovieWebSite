using Microsoft.AspNetCore.Mvc;
using Server.Model.Models;
using MovieWebSite.Server.Repository.IRepository;
using Server.Model.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace MovieWebSite.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmController(IUnitOfWork unitOfWork, IWebHostEnvironment webhost) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IWebHostEnvironment _webhost = webhost;

        [HttpGet]
        public IActionResult GetFilms()
        {
            return Ok(_unitOfWork.FilmRepository.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetFilm(int id)
        {
            try
            {
                return Ok(_unitOfWork.FilmRepository.Get(f => f.Id == id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpPost, Authorize(Roles = "Admin")]
        public IActionResult CreateUpdate([FromBody] FilmDTO filmDTO)
        {
            try
            {
                if (filmDTO.Film.Id == 0)
                {
                    _unitOfWork.FilmRepository.Add(filmDTO.Film);
                    _unitOfWork.Save();
                    if (filmDTO.SelectedCategories != null && filmDTO.SelectedCategories.Count > 0)
                    {
                        foreach (int categoryId in filmDTO.SelectedCategories)
                        {
                            var category = _unitOfWork.CategoryRepository.Get(c => c.Id == categoryId);
                            if (category != null)
                            {
                                var categoryFilm = new CategoryFilm
                                {
                                    FilmId = filmDTO.Film.Id,
                                    CategoryId = category.Id
                                };
                                _unitOfWork.CategoryFilmRepository.Add(categoryFilm);
                            }
                        }
                        _unitOfWork.Save();
                    }
                }
                else
                {
                    _unitOfWork.FilmRepository.Update(filmDTO.Film);
                    _unitOfWork.Save();
                    var PastCategoriyFilms = _unitOfWork.CategoryFilmRepository.GetAll().Where(cf => cf.FilmId == filmDTO.Film.Id).ToList();
                    foreach (var categoryFilm in PastCategoriyFilms)
                    {
                        _unitOfWork.CategoryFilmRepository.Remove(categoryFilm);
                    }
                    if (filmDTO.SelectedCategories != null && filmDTO.SelectedCategories.Count > 0)
                    {
                        foreach (int categoryId in filmDTO.SelectedCategories)
                        {
                            var category = _unitOfWork.CategoryRepository.Get(c => c.Id == categoryId);
                            if (category != null)
                            {
                                var categoryFilm = new CategoryFilm
                                {
                                    FilmId = filmDTO.Film.Id,
                                    CategoryId = category.Id
                                };
                                _unitOfWork.CategoryFilmRepository.Add(categoryFilm);
                            }
                        }
                        _unitOfWork.Save();
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                var film = _unitOfWork.FilmRepository.Get(f => f.Id == id);
                string wwwRootPath = _webhost.WebRootPath;
                string imagePath = Path.Combine(wwwRootPath, "img");
                if (!string.IsNullOrEmpty(film.FilmPath))
                {
                    var oldImagePath = Path.Combine(imagePath, film.FilmPath.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        try
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                        catch (Exception ex)
                        {
                            return StatusCode(500, $"Failed to delete old image: {ex.Message}");
                        }
                    }
                }
                _unitOfWork.FilmRepository.Remove(film);
                _unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
