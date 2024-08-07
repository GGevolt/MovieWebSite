using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using Server.Model.ViewModels;
using Server.Utility.Interfaces;

namespace Server.Utility.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;
        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendEmailAsync(EmailComponent emailComponent)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.SenderEmail);
            email.To.Add(MailboxAddress.Parse(emailComponent.To));
            email.Subject = emailComponent.Subject;
            var builder = new BodyBuilder();
            if (emailComponent.Attachement != null)
            {
                byte[] filebytes;
                foreach (var file in emailComponent.Attachement)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            filebytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.Name, filebytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = emailComponent.Body;
            email.Body = builder.ToMessageBody();
            using (MailKit.Net.Smtp.SmtpClient mailClient = new MailKit.Net.Smtp.SmtpClient())
            {
                mailClient.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                mailClient.Connect(_mailSettings.Server, _mailSettings.Port, SecureSocketOptions.StartTls);
                mailClient.Authenticate(_mailSettings.UserName, _mailSettings.Password);
                mailClient.Send(email);
                mailClient.Disconnect(true);
            }
        }
    }
}
