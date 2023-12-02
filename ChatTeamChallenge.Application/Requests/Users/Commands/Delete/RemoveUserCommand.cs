using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Contracts.Enums;
using ChatTeamChallenge.Domain.Apartments;
using ChatTeamChallenge.Domain.Core.Primities.Result;

namespace ChatTeamChallenge.Application.Requests.Users.Commands.Delete;

public sealed record RemoveUserCommand(
        User User)
    : ICommand<Result<int>>
{
    public int Id { get; set; } = User.Id;
    public DateTime CreatedAt { get; set; } = User.CreatedAt;
    public DateTime? UpdatedAt { get; set; } = User.UpdatedAt;
    public CreativeRoles Roles { get; set; } = User.Roles;
    public string Email { get; set; } = User.Email;
    public string Password { get; set; } = User.Password;
    public string Username { get; set; } = User.Username;
    public string City { get; set; } = User.City;
    public bool IsRemote { get; set; } = User.IsRemote;
    public string? Description { get; set; } = User.Description;
    public string? InstagramLink { get; set; } = User.InstagramLink;
    public string? DiscordLink { get; set; } = User.DiscordLink;
    public string? TelegramLink { get; set; } = User.TelegramLink;
    public string? SpotifyLink { get; set; } = User.SpotifyLink;
}