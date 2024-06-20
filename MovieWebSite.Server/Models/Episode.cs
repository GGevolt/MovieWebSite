using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieWebSite.Server.Models
{
    public class Episode
    {
        [Key]
        public int Id { get; set; }
        public required int EpisodeNumber { get; set; }
        public required int FilmId { get; set; }
        [ForeignKey("FilmId")]
        [ValidateNever]
        public Film Film { get; set; }
        [ValidateNever]
        public virtual ICollection<Video>? Videos{ get; set; }
    }
}
