using System.Threading.Tasks;
using CWBlazor.Server.DTOs.Account;
using CWBlazor.Shared.Models;

namespace CWBlazor.Server.Modules.Account
{
    /// <summary>
    /// User Identity service.
    /// </summary>
    public interface IIdentityService
    {
        /// <summary>
        /// Register user asynchronous.
        /// </summary>
        /// <param name="request">The <see cref="UserRegistrationRequest"/>.</param>
        /// <returns>The <see cref="AuthenticationResultDto"/>.</returns>
        Task<AuthenticationResultDto> RegisterAsync(UserRegistrationRequest request);

        /// <summary>
        /// Login user asynchronous.
        /// </summary>
        /// <param name="request">The <see cref="UserLoginRequest"/>.</param>
        /// <returns>The <see cref="AuthenticationResultDto"/>.</returns>
        Task<AuthenticationResultDto> LoginAsync(UserLoginRequest request);

        /// <summary>
        /// Change password using old password.
        /// </summary>
        /// <param name="userId">User Id.</param>
        /// <param name="request">The <see cref="ChangePasswordRequest"/>.</param>
        /// <returns>The <see cref="AuthenticationResultDto"/>.</returns>
        Task<AuthenticationResultDto> ChangePasswordAsync(string userId, ChangePasswordRequest request);
    }
}