using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model.DTO
{
    public class StripeSettingDTO
    {
        public string PublicKey { get; set; }
        public string WebHookSecret { get; set; }
    }
}
