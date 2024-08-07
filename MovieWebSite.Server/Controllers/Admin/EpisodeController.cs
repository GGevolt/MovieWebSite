using Microsoft.AspNetCore.Mvc;
using Server.Model.Models;
using MovieWebSite.Server.Repository.IRepository;
using Server.Model.ViewModels;
using System.Text.RegularExpressions;




namespace MovieWebSite.Server.Controllers.Admin
{
    [ApiController]
    [Route("[controller]")]
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
        [HttpPost]
        public IActionResult CreateUpdate([FromForm] EpisodeVM episodevm)
        {
            try
            {
                Episode ep = new Episode()
                {
                    Id = episodevm.Id,
                    EpisodeNumber = episodevm.EpisodeNumber,
                    FilmId = episodevm.FilmId,
                };
                string wwwRootPath = _webhost.WebRootPath;
                string videoPath = Path.Combine(wwwRootPath, "videos");
                if (ep.Id == 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(episodevm.VideoFile.FileName);
                    using (var fileStream = new FileStream(Path.Combine(videoPath, fileName), FileMode.Create))
                    {
                        episodevm.VideoFile.CopyTo(fileStream);
                    }
                    ep.VidName = fileName;
                    _unitOfWork.EpisodeRepository.Add(ep);
                    _unitOfWork.Save();
                    return Ok();
                }
                if (episodevm.VideoFile != null)
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
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(episodevm.VideoFile.FileName);
                    using (var fileStream = new FileStream(Path.Combine(videoPath, fileName), FileMode.Create))
                    {
                        episodevm.VideoFile.CopyTo(fileStream);
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

        [HttpDelete("{epId}")]
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
                            return StatusCode(500, $"Failed to delete old video: {ex.Message}");
                        }
                    }
                }
                _unitOfWork.EpisodeRepository.Remove(ep);
                _unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Fail to delete episode. {ex}");
            }
        }

    }
}
