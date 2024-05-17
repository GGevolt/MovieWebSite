using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebSite.Server.Models
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
		[ValidateNever]
		public virtual ICollection<CategoryFilm> CategoryFilms { get; set; }
	}
}
