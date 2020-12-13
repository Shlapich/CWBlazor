using CWBlazor.Domain;
using CWBlazor.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CWBlazor.Server.Installers
{
    /// <summary>
    /// Add Db context and identity to IServiceCollection.
    /// </summary>
    public class DbInstall : IServiceInstaller
    {
        /// <summary>
        /// Add Db context and identity to IServiceCollection.
        /// </summary>
        /// <param name="services">Services.</param>
        /// <param name="configuration">Configuration.</param>
        /// <param name="environment">Hosting environment.</param>
        public void InstallServices(
            IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            services.AddDbContext<CWServerDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString(nameof(CWServerDbContext))));
            services.AddIdentity<CWUser, IdentityRole>()
                .AddEntityFrameworkStores<CWServerDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}