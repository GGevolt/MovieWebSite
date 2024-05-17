using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MovieWebSite.Server.Models
{
    public class Episode
    {
        [Key]
        public int Id { get; set; }
        public required string Title { get; set; }
        public required int EpisodeNumber { get; set; }
        [ValidateNever]
        public virtual ICollection<VideoQuality>? VideoQualities { get; set; }
    }
}
