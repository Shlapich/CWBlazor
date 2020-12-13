using System.Threading.Tasks;
using CWBlazor.Domain.Entities;

namespace CWBlazor.Server.Repositories.Interfaces
{
    /// <summary>
    /// Provides method(s) to work with <see cref="RefreshToken"/> entity.
    /// </summary>
    public interface IJwtTokensRepository
    {
        /// <summary>
        /// Saves <paramref name="refreshToken"/> into database.
        /// It also removes all existing tokens of the related user.
        /// </summary>
        /// <param name="refreshToken">The <see cref="RefreshToken"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        Task SaveNewTokenAsync(RefreshToken refreshToken);

        /// <summary>
        /// Read <see cref="RefreshToken"/> by <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Id of the <see cref="RefreshToken"/>.</param>
        /// <returns>The <see cref="RefreshToken"/>.</returns>
        Task<RefreshToken> ReadAsync(string id);

        /// <summary>
        /// Check if user is already logged in.
        /// </summary>
        /// <param name="userId">The <see cref="CWUser"/> id.</param>
        /// <returns><c>true</c> if already logged otherwise <c>false</c>.</returns>
        Task<bool> IsAlreadyLogged(string userId);
    }
}