using System.Net.Mail;

namespace CWBlazor.Server.Core.Email
{
    /// <summary>
    /// Email service.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Send email message.
        /// </summary>
        /// <param name="mailMessage">Message.</param>
        void SendEmail(MailMessage mailMessage);
    }
}