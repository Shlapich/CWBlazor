using System.Threading.Tasks;
using CWBlazor.Domain.Entities;

namespace CWBlazor.Server.Repositories.Interfaces
{
    /// <summary>
    /// Provides method(s) to log <see cref="SecurityAudit"/>.
    /// </summary>
    public interface ISecurityAuditRepository
    {
        /// <summary>
        /// Asynchronously log new security audit occurrence.
        /// </summary>
        /// <param name="log">The <see cref="SecurityAudit"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task LogAsync(SecurityAudit log);

        /// <summary>
        /// Log new security audit occurrence.
        /// </summary>
        /// <param name="log">The <see cref="SecurityAudit"/>.</param>
        void Log(SecurityAudit log);
    }
}