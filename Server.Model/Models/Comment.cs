using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string CommnentText { get; set; }
        [Required]
        public required string UserName { get; set; }
        public required int EpisodeId { get; set; }
        [ForeignKey("EpisodeId")]
        [ValidateNever]
        public Episode Episode { get; set; }
    }
}
