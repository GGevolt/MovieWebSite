using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebSite.Server.Models
{
	public class Quality
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string QualityName { get; set; }
		[ValidateNever]
		public virtual ICollection<VideoQuality>? VideoQualities { get; set; }

	}
}
