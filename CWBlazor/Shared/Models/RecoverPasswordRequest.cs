using System.ComponentModel.DataAnnotations;

namespace CWBlazor.Shared.Models
{
    /// <summary>
    /// Recover user password request.
    /// </summary>
    public class RecoverPasswordRequest
    {
        /// <summary>
        /// User email.
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }
    }
}
