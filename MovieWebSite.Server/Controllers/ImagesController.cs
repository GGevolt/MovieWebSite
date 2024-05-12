using Microsoft.AspNetCore.Mvc;

namespace MovieWebSite.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagesController : ControllerBase
    {
        [HttpGet("{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            // Construct the full path to the image file
            var imagePath = Path.Combine("wwwroot", "img", imageName);

            // Check if the file exists
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
            else if (extension == ".gif")
            {
                mimeType = "image/gif";
            }
            // Add more conditions here for other image types as needed

            // Read the file into a byte array
            var imageBytes = System.IO.File.ReadAllBytes(imagePath);

            // Return the image as a response
            return File(imageBytes, mimeType);
        }
    }
}
