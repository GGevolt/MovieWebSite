using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebSite.Server.ViewModels
{
    public class WatchVideoVM
    {
        public string Title { get; set; }
        public List<string> FilePaths { get; set; }
        public List<string> QualityLevels { get; set; }
    }
}
