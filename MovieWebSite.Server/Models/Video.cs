using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieWebSite.Server.Models
{
	public class Video
	{
        [Key]
        public int Id { get; set; }
        public int EpisodeId { get; set; }
        [ForeignKey("EpisodeId")]
        [ValidateNever]
        public Episode Episode { get; set; }
        public string VidName { get; set; }
	}
}
