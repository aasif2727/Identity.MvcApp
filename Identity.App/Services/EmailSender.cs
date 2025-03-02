using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace Identity.App.Services
{
    public class EmailSender : IEmailSender
    {
        public string? SendGridKey { get; set; }
        public string? MailTrapKey { get; set; }
        public EmailSender(IConfiguration _config)
        {
            SendGridKey = _config.GetValue<string>("SendGrid:SecretKey");
            MailTrapKey = _config.GetValue<string>("MailTrap:SecretKey");
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            var client = new SmtpClient("live.smtp.mailtrap.io", 587)
            {
                Credentials = new NetworkCredential("api", MailTrapKey),
                EnableSsl = true
            };
            return client.SendMailAsync("hello@indian-tribe.com", email, subject, htmlMessage);
        }
    }
}
