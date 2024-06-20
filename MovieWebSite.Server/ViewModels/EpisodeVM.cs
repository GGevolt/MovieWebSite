﻿namespace MovieWebSite.Server.ViewModels
{
    public class EpisodeVM
    {
        public int Id { get; set; }
        public required int EpisodeNumber { get; set; }
        public required int FilmId { get; set; }
        public IFormFile VideoFile { get; set; }
    }
}
