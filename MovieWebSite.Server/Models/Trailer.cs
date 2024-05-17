using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MovieWebSite.Server.Models
{
    public class Trailer
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        [ValidateNever]
        public virtual ICollection<VideoQuality>? VideoQualities { get; set; }
    }
}
