using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CWBlazor.Server.Installers
{
    public class ControllersInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddControllersWithViews();
        }
    }
}