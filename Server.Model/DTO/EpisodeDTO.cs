
using Microsoft.AspNetCore.Http;

namespace Server.Model.DTO
{
    public class EpisodeDTO
    {
        public int Id { get; set; }
        public required int EpisodeNumber { get; set; }
        public required int FilmId { get; set; }
        public IFormFile VideoFile { get; set; }
    }
}
