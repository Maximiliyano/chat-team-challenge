using ChatTeamChallenge.Domain.Apartments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatTeamChallenge.Persistence.Configurations;

public sealed class ChatMemberConfiguration : IEntityTypeConfiguration<ChatMember>
{
    public void Configure(EntityTypeBuilder<ChatMember> builder)
    {
        builder.ToTable("ChatMembers");

        builder.HasKey(c => c.Id);
            
        builder
            .HasOne(c => c.Chat)
            .WithMany(c => c.Members)
            .HasForeignKey(c => c.ChatId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder
            .HasOne(c => c.User)
            .WithMany(u => u.Members)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}