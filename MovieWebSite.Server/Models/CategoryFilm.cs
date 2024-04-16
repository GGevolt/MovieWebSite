using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebSite.Server.Models
{
	public class CategoryFilm
	{
		public int CategoryId { get; set; }
		public Category Category { get; set; }
		public int FilmId { get; set; }
		public Film Film { get; set; }
	}
}
