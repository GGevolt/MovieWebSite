﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebSite.Server.ViewModels
{
    public class EmailComponent
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile>? Attachement { get; set; }
    }
}
