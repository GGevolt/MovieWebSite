using Microsoft.AspNetCore.Mvc;
using MovieWebSite.Server.Models;
using MovieWebSite.Server.Repository.IRepository;
using MovieWebSite.Server.ViewModels;

namespace MovieWebSite.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController(IUnitOfWork unitOfWork, IWebHostEnvironment webhost) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IWebHostEnvironment _webhost = webhost;
        [HttpPost]
        public IActionResult UploadMoviePicture([FromForm] IFormFile imageFile, int filmId)
        {
            try
            {
                string wwwRootPath = _webhost.WebRootPath;
                if (imageFile != null)
                {
                    var film = _unitOfWork.FilmRepository.Get(f=>f.Id==filmId);
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    string imagePath = Path.Combine(wwwRootPath, "img");
                    if (!string.IsNullOrEmpty(film.FilmImg))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, film.FilmImg.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create))
                    {
                        imageFile.CopyTo(fileStream);
                    }
                    film.FilmImg = fileName;
                    _unitOfWork.FilmRepository.Update(film);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while uploading the Movie picture.");
            }
        }
    }
}