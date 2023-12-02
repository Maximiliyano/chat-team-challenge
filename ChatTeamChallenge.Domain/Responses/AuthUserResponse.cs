using ChatTeamChallenge.Domain.Models;

namespace ChatTeamChallenge.Domain.Responses;

public sealed class AuthUserResponse
{
    public required UserModel User { get; set; }
    public required AccessTokenResponse Token { get; set; }
}