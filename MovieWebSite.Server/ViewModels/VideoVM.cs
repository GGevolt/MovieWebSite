using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MovieWebSite.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebSite.Server.ViewModels
{
    public class VideoVM
    {
        public Video video { get; set; }
        public List<int> SelectedQuality { get; set; }
        public List<IFormFile> VidUrls { get; set; }

        [ValidateNever]
        public List<Quality>? qualities { get; set; }
    }
}
