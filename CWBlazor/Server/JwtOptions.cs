namespace CWBlazor.Server
{
    /// <summary>
    /// JWT options from appsettings.json.
    /// </summary>
    public class JwtOptions
    {
        /// <summary>
        /// JWT secret.
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// JWT token lifetime in seconds.
        /// </summary>
        public int TokenLifetimeInSeconds { get; set; }

        /// <summary>
        /// JWT refresh token lifetime in seconds.
        /// </summary>
        public int RefreshTokenLifetimeInSeconds { get; set; }
    }
}