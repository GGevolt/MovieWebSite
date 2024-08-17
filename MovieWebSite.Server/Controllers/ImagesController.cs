using Microsoft.AspNetCore.Mvc;
using MovieWebSite.Server.Repository.IRepository;
using Server.Model.ViewModels;


namespace MovieWebSite.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController(IUnitOfWork unitOfWork, IWebHostEnvironment webhost) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IWebHostEnvironment _webhost = webhost;
        [HttpGet("{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            string wwwRootPath = _webhost.WebRootPath;
            var imagePath = Path.Combine(wwwRootPath, "img", imageName);
            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound();
            }
            // Determine the MIME type of the file
            var mimeType = "image/jpeg"; // Default to JPEG. Adjust based on the file extension if necessary.
            var extension = Path.GetExtension(imageName).ToLowerInvariant();
            if (extension == ".png")
            {
                mimeType = "image/png";
            }

            // Read the file into a byte array
            var imageBytes = System.IO.File.ReadAllBytes(imagePath);

            // Return the image as a response
            return File(imageBytes, mimeType);
        }
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
