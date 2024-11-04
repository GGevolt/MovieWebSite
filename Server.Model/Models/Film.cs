using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        public string? FilmPath { get; set; }
        public string? BlurHash { get; set; }
        public required string Synopsis { get; set; }
        public required string Director { get; set; }
        public required string Type { get; set; }
        [ValidateNever]
        public virtual ICollection<CategoryFilm> CategoryFilms { get; set; }
        [ValidateNever]
        public virtual ICollection<Episode> Episodes { get; set; }
        [ValidateNever]
        public virtual ICollection<UserFilm> UserFilms { get; set; }
    }
}
