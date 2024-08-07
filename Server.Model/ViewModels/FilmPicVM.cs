using Microsoft.AspNetCore.Http;


namespace Server.Model.ViewModels
{
    public class FilmPicVM
    {
        public int FilmId { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}