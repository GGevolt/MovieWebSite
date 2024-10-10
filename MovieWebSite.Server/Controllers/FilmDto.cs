namespace MovieWebSite.Server.Controllers
{
    internal class FilmDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FilmPath { get; set; }
        public string BlurHash { get; set; }
        public string Synopsis { get; set; }
        public string Director { get; set; }
        public string Type { get; set; }
    }
}