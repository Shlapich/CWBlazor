﻿using System.Collections.Generic;

 namespace CWBlazor.Server.DTOs.Account
{
    public class AuthenticationResultDto
    {
        /// <summary>
        /// JWT.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Refresh token.
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Is Success.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Errors array.
        /// </summary>
        public IEnumerable<string> Errors { get; set; }
    }
}