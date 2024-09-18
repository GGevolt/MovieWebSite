using Microsoft.AspNetCore.Mvc;
using Server.Model.Models;
using MovieWebSite.Server.Repository.IRepository;
using Server.Model.ViewModels;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;




namespace MovieWebSite.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EpisodeController(IUnitOfWork unitOfWork, IWebHostEnvironment webhost) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IWebHostEnvironment _webhost = webhost;
        [HttpGet("{id}")]
        public IActionResult GetFilmEpisode(int id)
        {
            try
            {
                return Ok(_unitOfWork.EpisodeRepository.GetAll().Where(e => e.FilmId == id).ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal server error.");
            }
        }
        [HttpPost, Authorize(Roles = "Admin")]
        public IActionResult CreateUpdate([FromForm] EpisodeDTO episodeDTO)
        {
            try
            {
                Episode ep = new Episode()
                {
                    Id = episodeDTO.Id,
                    EpisodeNumber = episodeDTO.EpisodeNumber,
                    FilmId = episodeDTO.FilmId,
                };
                string wwwRootPath = _webhost.WebRootPath;
                string videoPath = Path.Combine(wwwRootPath, "videos");
                if (ep.Id == 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(episodeDTO.VideoFile.FileName);
                    using (var fileStream = new FileStream(Path.Combine(videoPath, fileName), FileMode.Create))
                    {
                        episodeDTO.VideoFile.CopyTo(fileStream);
                    }
                    ep.VidName = fileName;
                    _unitOfWork.EpisodeRepository.Add(ep);
                    _unitOfWork.Save();
                    return Ok();
                }
                if (episodeDTO.VideoFile != null)
                {
                    if (!string.IsNullOrEmpty(ep.VidName))
                    {
                        var oldVidPath = Path.Combine(videoPath, ep.VidName.TrimStart('\\'));
                        if (System.IO.File.Exists(oldVidPath))
                        {
                            try
                            {
                                System.IO.File.Delete(oldVidPath);
                            }
                            catch (Exception ex)
                            {
                                return StatusCode(500, $"Failed to delete old video: {ex.Message}");
                            }
                        }
                    }
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(episodeDTO.VideoFile.FileName);
                    using (var fileStream = new FileStream(Path.Combine(videoPath, fileName), FileMode.Create))
                    {
                        episodeDTO.VideoFile.CopyTo(fileStream);
                    }
                    ep.VidName = fileName;
                }
                _unitOfWork.EpisodeRepository.Update(ep);
                _unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("{epId}"), Authorize(Roles = "Admin")]
        public IActionResult Delete(int epId)
        {
            try
            {
                var ep = _unitOfWork.EpisodeRepository.Get(c => c.Id == epId);
                string wwwRootPath = _webhost.WebRootPath;
                var videoPath = Path.Combine(wwwRootPath, "videos");
                if (!string.IsNullOrEmpty(ep.VidName))
                {
                    var oldVidPath = Path.Combine(videoPath, ep.VidName.TrimStart('\\'));
                    if (System.IO.File.Exists(oldVidPath))
                    {
                        try
                        {
                            System.IO.File.Delete(oldVidPath);
                        }
                        catch (Exception ex)
                        {
                            return BadRequest(new {message = "Can't delete video!"});
                        }
                    }
                }
                _unitOfWork.EpisodeRepository.Remove(ep);
                _unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Something has gone wrong" });
            }
        }

    }
}
