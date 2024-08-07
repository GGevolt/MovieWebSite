using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Model.Models
{
    public class Episode
    {
        [Key]
        public int Id { get; set; }
        public required int EpisodeNumber { get; set; }
        public required int FilmId { get; set; }
        [ForeignKey("FilmId")]
        public Film Film { get; set; }
        public string VidName { get; set; }
    }
}
