using System.Threading.Tasks;
using AutoMapper;
using CWBlazor.Server.Modules.Account;
using CWBlazor.Shared;
using CWBlazor.Shared.Contracts.Wrappers;
using CWBlazor.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CWBlazor.Server.Controllers
{
    using static Constants.Routes;

    /// <summary>
    /// Uses to Authenticate users, refresh tokens and reset password.
    /// </summary>
    [ApiController]
    [Route(ApiController)]
    public class AccountController : DefaultController
    {
        private readonly IIdentityService identityService;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;
        private readonly IRecoveryPasswordService recoveryPasswordService;

        public AccountController(
            ITokenService tokenService,
            IIdentityService identityService,
            IMapper mapper,
            IRecoveryPasswordService recoveryPasswordService)
        {
            this.tokenService = tokenService;
            this.identityService = identityService;
            this.mapper = mapper;
            this.recoveryPasswordService = recoveryPasswordService;
        }

        /// <summary>
        /// User registration action.
        /// </summary>
        /// <param name="request">User register request.</param>
        /// <returns>The <see cref="AuthSuccessResponse"/>.</returns>
        [HttpPost(nameof(Register))]
        [ProducesResponseType(typeof(AuthSuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            var authResponse = await identityService.RegisterAsync(request);

            if (authResponse.Success)
            {
                return Ok(mapper.Map<AuthSuccessResponse>(authResponse));
            }

            return StatusCode(StatusCodes.Status400BadRequest, authResponse.Errors);
        }

        /// <summary>
        /// User login action.
        /// </summary>
        /// <param name="request">User login request model.</param>
        /// <returns>The <see cref="AuthSuccessResponse"/>.</returns>
        [HttpPost(nameof(Login))]
        [ProducesResponseType(typeof(AuthSuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var authResponse = await identityService.LoginAsync(request);

            if (authResponse.Success)
            {
                return Ok(mapper.Map<AuthSuccessResponse>(authResponse));
            }

            return StatusCode(StatusCodes.Status400BadRequest, authResponse.Errors);
        }

        /// <summary>
        /// User refresh jwt action.
        /// </summary>
        /// <param name="request">Refresh token request.</param>
        /// <returns>The <see cref="OkResult"/> contains <see cref="AuthSuccessResponse"/> json.</returns>
        [HttpPost(nameof(TokenRefresh))]
        [ProducesResponseType(typeof(AuthSuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> TokenRefresh([FromBody] RefreshTokenRequest request)
        {
            var authResponse = await tokenService.RefreshTokenAsync(request.RefreshToken);

            return authResponse.Success
                ? Ok(mapper.Map<AuthSuccessResponse>(authResponse))
                : StatusCode(StatusCodes.Status400BadRequest, authResponse.Errors);
        }

        /// <summary>
        /// Recover password action.
        /// </summary>
        /// <param name="request">Recovery password request.</param>
        /// <returns>The <see cref="OkResult"/>.</returns>
        [HttpPost(nameof(RecoverPassword))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RecoverPassword([FromBody] RecoverPasswordRequest request)
        {
            var authResponse = await recoveryPasswordService.RecoverPasswordAsync(request.Email);

            return authResponse.Success
                ? (IActionResult)Ok()
                : StatusCode(StatusCodes.Status400BadRequest, authResponse.Errors);
        }

        /// <summary>
        /// Reset password action.
        /// </summary>
        /// <param name="request">Reset password request.</param>
        /// <returns>The <see cref="OkResult"/>.</returns>
        [HttpPost(nameof(ResetPassword))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var authResponse = await recoveryPasswordService.ResetPasswordAsync(request);
            return authResponse.Success
                ? (IActionResult)Ok(mapper.Map<AuthSuccessResponse>(authResponse))
                : StatusCode(StatusCodes.Status400BadRequest, authResponse.Errors);
        }

        /// <summary>
        /// Change password.
        /// </summary>
        /// <param name="request">Change password request.</param>
        /// <returns>The <see cref="OkResult"/>.</returns>
        [HttpPost(nameof(ChangePassword))]
        [ProducesResponseType(typeof(AuthSuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var authResponse =
                await identityService.ChangePasswordAsync(CurrentUserId, request);
            return authResponse.Success
                ? (IActionResult)Ok(mapper.Map<AuthSuccessResponse>(authResponse))
                : StatusCode(StatusCodes.Status400BadRequest, authResponse.Errors);
        }
    }
}