using System.Threading.Tasks;
using CWBlazor.Domain;
using CWBlazor.Domain.Entities;
using CWBlazor.Server.Repositories.Interfaces;

namespace CWBlazor.Server.Repositories
{
    /// <inheritdoc />
    public class SecurityAuditRepository : ISecurityAuditRepository
    {
        private readonly CWServerDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityAuditRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="CWServerDbContext"/>.</param>
        public SecurityAuditRepository(CWServerDbContext context)
        {
            this.context = context;
        }

        /// <inheritdoc />
        public async Task LogAsync(SecurityAudit log)
        {
            context.SecurityAudit.Add(log);
            await context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public void Log(SecurityAudit log)
        {
            context.SecurityAudit.Add(log);
            context.SaveChanges();
        }
    }
}