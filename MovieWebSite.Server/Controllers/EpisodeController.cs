using Microsoft.AspNetCore.Mvc;
using MovieWebSite.Server.Models;
using MovieWebSite.Server.Repository.IRepository;
using MovieWebSite.Server.ViewModels;
using System.Text.RegularExpressions;




namespace MovieWebSite.Server.Controllers
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
                return Ok( _unitOfWork.EpisodeRepository.GetAll().Where(e=>e.FilmId==id).ToList());
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
                Episode ep = new Episode() { 
                    Id = episodevm.Id,
                    EpisodeNumber = episodevm.EpisodeNumber,
                    FilmId = episodevm.FilmId,
                };
                string wwwRootPath = _webhost.WebRootPath;
                string videoPath = Path.Combine(wwwRootPath, "videos");
                if (ep.Id == 0)
                {
                    _unitOfWork.EpisodeRepository.Add(ep);
                    _unitOfWork.Save();
                    string fileName = _unitOfWork.FilmRepository.Get(f=>f.Id==ep.FilmId).Title.Replace(" ", "") + "Episode"+ ep.EpisodeNumber + Path.GetExtension(episodevm.VideoFile.FileName);
                    using (var fileStream = new FileStream(Path.Combine(videoPath, fileName), FileMode.Create))
                    {
                        episodevm.VideoFile.CopyTo(fileStream);
                    }
                    Video vid = new Video()
                    {
                        Id = 0,
                        EpisodeId= ep.Id,
                        VidName = fileName
                    };
                    _unitOfWork.VideoRepository.Add(vid);
                    _unitOfWork.Save();
                }
                else
                {
                    if (episodevm.VideoFile != null) {
                        var vid = _unitOfWork.VideoRepository.Get(v => v.EpisodeId == ep.Id);
                        if (vid != null)
                        {
                            if (!string.IsNullOrEmpty(vid.VidName))
                            {
                                var oldVidPath = Path.Combine(videoPath, vid.VidName.TrimStart('\\'));
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
                            string fileName = _unitOfWork.FilmRepository.Get(f => f.Id == ep.FilmId).Title.Replace(" ", "") + "Episode" + ep.EpisodeNumber + Path.GetExtension(episodevm.VideoFile.FileName);
                            using (var fileStream = new FileStream(Path.Combine(videoPath, fileName), FileMode.Create))
                            {
                                episodevm.VideoFile.CopyTo(fileStream);
                            }
                            vid.VidName = fileName;
                            _unitOfWork.VideoRepository.Update(vid);
                        }
                    }
                    _unitOfWork.EpisodeRepository.Update(ep);
                    _unitOfWork.Save();

                }
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
                var vid = _unitOfWork.VideoRepository.Get(v=>v.EpisodeId == ep.Id);
                if (vid!=null) {
                    string wwwRootPath = _webhost.WebRootPath;
                    var videoPath = Path.Combine(wwwRootPath, "videos");
                    if (!string.IsNullOrEmpty(vid.VidName))
                    {
                        var oldVidPath = Path.Combine(videoPath, vid.VidName.TrimStart('\\'));
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
                    _unitOfWork.VideoRepository.Remove(vid); 
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
