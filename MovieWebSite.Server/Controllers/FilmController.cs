using Microsoft.AspNetCore.Mvc;
using MovieWebSite.Server.Models;
using MovieWebSite.Server.ViewModels;
using MovieWebSite.Server.Repository.IRepository;

namespace MovieWebSite.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public IActionResult GetCateFilm(int id)
        {
            try
            {
                List<Category> filmCate = new  List<Category>();
                var cateIds = _unitOfWork.CategoryFilmRepository.GetAll().Where(cf => cf.FilmId == id).Select(cf => cf.CategoryId);
                foreach(var cateId in cateIds){
                    filmCate.Add(_unitOfWork.CategoryRepository.Get(c=>c.Id == cateId));
                }
                return Ok(filmCate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpPost]
        public IActionResult CreateUpdate([FromBody] FilmVM filmVM)
        {
            try
            {
                if (filmVM.Film.Id == 0)
                {
                    _unitOfWork.FilmRepository.Add(filmVM.Film);
                    _unitOfWork.Save();
                    if (filmVM.SelectedCategories != null && filmVM.SelectedCategories.Count > 0)
                    {
                        foreach (int categoryId in filmVM.SelectedCategories)
                        {
                            var category = _unitOfWork.CategoryRepository.Get(c => c.Id == categoryId);
                            if (category != null)
                            {
                                var categoryFilm = new CategoryFilm
                                {
                                    FilmId = filmVM.Film.Id,
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
                    _unitOfWork.FilmRepository.Update(filmVM.Film);
                    _unitOfWork.Save();
                    var PastCategoriyFilms = _unitOfWork.CategoryFilmRepository.GetAll().Where(cf => cf.FilmId == filmVM.Film.Id).ToList();
                    foreach (var categoryFilm in PastCategoriyFilms)
                    {
                        _unitOfWork.CategoryFilmRepository.Remove(categoryFilm);
                    }
                    if (filmVM.SelectedCategories != null && filmVM.SelectedCategories.Count > 0)
                    {
                        foreach (int categoryId in filmVM.SelectedCategories)
                        {
                            var category = _unitOfWork.CategoryRepository.Get(c => c.Id == categoryId);
                            if (category != null)
                            {
                                var categoryFilm = new CategoryFilm
                                {
                                    FilmId = filmVM.Film.Id,
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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var film = _unitOfWork.FilmRepository.Get(f => f.Id == id);
                string wwwRootPath = _webhost.WebRootPath;
                string imagePath = Path.Combine(wwwRootPath, "img");
                if (!string.IsNullOrEmpty(film.FilmImg))
                    {
                        var oldImagePath = Path.Combine(imagePath, film.FilmImg.TrimStart('\\'));
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
