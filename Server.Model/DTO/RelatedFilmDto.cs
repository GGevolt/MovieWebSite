using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model.DTO
{
    public class RelatedFilmDto
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
