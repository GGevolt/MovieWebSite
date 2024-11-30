using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model.DTO
{
    public class UserFilmDTO
    {
        public int FilmId {  get; set; }
        public bool IsAddPlayList { get; set; }
        public bool IsRemoveFromPlayList { get; set; }
        public int FilmRatting { get; set; }
        public bool IsViewed { get; set; }
    }
}
