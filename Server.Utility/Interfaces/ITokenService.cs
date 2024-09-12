using Server.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Utility.Interfaces
{
    public interface ITokenService
    {
        public string GenarateToken(ApplicationUser user);
    }
}
