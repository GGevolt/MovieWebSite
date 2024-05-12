using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebSite.Server.Models
{
	public class VideoQuality
	{
		public int VideoId { get; set; }
        [ForeignKey("VideoId")]
        [ValidateNever]
        public Video Video { get; set; }
		public int QualityId { get; set; }
        [ForeignKey("QualityId")]
        [ValidateNever]
        public Quality Quality { get; set; }
		public string VidUrl { get; set; }
	}
}
