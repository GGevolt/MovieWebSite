using Microsoft.AspNetCore.Mvc;
using Server.Model.Models;
using MovieWebSite.Server.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Server.Model.DTO;
using System.Diagnostics;
using Server.Utility.Interfaces;
using System.Text.Json;

namespace MovieWebSite.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmController(IUnitOfWork unitOfWork, IWebHostEnvironment webhost, IBlurhasher blurhasher) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IWebHostEnvironment _webhost = webhost;
        private readonly IBlurhasher _blurhasher = blurhasher;

        [HttpGet]
        public IActionResult GetFilms()
        {
            var films = _unitOfWork.FilmRepository.GetAll();
            return Ok(films);
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
        public async Task<IActionResult> CreateUpdate([FromForm] string filmCateDTO, IFormFile? imageFile)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var filmCateData = JsonSerializer.Deserialize<FilmCateDTO>(filmCateDTO, options);

                if (filmCateData == null || filmCateData.Film == null)
                {
                    return BadRequest("Invalid film data");
                }
                if (filmCateData.Film.Id == 0)
                {
                    _unitOfWork.FilmRepository.Add(filmCateData.Film);
                    _unitOfWork.Save();
                    if (imageFile != null)
                    {
                        await UplaodImage(filmCateData.Film, imageFile);
                    }

                    if (filmCateData.SelectedCategories != null && filmCateData.SelectedCategories.Count > 0)
                    {
                        foreach (int categoryId in filmCateData.SelectedCategories)
                        {
                            var category = _unitOfWork.CategoryRepository.Get(c => c.Id == categoryId);
                            if (category != null)
                            {
                                var categoryFilm = new CategoryFilm
                                {
                                    FilmId = filmCateData.Film.Id,
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
                    _unitOfWork.FilmRepository.Update(filmCateData.Film);
                    _unitOfWork.Save();
                    if (imageFile != null)
                    {
                        await UplaodImage(filmCateData.Film, imageFile);
                    }
                    var PastCategoriyFilms = _unitOfWork.CategoryFilmRepository.GetAll().Where(cf => cf.FilmId == filmCateData.Film.Id).ToList();
                    foreach (var categoryFilm in PastCategoriyFilms)
                    {
                        _unitOfWork.CategoryFilmRepository.Remove(categoryFilm);
                    }
                    if (filmCateData.SelectedCategories != null && filmCateData.SelectedCategories.Count > 0)
                    {
                        foreach (int categoryId in filmCateData.SelectedCategories)
                        {
                            var category = _unitOfWork.CategoryRepository.Get(c => c.Id == categoryId);
                            if (category != null)
                            {
                                var categoryFilm = new CategoryFilm
                                {
                                    FilmId = filmCateData.Film.Id,
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
                Debug.WriteLine("💥 Film error", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        private async Task UplaodImage(Film film, IFormFile imageFile)
        {
            try
            {
                string wwwRootPath = _webhost.WebRootPath;
                string imagePath = Path.Combine(wwwRootPath, "img");
                string fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
                string filePath = Path.Combine(imagePath, fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                if (!string.IsNullOrEmpty(film.FilmPath))
                {
                    string oldImagePath = Path.Combine(imagePath, film.FilmPath.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                film.FilmPath = fileName;
                film.BlurHash = _blurhasher.Encode(filePath);
                _unitOfWork.FilmRepository.Update(film);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("💥An error occurred while uploading the Movie picture:" + ex.Message);
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
