using Microsoft.AspNetCore.Mvc;
using Server.Model.Models;
using MovieWebSite.Server.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Server.Model.DTO;

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
                return BadRequest("Error, Fail to get film!");
            }
        }
        [HttpGet("search")]
        public IActionResult SearchFilms([FromQuery] string? query, [FromQuery] string? categories, [FromQuery] string? type, [FromQuery] string? director)
        {
            try
            {
                var films = _unitOfWork.FilmRepository.GetAll();

                if (!string.IsNullOrEmpty(query))
                {
                    films = films.Where(f => f.Title.Contains(query, StringComparison.OrdinalIgnoreCase));
                }

                if (!string.IsNullOrEmpty(categories))
                {
                    var categoryIds = categories.Split(',').Select(int.Parse).ToList();
                    films = films.Where(f => categoryIds.All(categoryId =>
                        _unitOfWork.CategoryFilmRepository.GetAll().Any(cf => cf.FilmId == f.Id && cf.CategoryId == categoryId)
                    ));
                }

                if (!string.IsNullOrEmpty(type))
                {
                    films = films.Where(f => f.Type.Equals(type, StringComparison.OrdinalIgnoreCase));
                }

                if (!string.IsNullOrEmpty(director))
                {
                    films = films.Where(f => f.Director.Contains(director, StringComparison.OrdinalIgnoreCase));
                }

                var result = films.Select(f => new FilmDTO
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
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal server error.");
            }
        }
        [HttpGet("relatefilms/{filmId}")]
        public IActionResult RelateFilms(int filmId) {
            try
            {
                Film film = _unitOfWork.FilmRepository.Get(f => f.Id == filmId);
                if (film == null)
                {
                    return NotFound("Film not found");
                }
                var relatedFilms = GetRelatedFilms(film);
                return Ok(relatedFilms);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest("Error, Fail to get relate film!");
            }
        }

        private List<FilmDTO> GetRelatedFilms(Film parentFilm)
        {
            var directorMatch = _unitOfWork.FilmRepository.GetAll()
                .Where(f => f.Director == parentFilm.Director && f.Id != parentFilm.Id);

            string titleBeforeColon = parentFilm.Title.Trim().ToLower();
            string seriesTitle = parentFilm.Title.Trim().ToLower();
            if (parentFilm.Title.Contains(':'))
            {
                string[] titleParts = parentFilm.Title.Split(new[] { ':' }, 2);
                titleBeforeColon = titleParts[0].Trim().ToLower();
            }
            if (parentFilm.Title.Contains("season", StringComparison.CurrentCultureIgnoreCase))
            {
                string[] titleParts = parentFilm.Title.ToLower().Split(new[] { "season" }, StringSplitOptions.None);
                seriesTitle = titleParts[0].Trim().ToLower();
            }

            var titleMatch = _unitOfWork.FilmRepository.GetAll()
                .Where(f => f.Id != parentFilm.Id &&
                           (f.Title.StartsWith(titleBeforeColon, StringComparison.OrdinalIgnoreCase) ||
                            f.Title.StartsWith(seriesTitle, StringComparison.OrdinalIgnoreCase)));

            var parentfilmCateIds = _unitOfWork.CategoryFilmRepository.GetAll()
                .Where(cf => cf.FilmId == parentFilm.Id)
                .Select(cf => cf.CategoryId);

            var relatedFilmIds = _unitOfWork.CategoryFilmRepository.GetAll()
                .Where(cf => parentfilmCateIds.Contains(cf.CategoryId) && cf.FilmId != parentFilm.Id)
                .Select(cf => cf.FilmId)
                .Distinct();

            var genreMatch = _unitOfWork.FilmRepository.GetAll()
                .Where(f => relatedFilmIds.Contains(f.Id));

            var combinedQuery = directorMatch.Union(titleMatch).Union(genreMatch).Distinct();

            return combinedQuery.Select(f => new FilmDTO
            {
                Id = f.Id,
                Title = f.Title,
                FilmPath = f.FilmPath,
                BlurHash = f.BlurHash,
                Synopsis = f.Synopsis,
                Director = f.Director,
                Type = f.Type
            }).Take(20).ToList();
        }


        [HttpPost, Authorize(Roles = "Admin")]
        public IActionResult CreateUpdate([FromBody] FilmCateDTO filmDTO)
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
