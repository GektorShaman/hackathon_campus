using hackathon_campus.Core.DataAccess;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using System;

namespace hackathon_campus.Core.Services
{
    public class MailService
    {
        private readonly MailSettings _mailSettings;

        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public void SendEmail(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Address));
            emailMessage.To.Add(MailboxAddress.Parse(email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("Plain")
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.Connect(_mailSettings.Host, _mailSettings.Port, true);
                client.Authenticate(_mailSettings.Address, _mailSettings.Password);
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }
    }
}
