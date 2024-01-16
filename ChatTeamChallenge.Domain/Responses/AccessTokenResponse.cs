using ChatTeamChallenge.Contracts.Authentication;
using ChatTeamChallenge.Domain.Models;

namespace ChatTeamChallenge.Domain.Responses;

public sealed class AccessTokenResponse
{
    public AccessToken AccessToken { get; }
    public string RefreshToken { get; }
    
    public UserModel? UserModel { get; }

    public AccessTokenResponse (AccessToken accessToken, string refreshToken, UserModel? userModel)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        UserModel = userModel;
    }
}