using System.ComponentModel.DataAnnotations;
using ChatTeamChallenge.Contracts.Enums;

namespace ChatTeamChallenge.Contracts.User;

public abstract class UserRequest
{
    [Required]
    public required bool IsRemote { get; init; }

    [MaxLength(32)]
    [MinLength(3)]
    [Required]
    public required string Username { get; init; }

    [MaxLength(85)]
    [MinLength(1)]
    [Required]
    public required string City { get; init; }

    [Required]
    public required CreativeRoles Roles { get; init; }
    
    public string? Description { get; init; }
    public string? InstagramLink { get; init; }
    public string? DiscordLink { get; init; }
    public string? TelegramLink { get; init; }
    public string? SpotifyLink { get; init; }
}