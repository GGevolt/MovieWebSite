using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebSite.Server.DTO
{
    public class StripeSettingDTO
    {
        public string PublicKey { get; set; }
        public string WebHookSecret { get; set; }
        public string ProPriceId { get; set; }
        public string PremiumPriceId { get; set; }
    }
}
