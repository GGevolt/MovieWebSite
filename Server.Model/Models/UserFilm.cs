using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Server.Model.AuthModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model.Models
{
    public class UserFilm
    {
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser User { get; set; }
        public DateTime? ViewedOn { get; set; }
        public int? Rating { get; set; }
        public DateTime? AddPlaylistOn { get; set; }
        public int FilmId { get; set; }
        [ForeignKey("FilmId")]
        [ValidateNever]
        public Film Film { get; set; }
    }
}
