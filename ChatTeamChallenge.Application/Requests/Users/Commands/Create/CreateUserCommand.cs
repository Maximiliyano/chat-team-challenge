using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Contracts.Authentication;
using ChatTeamChallenge.Contracts.Enums;
using ChatTeamChallenge.Domain.Core.Primities.Result;

namespace ChatTeamChallenge.Application.Requests.Users.Commands.Create;

public sealed record CreateUserCommand : ICommand<Result<int>>
{
    public CreateUserCommand(RegisterRequest registerRequest)
    {
        Roles = registerRequest.Roles;
        Email = registerRequest.Email;
        Password = registerRequest.Password;
        Username = registerRequest.Username;
        City = registerRequest.City;
        IsRemote = registerRequest.IsRemote;
        Description = registerRequest.Description;
        InstagramLink = registerRequest.InstagramLink;
        DiscordLink = registerRequest.DiscordLink;
        TelegramLink = registerRequest.TelegramLink;
        SpotifyLink = registerRequest.SpotifyLink;
    }
    
    public CreativeRoles Roles { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Username { get; set; }
    public string City { get; set; }
    public bool IsRemote { get; set; }
    public string? Description { get; set; }
    public string? InstagramLink { get; set; }
    public string? DiscordLink { get; set; }
    public string? TelegramLink { get; set; }
    public string? SpotifyLink { get; set; }
}