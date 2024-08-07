﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sever.IdentityModel.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(40)]
        public required string FirstName { get; set; }
        [MaxLength(40)]
        public required string LastName { get; set; }
        public int Age { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Column(TypeName = "datetime")]
        public DateTime LastLogin { get; set; } = DateTime.Now;
        public Boolean IsAdmin { get; set; } = false;
    }
}
