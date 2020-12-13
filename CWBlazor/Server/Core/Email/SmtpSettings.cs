namespace CWBlazor.Server.Core.Email
{
    /// <summary>
    /// Smtp client settings.
    /// </summary>
    public class SmtpSettings
    {
        /// <summary>
        /// Default timeout in milliseconds.
        /// </summary>
        private const int DefaultTimeout = 10000;

        /// <summary>
        /// Default port used for SMTP transactions.
        /// </summary>
        private const int DefaultPort = 25;

        /// <summary>
        /// The name or IP address of the host used for SMTP transactions.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Use name used in credentials to authenticate the sender.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password used in credentials to authenticate the sender.
        /// </summary>
        public string UserPassword { get; set; }

        /// <summary>
        /// The port used for SMTP transactions.
        /// </summary>
        public int Port { get; set; } = DefaultPort;

        /// <summary>
        /// Specify if SSL is used to encrypt the connection or not. Default is <see langword="false" />.
        /// </summary>
        public bool EnableSsl { get; set; }

        /// <summary>
        /// The amount of time after which a send call times out.
        /// </summary>
        public int Timeout { get; set; } = DefaultTimeout;
    }
}