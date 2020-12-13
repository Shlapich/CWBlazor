using System;
using CWBlazor.Domain.Entities;

namespace CWBlazor.Server.Extensions
{
    public static class RefreshTokenX
    {
        public static bool IsExpired(this RefreshToken token) => DateTime.UtcNow > token.ExpiryDate;
    }
}