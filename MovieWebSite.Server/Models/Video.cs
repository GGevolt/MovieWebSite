using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebSite.Server.Models
{
	public class Video
	{
		[Key]
		public int Id { get; set; }
		public string Title { get; set; }
		[ValidateNever]
		public virtual ICollection<VideoQuality>? VideoQualities { get; set; }
	}
}