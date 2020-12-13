using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CWBlazor.Domain
{
    public class DbContextFactory: IDesignTimeDbContextFactory<CWServerDbContext>
    {
        /// <summary>
        /// Added as a link and configured to copy on build to output directory.
        /// See CWServer.Domain.csproj for details.
        /// </summary>
        private const string ConfigurationFile = "appsettings.json";

        public CWServerDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(ConfigurationFile)
                .AddEnvironmentVariables()
                .Build();
            var connectionString = configuration.GetConnectionString(nameof(CWServerDbContext));
            var dbContextOptions = new DbContextOptionsBuilder<CWServerDbContext>().UseSqlServer(connectionString);
            var dbContext = new CWServerDbContext(dbContextOptions.Options);

            return dbContext;
        }
    }
}