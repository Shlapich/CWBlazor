using System.ComponentModel.DataAnnotations;

namespace CWBlazor.Shared.Models
{
    /// <summary>
    /// Change password request.
    /// </summary>
    public class ChangePasswordRequest
    {
        /// <summary>
        /// Old password.
        /// </summary>
        [Required]
        public string OldPassword { get; set; }

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
