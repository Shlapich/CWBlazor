using Microsoft.EntityFrameworkCore;

namespace CWBlazor.Domain
{
    public class DbContextConfigurator
    {
        public void Configure(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new RefreshTokensConfiguration());
            builder.ApplyConfiguration(new MessagesConfiguration());
        }
    }
}