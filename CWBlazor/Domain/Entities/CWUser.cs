using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace CWBlazor.Domain.Entities
{
    public class CWUser : IdentityUser
    {
        public IEnumerable<Message> ReceivedMessages { get; set; }

        public IEnumerable<Message> SendMessages { get; set; }

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}