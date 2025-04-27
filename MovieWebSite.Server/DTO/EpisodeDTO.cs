
using Microsoft.AspNetCore.Http;

namespace MovieWebSite.Server.DTO
{
    public class EpisodeDTO
    {
        public int Id { get; set; }
        public required int EpisodeNumber { get; set; }
        public required int FilmId { get; set; }
        public string? vidName { get; set; }
        public IFormFile? VideoFile { get; set; }
    }
}
