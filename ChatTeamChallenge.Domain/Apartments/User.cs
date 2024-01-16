using ChatTeamChallenge.Contracts.Enums;
using ChatTeamChallenge.Domain.Core.Abstractions;
using ChatTeamChallenge.Domain.Core.Events.User;
using ChatTeamChallenge.Domain.Core.Primities;

namespace ChatTeamChallenge.Domain.Apartments;

public sealed class User : AggregateRoot, IAuditableEntity
{
    public required DateTime CreatedAt { get; set; }
    public required CreativeRoles Roles { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Username { get; set; }
    public required string City { get; set; }
    public required bool IsRemote { get; set; }
    
    public string? Description { get; set; }
    public string? InstagramLink { get; set; }
    public string? DiscordLink { get; set; }
    public string? TelegramLink { get; set; }
    public string? SpotifyLink { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public IEnumerable<ChatMember>? Members { get; set; }

    public static User Create(
        string userName, string email, string passwordHash, string city, bool isRemote,
        CreativeRoles roles,
        string? description = null, string? instagramLink = null, string? discordLink = null, string? telegramLink = null, 
        string? spotifyLink = null, int? id = null)
    {
        var user = new User
        {
            Id = id ?? 0,
            City = city,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null,
            Password = passwordHash,
            Email = email,
            Username = userName,
            IsRemote = isRemote,
            Roles = roles,
            Description = description,
            InstagramLink = instagramLink,
            DiscordLink = discordLink,
            TelegramLink = telegramLink,
            SpotifyLink = spotifyLink
        };

        user.AddDomainEvent(new UserCreatedDomainEvent(user));

        return user;
    }
}