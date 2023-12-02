using ChatTeamChallenge.Domain.Apartments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatTeamChallenge.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    // TODO complete
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);
        
        builder.ToTable("Users");
    }
}