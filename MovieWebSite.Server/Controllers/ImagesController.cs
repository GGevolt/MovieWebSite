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
    public class ImagesController(IWebHostEnvironment webhost) : ControllerBase
    {
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
            var mimeType = "image/jpeg"; 
            var extension = Path.GetExtension(imageName).ToLowerInvariant();
            if (extension == ".png")
            {
                mimeType = "image/png";
            }

            var imageBytes = System.IO.File.ReadAllBytes(imagePath);
            return File(imageBytes, mimeType);
        }
    }
}
