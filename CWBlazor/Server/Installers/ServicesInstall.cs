using AutoMapper;
using CWBlazor.Server.Core.Email;
using CWBlazor.Server.Modules.Account;
using CWBlazor.Server.Repositories;
using CWBlazor.Server.Repositories.Interfaces;
using CWBlazor.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CWBlazor.Server.Installers
{
    /// <summary>
    /// Add other services to IServiceCollection.
    /// </summary>
    public class ServicesInstall : IServiceInstaller
    {
        /// <summary>
        /// Add other services to IServiceCollection.
        /// </summary>
        /// <param name="services">Services.</param>
        /// <param name="configuration">Configuration.</param>
        /// <param name="environment">Hosting environment.</param>
        public void InstallServices(
            IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            services.AddCors(
                options =>
                {
                    // Create named CORS policies here which you can consume using application.UseCors("PolicyName")
                    // or a [EnableCors("PolicyName")] attribute on your controller or action.
                    options.AddPolicy(
                        Constants.CorsPolicyName.AllowAny,
                        x => x
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
                });
            services.AddAutoMapper(c => c.AddProfile<AutoMapperProfile>(), typeof(Startup));
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IRecoveryPasswordService, RecoveryPasswordService>();
            services.AddScoped<IJwtTokensRepository, JwtTokensRepository>();
            services.AddScoped<ISecurityAuditRepository, SecurityAuditRepository>();
            services.AddScoped<ISecurityAuditService, SecurityAuditService>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddScoped(x =>
            {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
            });
        }
    }
}
