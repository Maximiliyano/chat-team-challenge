using ChatTeamChallenge.Domain.Apartments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatTeamChallenge.Persistence.Configurations;

public sealed class MessageFileConfiguration : IEntityTypeConfiguration<MessageFile>
{
    public void Configure(EntityTypeBuilder<MessageFile> builder)
    {
        builder.ToTable("MessageFiles");

        builder
            .HasOne(c => c.Message)
            .WithMany(c => c.Files)
            .HasForeignKey(c => c.MessageId);
    }
}