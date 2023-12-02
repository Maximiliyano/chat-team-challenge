using ChatTeamChallenge.Contracts.Authentication;

namespace ChatTeamChallenge.Domain.Responses;

public sealed class AccessTokenResponse
{
    public AccessToken AccessToken { get; }
    public string RefreshToken { get; }

    public AccessTokenResponse (AccessToken accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}