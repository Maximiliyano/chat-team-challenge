using ChatTeamChallenge.Contracts.Enums;
using ChatTeamChallenge.Domain.Core.Primities;

namespace ChatTeamChallenge.Domain.Models;

public sealed class UserModel : BaseModel
{
    public required string Email { get; set; }
    public required bool IsRemote { get; set; }
    public required string Username { get; set; }
    public required string City { get; set; }
    public required CreativeRoles Roles { get; set; }
    
    public string? Description { get; set; }
    public string? InstagramLink { get; set; }
    public string? DiscordLink { get; set; }
    public string? TelegramLink { get; set; }
    public string? SpotifyLink { get; set; }
    // TODO add user ability have a lots of chat

    public static UserModel Create(
        int id,
        string userName, string email, string city, bool isRemote,
        CreativeRoles roles, DateTime createdAt, DateTime? updatedAt,
        string? description, string? instagramLink, string? discordLink, string? telegramLink, 
        string? spotifyLink) =>
            new()
            {
                Id = id,
                Email = email,
                IsRemote = isRemote,
                Username = userName,
                City = city,
                Roles = roles,
                CreatedAt = createdAt,
                UpdatedAt = updatedAt,
                Description = description,
                DiscordLink = discordLink,
                InstagramLink = instagramLink,
                SpotifyLink = spotifyLink,
                TelegramLink = telegramLink
            };
}