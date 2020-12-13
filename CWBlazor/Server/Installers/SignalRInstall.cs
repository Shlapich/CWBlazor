using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CWBlazor.Server.Installers
{
    public class SignalRInstall : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            services.AddSignalR();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });
        }
    }
}