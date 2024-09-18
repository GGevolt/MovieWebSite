using Microsoft.AspNetCore.Http;


namespace Server.Model.ViewModels
{
    public class FilmPicDTO
    {
        public int FilmId { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}