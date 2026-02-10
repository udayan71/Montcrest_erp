using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Montcrest.BLL.Interfaces;

namespace Montcrest.BLL.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendExamLinkAsync(string toEmail, string candidateName, string examLink)
        {
            var host = _config["EmailSettings:SmtpHost"];
            var port = int.Parse(_config["EmailSettings:SmtpPort"] ?? "587");

            var fromEmail = _config["EmailSettings:FromEmail"];
            var username = _config["EmailSettings:Username"];
            var password = _config["EmailSettings:Password"];

            if (string.IsNullOrWhiteSpace(host) ||
                string.IsNullOrWhiteSpace(fromEmail) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("EmailSettings configuration missing in appsettings.json");
            }

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Montcrest HR", fromEmail));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = "Montcrest Recruitment Exam Link";

            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $@"
                    <h2>Hello {candidateName},</h2>
                    <p>You have been shortlisted for the online assessment.</p>

                    <p><b>Exam Link:</b>
                        <a href='{examLink}' target='_blank'>Click here to start exam</a>
                    </p>

                    <p>Please complete the exam within the given time.</p>

                    <br/>
                    <p>Regards,<br/>Montcrest HR Team</p>
                "
            };

            using var client = new SmtpClient();

            await client.ConnectAsync(host, port, MailKit.Security.SecureSocketOptions.StartTls);

            // Gmail requires login using Username + AppPassword
            await client.AuthenticateAsync(username, password);

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
