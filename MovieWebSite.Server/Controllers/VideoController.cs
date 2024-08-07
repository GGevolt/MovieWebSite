using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieWebSite.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideoController(IWebHostEnvironment webhost) : ControllerBase
    {
        private readonly IWebHostEnvironment _webhost = webhost;
        [HttpGet("{videoName}")]
        public IActionResult GetVideo(string videoName)
        {
            string wwwRootPath = _webhost.WebRootPath;
            var videoPath = Path.Combine(wwwRootPath, "videos", videoName);
            if (!System.IO.File.Exists(videoPath))
            {
                return NotFound($"Video {videoName} file not found.");
            }

            string contentType = "video/mp4";

            return PhysicalFile(videoPath, contentType, videoName);
        }
    }
}
