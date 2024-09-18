using Microsoft.AspNetCore.Http;
using Server.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model.ViewModels
{
    public class FilmDTO
    {
        public required Film Film { get; set; }
        public List<int>? SelectedCategories { get; set; }
    }
}
