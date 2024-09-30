using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieWebSite.Server.Repository.IRepository;
using Server.Model.DTO;
using Server.Utility.Interfaces;
using Server.Utility.Services;
using System.IO;


namespace MovieWebSite.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController(IUnitOfWork unitOfWork, IWebHostEnvironment webhost, IBlurhasher blurhasher) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IWebHostEnvironment _webhost = webhost;
        private  readonly IBlurhasher _blurhasher = blurhasher;
        [HttpGet("{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            string wwwRootPath = _webhost.WebRootPath;
            var imagePath = Path.Combine(wwwRootPath, "img", imageName);
            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound();
            }
            var mimeType = "image/jpeg"; 
            var extension = Path.GetExtension(imageName).ToLowerInvariant();
            if (extension == ".png")
            {
                mimeType = "image/png";
            }

            var imageBytes = System.IO.File.ReadAllBytes(imagePath);
            return File(imageBytes, mimeType);
        }
        [HttpPost, Authorize(Roles = "Admin")]
        public IActionResult UploadMoviePicture([FromForm] FilmPicDTO filmPicDTO)
        {
            try
            {
                string wwwRootPath = _webhost.WebRootPath;
                string imagePath = Path.Combine(wwwRootPath, "img");
                if (filmPicDTO.ImageFile != null)
                {
                    var film = _unitOfWork.FilmRepository.Get(f => f.Id == filmPicDTO.FilmId);
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
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(filmPicDTO.ImageFile.FileName);
                    using (var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create))
                    {
                        filmPicDTO.ImageFile.CopyTo(fileStream);
                    }
                    string blurhashString = _blurhasher.Encode(Path.Combine(imagePath, fileName));
                    film.FilmPath = fileName;
                    film.BlurHash = blurhashString;
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
