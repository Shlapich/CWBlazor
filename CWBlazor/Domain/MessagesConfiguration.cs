using CWBlazor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWBlazor.Domain
{
    public class MessagesConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(t => t.Id);
            builder
                .HasOne(message => message.Sender)
                .WithMany(sender => sender.SendMessages)
                .HasForeignKey(message => message.SenderId);
        }
    }
}