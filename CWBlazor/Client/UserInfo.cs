using CWBlazor.Shared.Models;

namespace CWBlazor.Client
{
    public class UserInfo : AuthSuccessResponse
    {
        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }
    }
}