using System.Threading.Tasks;
using CWBlazor.Domain.Entities;
using CWBlazor.Server.DTOs.Account;

namespace CWBlazor.Server.Modules.Account
{
    /// <summary>
    /// Token generation and refresh service.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Asynchronously generates Auth token for user.
        /// </summary>
        /// <param name="user">The <see cref="CWUser"/>.</param>
        /// <returns>AuthenticationResultDto.</returns>
        Task<AuthenticationResultDto> Generate(CWUser user);

        /// <summary>
        /// Check if user is already logged in.
        /// </summary>
        /// <param name="user">The <see cref="CWUser"/>.</param>
        /// <returns><c>true</c> if already logged otherwise <c>false</c>.</returns>
        Task<bool> IsAlreadyLogged(CWUser user);

        /// <summary>
        /// Asynchronously generates Auth token for the <paramref name="refreshToken"/>.
        /// </summary>
        /// <param name="refreshToken">JWT refresh token.</param>
        /// <returns>AuthenticationResultDto.</returns>
        Task<AuthenticationResultDto> Generate(RefreshToken refreshToken);

        /// <summary>
        /// Asynchronously generates <see cref="AuthenticationResultDto"/> for the <paramref name="refreshToken"/>.
        /// </summary>
        /// <param name="refreshToken">JWT refresh token.</param>
        /// <returns>The <see cref="AuthenticationResultDto"/>.</returns>
        Task<AuthenticationResultDto> RefreshTokenAsync(string refreshToken);
    }
}