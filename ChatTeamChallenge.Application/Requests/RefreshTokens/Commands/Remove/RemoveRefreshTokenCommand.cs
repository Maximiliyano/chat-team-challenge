using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Domain.Apartments;
using ChatTeamChallenge.Domain.Core.Primities.Result;

namespace ChatTeamChallenge.Application.Requests.RefreshTokens.Commands.Remove;

public sealed record RemoveRefreshTokenCommand(RefreshToken RToken)
    : ICommand<Result>
{
    public int Id { get; set; } = RToken.Id;
    public int UserId { get; set; } = RToken.UserId;
    public bool IsActive => DateTime.UtcNow <= RToken.Expires;
    public string Token { get; set; } = RToken.Token;
    public DateTime CreatedAt { get; set; } = RToken.CreatedAt;
    public DateTime? UpdatedAt { get; set; } = RToken.UpdatedAt;
    public User User { get; set; } = RToken.User;
}