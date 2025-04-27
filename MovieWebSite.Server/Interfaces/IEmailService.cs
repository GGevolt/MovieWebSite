using MovieWebSite.Server.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebSite.Server.Interfaces
{
    public interface IEmailService
    {
        public Task SendEmailAsync(EmailComponent emailComponent);
    }
}
