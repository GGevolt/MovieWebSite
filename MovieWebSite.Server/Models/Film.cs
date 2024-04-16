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
		[Required]
		public string Title { get; set; }
		public string? FilmImg { get; set; }
		public string Synopsis { get; set; }
		public string Director { get; set; }
		[ValidateNever]
		public virtual ICollection<CategoryFilm> CategoryFilms { get; set; }
	}
}
