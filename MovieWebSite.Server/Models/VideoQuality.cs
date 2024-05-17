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
		public int? TrailerId { get; set; }
        [ForeignKey("TrailerId")]
        [ValidateNever]
        public Trailer? Trailer { get; set; }
        public int? EpisodeId { get; set; }
        [ForeignKey("EpisodeId")]
        [ValidateNever]
        public Episode? Episode { get; set; }
		public required int QualityId { get; set; }
        [ForeignKey("QualityId")]
        [ValidateNever]
        public required Quality Quality { get; set; }
		public string VidUrl { get; set; }
	}
}
