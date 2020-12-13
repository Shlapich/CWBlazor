using System.Threading.Tasks;
using CWBlazor.Shared.Models;
using Microsoft.AspNetCore.Identity;

namespace CWBlazor.Server.Modules.Account
{
    /// <summary>
    /// Security Audit Service.
    /// </summary>
    public interface ISecurityAuditService
    {
        /// <summary>
        /// Process failed registration.
        /// </summary>
        /// <param name="request">The <see cref="UserRegistrationRequest"/>.</param>
        /// <param name="message">Message.</param>
        Task ProcessFailedRegistrationAsync(UserRegistrationRequest request, string message);

        /// <summary>
        /// Process failed registration.
        /// </summary>
        /// <param name="request">The <see cref="UserRegistrationRequest"/>.</param>
        /// <param name="result">The <see cref="IdentityResult"/>.</param>
        Task ProcessFailedRegistrationAsync(UserRegistrationRequest request, IdentityResult result);

        /// <summary>
        /// Process failed login.
        /// </summary>
        /// <param name="request">The <see cref="UserLoginRequest"/>.</param>
        /// <param name="message">Message.</param>
        Task ProcessFailedLoginAsync(UserLoginRequest request, string message);

        /// <summary>
        /// Process login from another device.
        /// </summary>
        /// <param name="request">The <see cref="UserLoginRequest"/>.</param>
        Task ProcessLoginFromAnotherDevice(UserLoginRequest request);

        /// <summary>
        /// Process change password.
        /// </summary>
        /// <param name="email">Email.</param>
        Task ProcessChangePassword(string email);

        /// <summary>
        /// Process forgot password.
        /// </summary>
        /// <param name="email">Email.</param>
        Task ProcessForgotPassword(string email);

        /// <summary>
        /// Process reset password.
        /// </summary>
        /// <param name="email">Email.</param>
        Task ProcessResetPassword(string email);
    }
}