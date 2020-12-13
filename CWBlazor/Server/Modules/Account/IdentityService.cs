using System.Linq;
using System.Threading.Tasks;
using CWBlazor.Domain.Entities;
using CWBlazor.Server.DTOs.Account;
using CWBlazor.Shared.Models;
using Microsoft.AspNetCore.Identity;

namespace CWBlazor.Server.Modules.Account
{
    /// <inheritdoc />
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<CWUser> userManager;
        private readonly ITokenService tokenService;
        private readonly ISecurityAuditService securityAuditService;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityService"/> class.
        /// </summary>
        /// <param name="userManager">User manager from DI.</param>
        /// <param name="tokenService">Token service.</param>
        /// <param name="securityAuditService">The <see cref="ISecurityAuditService"/>.</param>
        public IdentityService(
            UserManager<CWUser> userManager,
            ITokenService tokenService,
            ISecurityAuditService securityAuditService)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
            this.securityAuditService = securityAuditService;
        }

        /// <inheritdoc />
        public async Task<AuthenticationResultDto> RegisterAsync(UserRegistrationRequest request)
        {
            var existingUser = await userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                var message = "User with this email address already exists";
                await securityAuditService.ProcessFailedRegistrationAsync(request, message);

                return new AuthenticationResultDto { Errors = new[] { message }, };
            }

            var newUser = new CWUser { Email = request.Email, UserName = request.Email };

            var result = await userManager.CreateAsync(newUser, request.Password);
            if (!result.Succeeded)
            {
                await securityAuditService.ProcessFailedRegistrationAsync(request, result);

                return new AuthenticationResultDto { Errors = result.Errors.Select(x => x.Description) };
            }

            return await tokenService.Generate(newUser);
        }

        /// <inheritdoc />
        public async Task<AuthenticationResultDto> LoginAsync(UserLoginRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                var message = "User does not exists";
                await securityAuditService.ProcessFailedLoginAsync(request, message);

                return new AuthenticationResultDto { Errors = new[] { message } };
            }

            var userHasValidPassword = await userManager.CheckPasswordAsync(user, request.Password);
            if (!userHasValidPassword)
            {
                var message = "User/password combination is wrong";
                await securityAuditService.ProcessFailedLoginAsync(request, message);

                return new AuthenticationResultDto { Errors = new[] { message }, };
            }

            if (await tokenService.IsAlreadyLogged(user))
            {
                await securityAuditService.ProcessLoginFromAnotherDevice(request);
            }

            return await tokenService.Generate(user);
        }

        /// <inheritdoc />
        public async Task<AuthenticationResultDto> ChangePasswordAsync(string userId, ChangePasswordRequest request)
        {
            var cwUser = await userManager.FindByIdAsync(userId);
            await securityAuditService.ProcessChangePassword(cwUser.Email);

            var result =
                await userManager.ChangePasswordAsync(cwUser, request.OldPassword, request.Password);

            if (!result.Succeeded)
            {
                return new AuthenticationResultDto { Errors = result.Errors.Select(e => e.Description) };
            }

            return await tokenService.Generate(cwUser);
        }
    }
}