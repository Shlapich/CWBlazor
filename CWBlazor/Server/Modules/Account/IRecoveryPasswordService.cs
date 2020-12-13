using System.Threading.Tasks;
using CWBlazor.Server.DTOs.Account;
using CWBlazor.Shared.Models;

namespace CWBlazor.Server.Modules.Account
{
    /// <summary>
    /// Password recovery service.
    /// </summary>
    public interface IRecoveryPasswordService
    {
        /// <summary>
        /// Send Recover password message to email.
        /// </summary>
        /// <param name="email">User email.</param>
        /// <returns>The <see cref="AuthenticationResultDto"/>.</returns>
        Task<AuthenticationResultDto> RecoverPasswordAsync(string email);

        /// <summary>
        /// Reset password asynchronous.
        /// </summary>
        /// <param name="resetPasswordRequest">Reset password request.</param>
        /// <returns>The <see cref="AuthenticationResultDto"/>.</returns>
        Task<AuthenticationResultDto> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest);
    }
}