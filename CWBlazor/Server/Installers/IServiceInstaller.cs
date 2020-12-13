using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CWBlazor.Server.Installers
{
    /// <summary>
    /// Interface for adding services to <see cref="IServiceCollection"/>.
    /// </summary>
    public interface IServiceInstaller
    {
        /// <summary>
        /// Add service to IServiceCollection.
        /// </summary>
        /// <param name="services">Services.</param>
        /// <param name="configuration">Configuration.</param>
        /// <param name="environment">Hosting environment.</param>
        void InstallServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment);
    }
}
