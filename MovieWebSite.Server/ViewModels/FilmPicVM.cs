using MovieWebSite.Server.Models;


namespace MovieWebSite.Server.ViewModels
{
    public class FilmPicVM
    {
        public int FilmId { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}