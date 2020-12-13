using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using CWBlazor.Domain.Entities;
using CWBlazor.Server.Core.Email;
using CWBlazor.Server.DTOs.Account;
using CWBlazor.Shared;
using CWBlazor.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CWBlazor.Server.Modules.Account
{
    public class RecoveryPasswordService : IRecoveryPasswordService
    {
        private readonly IUrlHelper urlHelper;
        private readonly UserManager<CWUser> userManager;
        private readonly IEmailService emailService;
        private readonly IConfiguration configuration;
        private readonly ITokenService tokenService;
        private readonly ISecurityAuditService securityAuditService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecoveryPasswordService"/> class.
        /// </summary>
        /// <param name="urlHelper">UrlHelper from DI.</param>
        /// <param name="userManager">User manager from DI.</param>
        /// <param name="emailService">Email service from DI.</param>
        /// <param name="configuration">Configuration.</param>
        /// <param name="tokenService">Token service.</param>
        /// <param name="securityAuditService">The <see cref="ISecurityAuditService"/>.</param>
        public RecoveryPasswordService(
            IUrlHelper urlHelper,
            UserManager<CWUser> userManager,
            IEmailService emailService,
            IConfiguration configuration,
            ITokenService tokenService,
            ISecurityAuditService securityAuditService)
        {
            this.urlHelper = urlHelper;
            this.userManager = userManager;
            this.emailService = emailService;
            this.configuration = configuration;
            this.tokenService = tokenService;
            this.securityAuditService = securityAuditService;
        }

        /// <inheritdoc />
        public async Task<AuthenticationResultDto> RecoverPasswordAsync(string email)
        {
            await securityAuditService.ProcessForgotPassword(email);

            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new AuthenticationResultDto { Errors = new[] { "User does not exist" } };
            }

            var pwdResetLink = await GetPasswordResetLink(user);

            using var msg = GetEmailMessage(user, pwdResetLink);

            emailService.SendEmail(msg);

            return new AuthenticationResultDto { Success = true };
        }

        /// <inheritdoc />
        public async Task<AuthenticationResultDto> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest)
        {
            await securityAuditService.ProcessResetPassword(resetPasswordRequest.Email);

            if (resetPasswordRequest.Password != resetPasswordRequest.ConfirmPassword)
            {
                return new AuthenticationResultDto
                {
                    Errors = new[] { "Passwords don't match" },
                };
            }

            var user = await userManager.FindByEmailAsync(resetPasswordRequest.Email);
            if (user == null)
            {
                return new AuthenticationResultDto { Errors = new[] { "User does not exist" } };
            }

            var result = await userManager.ResetPasswordAsync(user, resetPasswordRequest.Token, resetPasswordRequest.Password);

            if (result.Succeeded)
            {
                return await tokenService.Generate(user);
            }

            var r = new AuthenticationResultDto { Errors = result.Errors.Select(x => x.Description), Success = false };
            return r;
        }

        private MailMessage GetEmailMessage(CWUser user, string pwdResetLink)
        {
            var emailSettings = new EmailSettings();
            configuration.Bind(nameof(EmailSettings), emailSettings);

            var messageBody =
                $"Click here to reset your password: <a href=\"{pwdResetLink}\">link</a>";
            var msg = new MailMessage
            {
                From = new MailAddress(emailSettings.FromEmail),
                Subject = "Password reset",
                IsBodyHtml = true,
                Body = messageBody,
            };

            msg.To.Add(new MailAddress(user.Email));

            return msg;
        }

        private async Task<string> GetPasswordResetLink(CWUser user)
        {
            // generate password token
            var pwdResetToken = await userManager.GeneratePasswordResetTokenAsync(user);
            var value = new { email = user.Email, token = pwdResetToken };
            return urlHelper.RouteUrl(
                Constants.RouteNames.ResetPassword,
                value,
                urlHelper.ActionContext.HttpContext.Request.Scheme);
        }
    }
}