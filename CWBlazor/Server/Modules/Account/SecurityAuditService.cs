using System.Threading.Tasks;
using CWBlazor.Domain.Entities;
using CWBlazor.Domain.Enums;
using CWBlazor.Server.Repositories.Interfaces;
using CWBlazor.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace CWBlazor.Server.Modules.Account
{
     /// <inheritdoc />
    public class SecurityAuditService : ISecurityAuditService
    {
        private readonly ISecurityAuditRepository securityAuditRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityAuditService"/> class.
        /// </summary>
        /// <param name="securityAuditRepository">The <see cref="ISecurityAuditRepository"/>.</param>
        public SecurityAuditService(ISecurityAuditRepository securityAuditRepository)
        {
            this.securityAuditRepository = securityAuditRepository;
        }

        /// <inheritdoc />
        public async Task ProcessFailedRegistrationAsync(UserRegistrationRequest request, string message)
        {
            var logMessage = $"{message}: {JsonConvert.SerializeObject(request)}";
            await securityAuditRepository.LogAsync(
                new SecurityAudit
                {
                    Type = SecurityAuditType.RegisterFailure,
                    Details = logMessage,
                });
        }

        /// <inheritdoc />
        public async Task ProcessFailedRegistrationAsync(UserRegistrationRequest request, IdentityResult result)
        {
            var logMessage = $"{string.Join(", ", result.Errors)}: {JsonConvert.SerializeObject(request)}";
            await securityAuditRepository.LogAsync(new SecurityAudit
            {
                Type = SecurityAuditType.RegisterFailure,
                Details = logMessage,
            });
        }

        /// <inheritdoc />
        public async Task ProcessFailedLoginAsync(UserLoginRequest request, string message)
        {
            await securityAuditRepository.LogAsync(new SecurityAudit
            {
                Type = SecurityAuditType.LoginFailure,
                Details = $"{message}: {request.Email}",
            });
        }

        /// <inheritdoc />
        public async Task ProcessLoginFromAnotherDevice(UserLoginRequest request)
        {
            await securityAuditRepository.LogAsync(new SecurityAudit
            {
                Type = SecurityAuditType.AnotherDeviceLogin,
                Details = $"Login from another device: {request.Email}",
            });
        }

        /// <inheritdoc />
        public async Task ProcessChangePassword(string email)
        {
            await securityAuditRepository.LogAsync(new SecurityAudit
            {
                Type = SecurityAuditType.ChangePassword,
                Details = email,
            });
        }

        /// <inheritdoc />
        public async Task ProcessForgotPassword(string email)
        {
            await securityAuditRepository.LogAsync(new SecurityAudit
            {
                Type = SecurityAuditType.ForgotPassword,
                Details = email,
            });
        }

        /// <inheritdoc />
        public async Task ProcessResetPassword(string email)
        {
            await securityAuditRepository.LogAsync(new SecurityAudit
            {
                Type = SecurityAuditType.ResetPassword,
                Details = email,
            });
        }
    }
}