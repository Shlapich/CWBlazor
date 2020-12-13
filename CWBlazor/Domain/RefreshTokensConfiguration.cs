using CWBlazor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWBlazor.Domain
{
    public class RefreshTokensConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(t => t.Id);
            builder
                .HasOne(u => u.User)
                .WithMany(a => a.RefreshTokens)
                .HasForeignKey(u => u.UserId);
        }
    }
}