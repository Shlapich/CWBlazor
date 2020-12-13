using System;
using System.Linq;
using System.Threading.Tasks;
using CWBlazor.Domain;
using CWBlazor.Domain.Entities;
using CWBlazor.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CWBlazor.Server.Repositories
{
    public class JwtTokensRepository : IJwtTokensRepository
    {
        private readonly CWServerDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtTokensRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="CWServerDbContext"/>.</param>
        public JwtTokensRepository(CWServerDbContext context)
        {
            this.context = context;
        }

        /// <inheritdoc />
        public async Task SaveNewTokenAsync(RefreshToken refreshToken)
        {
            var existingTokens = context.RefreshTokens.Where(t => t.UserId == refreshToken.UserId);
            context.RefreshTokens.RemoveRange(existingTokens);
            await context.RefreshTokens.AddAsync(refreshToken);
            await context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<RefreshToken> ReadAsync(string id)
        {
            return await context.RefreshTokens
                .AsNoTracking()
                .Include(c => c.User)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        /// <inheritdoc />
        public async Task<bool> IsAlreadyLogged(string userId)
        {
            return await context.RefreshTokens
                .AsNoTracking()
                .AnyAsync(token => token.UserId == userId && DateTime.UtcNow > token.ExpiryDate);
        }
    }
}