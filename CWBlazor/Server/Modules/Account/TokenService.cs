using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CWBlazor.Domain.Entities;
using CWBlazor.Server.DTOs.Account;
using CWBlazor.Server.Extensions;
using CWBlazor.Server.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace CWBlazor.Server.Modules.Account
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions jwtSettings;
        private readonly IJwtTokensRepository repository;
        private readonly JwtSecurityTokenHandler tokenHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenService"/> class.
        /// </summary>
        /// <param name="repository">The <see cref="IJwtTokensRepository"/>.</param>
        /// <param name="jwtSettings">Jwt settings.</param>
        public TokenService(IJwtTokensRepository repository, JwtOptions jwtSettings)
        {
            this.repository = repository;
            this.jwtSettings = jwtSettings;
            tokenHandler = new JwtSecurityTokenHandler();
        }

        /// <inheritdoc />
        public async Task<AuthenticationResultDto> RefreshTokenAsync(string refreshToken)
        {
            var storedRefreshToken = await repository.ReadAsync(refreshToken);
            var error = ValidateRefreshToken(storedRefreshToken);
            if (string.IsNullOrEmpty(error))
            {
                return await Generate(storedRefreshToken);
            }

            return new AuthenticationResultDto
            {
                Success = false,
                Errors = new[] { error },
            };
        }

        /// <inheritdoc />
        public async Task<bool> IsAlreadyLogged(CWUser user)
        {
            return await repository.IsAlreadyLogged(user.Id);
        }

        /// <inheritdoc />
        public async Task<AuthenticationResultDto> Generate(CWUser user)
        {
            var (token, refreshToken) = GenerateTokenPair(user);

            await repository.SaveNewTokenAsync(refreshToken);

            return new AuthenticationResultDto
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Id,
            };
        }

        /// <inheritdoc />
        public async Task<AuthenticationResultDto> Generate(RefreshToken refreshToken)
        {
            var user = refreshToken.User;

            return await Generate(user);
        }

        private (SecurityToken Token, RefreshToken RefreshToken) GenerateTokenPair(CWUser user)
        {
            return (GenerateToken(user), GenerateRefreshToken(user));
        }

        private SecurityToken GenerateToken(CWUser user)
        {
            var tokenDescriptor = GenerateTokenDescriptor(user);
            return tokenHandler.CreateToken(tokenDescriptor);
        }

        private SecurityTokenDescriptor GenerateTokenDescriptor(CWUser user)
        {
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
            var now = DateTime.UtcNow;
            var sumKey = new SymmetricSecurityKey(key);
            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                }),
                Expires = now.AddSeconds(jwtSettings.TokenLifetimeInSeconds),
                SigningCredentials = new SigningCredentials(
                    sumKey, SecurityAlgorithms.HmacSha256Signature),
            };
        }

        private RefreshToken GenerateRefreshToken(CWUser user)
        {
            return new RefreshToken
            {
                UserId = user.Id,
                ExpiryDate = DateTime.UtcNow.AddSeconds(jwtSettings.RefreshTokenLifetimeInSeconds),
            };
        }

        private string ValidateRefreshToken(RefreshToken token)
        {
            if (token == null)
            {
                return "Invalid refresh token";
            }

            return token.IsExpired() ? "Expired refresh token" : null;
        }
    }
}