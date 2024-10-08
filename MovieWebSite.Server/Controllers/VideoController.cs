using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.IO;

namespace MovieWebSite.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly IWebHostEnvironment _webhost;

        public VideoController(IWebHostEnvironment webhost)
        {
            _webhost = webhost;
        }

        [HttpGet("{videoName}")]
        public async Task<IActionResult> GetVideo(string videoName)
        {
            var videoPath = Path.Combine(_webhost.WebRootPath, "videos", videoName);
            if (!System.IO.File.Exists(videoPath))
            {
                return NotFound($"Video {videoName} file not found.");
            }

            var fileInfo = new FileInfo(videoPath);
            var fileLength = fileInfo.Length;
            var contentType = "video/mp4";

            var rangeHeader = Request.Headers[HeaderNames.Range].ToString();
            if (string.IsNullOrEmpty(rangeHeader))
            {
                return File(System.IO.File.OpenRead(videoPath), contentType, enableRangeProcessing: true);
            }

            var range = rangeHeader.Replace("bytes=", "").Split('-');
            var start = long.Parse(range[0]);
            var end = range.Length > 1 && !string.IsNullOrEmpty(range[1]) ? long.Parse(range[1]) : fileLength - 1;

            var length = end - start + 1;
            var stream = new FileStream(videoPath, FileMode.Open, FileAccess.Read);
            stream.Seek(start, SeekOrigin.Begin);

            Response.StatusCode = 206;
            Response.Headers[HeaderNames.ContentRange] = $"bytes {start}-{end}/{fileLength}";
            Response.Headers[HeaderNames.AcceptRanges] = "bytes";

            return File(stream, contentType, null, enableRangeProcessing: true);
        }
    }
}
