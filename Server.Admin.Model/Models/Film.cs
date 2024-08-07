using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model.Models
{
    public class Film
    {
        [Key]
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? FilmImg { get; set; }
        public required string Synopsis { get; set; }
        public required string Director { get; set; }
        public required string Type { get; set; }
        public virtual ICollection<CategoryFilm> CategoryFilms { get; set; }
        public virtual ICollection<Episode> Episodes { get; set; }
    }
}
