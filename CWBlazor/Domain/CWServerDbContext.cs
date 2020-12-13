using CWBlazor.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CWBlazor.Domain
{
    public class CWServerDbContext : IdentityDbContext<CWUser>
    {
        private readonly DbContextConfigurator configurator = new DbContextConfigurator();

        public CWServerDbContext(DbContextOptions<CWServerDbContext> options)
            : base(options)
        {
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<SecurityAudit> SecurityAudit { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            configurator.Configure(builder);
        }
    }
}