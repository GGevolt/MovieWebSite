using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebSite.Server.Models
{
	public class VideoQuality
	{
		public int VideoId { get; set; }
		public Video Video { get; set; }
		public int QualityId { get; set; }
		public Quality Quality { get; set; }
		public string VidUrl { get; set; }
	}
}
