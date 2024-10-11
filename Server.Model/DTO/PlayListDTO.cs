using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model.DTO
{
    public class PlayListDTO
    {
        public int FilmId {  get; set; }
        public bool IsAdd { get; set; }
        public bool IsRemove { get; set; }
    }
}
