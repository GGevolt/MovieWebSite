using Server.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Utility.Interfaces
{
    public interface IEmailService
    {
        public Task SendEmailAsync(EmailComponent emailComponent);
    }
}
