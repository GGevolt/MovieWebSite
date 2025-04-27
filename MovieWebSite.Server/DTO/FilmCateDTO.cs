using Microsoft.AspNetCore.Http;
using MovieWebSite.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebSite.Server.DTO
{
    public class FilmCateDTO
    {
        public required Film Film { get; set; }
        public List<int>? SelectedCategories { get; set; }
    }
}
