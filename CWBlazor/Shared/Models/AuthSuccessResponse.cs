namespace CWBlazor.Shared.Models
{
    /// <summary>
    /// Success authentication response.
    /// </summary>
    public class AuthSuccessResponse
    {
        /// <summary>
        /// Auth token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Refresh token.
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
