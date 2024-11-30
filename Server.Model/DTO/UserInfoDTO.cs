using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model.DTO
{
    public class UserInfoDTO
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public DateOnly Dob { get; set; }
        public DateTime AccountCreatedDate { get; set; }
        public string? SubscriptionPlan { get; set; }
        public DateTime? SubscriptionEndPeriod { get; set; }
    }
}
