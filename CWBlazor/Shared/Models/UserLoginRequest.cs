namespace CWBlazor.Shared.Models
{
    /// <summary>
    /// Login request model.
    /// </summary>
    public class UserLoginRequest
    {
        /// <summary>
        /// User email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User password.
        /// </summary>
        public string Password { get; set; }
    }
}
