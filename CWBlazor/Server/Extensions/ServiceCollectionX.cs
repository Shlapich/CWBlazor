using System;
using System.Linq;
using CWBlazor.Server.Installers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CWBlazor.Server.Extensions
{
    public static class ServiceCollectionX
    {
        /// <summary>
        /// Search all classes implemented IInstaller interface and execute InstallServices method.
        /// </summary>
        /// <param name="services">Services collection from startup.</param>
        /// <param name="configuration">Configuration.</param>
        /// <param name="environment">Hosting environment.</param>
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            var installers = typeof(Startup).Assembly.ExportedTypes
                .Where(x => typeof(IServiceInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IServiceInstaller>().ToList();

            installers.ForEach(installer => installer.InstallServices(services, configuration, environment));
        }
    }
}