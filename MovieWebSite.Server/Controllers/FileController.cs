using Microsoft.AspNetCore.Mvc;
using MovieWebSite.Server.Models;
using MovieWebSite.Server.Repository.IRepository;
using MovieWebSite.Server.ViewModels;
using System.Drawing;

namespace MovieWebSite.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController(IUnitOfWork unitOfWork, IWebHostEnvironment webhost) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IWebHostEnvironment _webhost = webhost;
        [HttpPost]
        public IActionResult UploadMoviePicture([FromForm] FilmPicVM filmPicVM)
        {
            try
            {
                string wwwRootPath = _webhost.WebRootPath;
                string imagePath = Path.Combine(wwwRootPath, "img");
                if (filmPicVM.ImageFile != null)
                {
                    var film = _unitOfWork.FilmRepository.Get(f => f.Id == filmPicVM.FilmId);
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
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(filmPicVM.ImageFile.FileName);
                    using (var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create))
                    {
                        filmPicVM.ImageFile.CopyTo(fileStream);
                    }
                    film.FilmImg = fileName;
                    _unitOfWork.FilmRepository.Update(film);
                    _unitOfWork.Save();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while uploading the Movie picture:" + ex);
            }
        }
    }
}