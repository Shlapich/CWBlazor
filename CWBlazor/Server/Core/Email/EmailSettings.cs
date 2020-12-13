namespace CWBlazor.Server.Core.Email
{
    /// <summary>
    /// Email settings.
    /// </summary>
    public class EmailSettings
    {
        /// <summary>
        /// Sender email.
        /// </summary>
        public string FromEmail { get; set; }

        /// <summary>
        /// Smtp client settings.
        /// </summary>
        public SmtpSettings SmtpSettings { get; set; } = new SmtpSettings();
    }
}