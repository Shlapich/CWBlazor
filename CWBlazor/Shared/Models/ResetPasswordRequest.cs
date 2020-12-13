using System.ComponentModel.DataAnnotations;

namespace CWBlazor.Shared.Models
{
    /// <summary>
    /// Reset password request model.
    /// </summary>
    public class ResetPasswordRequest
    {
        /// <summary>
        /// User email.
        /// </summary>
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Refresh password token.
        /// </summary>
        [Required]
        public string Token { get; set; }

        /// <summary>
        /// New password.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Confirm new password.
        /// </summary>
        [Required]
        [Compare("Password", ErrorMessage = "Confirm password should match Password")]
        public string ConfirmPassword { get; set; }
    }
}
