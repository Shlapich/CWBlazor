using System.ComponentModel.DataAnnotations;

namespace CWBlazor.Shared.Models
{
    /// <summary>
    /// Register User request model.
    /// </summary>
    public class UserRegistrationRequest
    {
        /// <summary>
        /// User email.
        /// </summary>
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// User password.
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
