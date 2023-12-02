using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Contracts.Enums;
using ChatTeamChallenge.Domain.Core.Primities.Result;

namespace ChatTeamChallenge.Application.Requests.Users.Commands.Update;

public sealed record UpdateUserCommand : ICommand<Result<int>>
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public CreativeRoles Roles { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Username { get; set; }
    public required string City { get; set; }
    public bool IsRemote { get; set; }
    public string? Description { get; set; }
    public string? InstagramLink { get; set; }
    public string? DiscordLink { get; set; }
    public string? TelegramLink { get; set; }
    public string? SpotifyLink { get; set; }
}