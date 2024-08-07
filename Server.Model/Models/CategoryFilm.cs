using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model.Models
{
    public class CategoryFilm
    {
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        public int FilmId { get; set; }
        [ForeignKey("FilmId")]
        [ValidateNever]
        public Film Film { get; set; }
    }
}
