using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace CWBlazor.Server.Core.Email
{
    /// <inheritdoc />
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings smtpSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService"/> class.
        /// </summary>
        /// <param name="configuration">Configuration from DI.</param>
        public EmailService(IConfiguration configuration)
        {
            var emailSettings = new EmailSettings();
            configuration.Bind(nameof(EmailSettings), emailSettings);

            smtpSettings = emailSettings.SmtpSettings;
        }

        /// <inheritdoc />
        public void SendEmail(MailMessage mailMessage)
        {
            using var smtpClient = CreateSmtpClient();
            smtpClient.Send(mailMessage);
        }

        private SmtpClient CreateSmtpClient()
        {
            var networkCredentials = new NetworkCredential(smtpSettings.UserName, smtpSettings.UserPassword);

            var smtpClient = new SmtpClient
            {
                Host = smtpSettings.Host,
                Port = smtpSettings.Port,
                Timeout = smtpSettings.Timeout,
                EnableSsl = smtpSettings.EnableSsl,
                Credentials = networkCredentials,
            };

            return smtpClient;
        }
    }
}