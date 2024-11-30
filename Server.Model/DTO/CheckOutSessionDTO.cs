using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model.DTO
{
    public class CheckOutSessionDTO
    {
        [Required]
        public string SuccessUrl { get; set; }
        [Required]
        public string FailureUrl { get; set; }
        [Required]
        public string Plan { get; set; }
    }
}
