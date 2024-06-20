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

            // Check if the file exists
            if (!System.IO.File.Exists(videoPath))
            {
                return NotFound("Video file not found.");
            }

            // Return the file with the correct MIME type for HLS
            return PhysicalFile(videoPath, "application/x-mpegURL");
        }
    }
}
