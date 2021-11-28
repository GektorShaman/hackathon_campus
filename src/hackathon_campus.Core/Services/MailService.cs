using hackathon_campus.Core.DataAccess;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Net.Http;

namespace hackathon_campus.Core.Services
{
    public class MailService
    {
        private readonly MailSettings _mailSettings;

        private readonly IHttpClientFactory _clientFactory;

        public MailService(IOptions<MailSettings> mailSettings, IHttpClientFactory clientFactory)
        {
            _mailSettings = mailSettings.Value;
            _clientFactory = clientFactory;
        }

        public async Task SendEmail(string email, string subject, string message)
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
                await client.ConnectAsync(_mailSettings.Host, _mailSettings.Port, true);
                await client.AuthenticateAsync(_mailSettings.Address, _mailSettings.Password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }

        public async Task AskTelegramBot(string email, string message)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://api.telegram.org/2144370865:AAEoJfPQL8wSmbZdMM22vNGI_FclHu80zHI/getUpdates");

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);


        }
    }
}
