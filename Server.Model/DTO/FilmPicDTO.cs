using Microsoft.AspNetCore.Http;


namespace Server.Model.DTO
{
    public class FilmPicDTO
    {
        public int FilmId { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}