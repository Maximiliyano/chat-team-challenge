using ChatTeamChallenge.Contracts.Authentication;
using ChatTeamChallenge.Contracts.Enums;
using ChatTeamChallenge.Domain.Core.Primities.Result;

namespace ChatTeamChallenge.Domain.Interfaces;

public interface IJwtFactory
{
    Task<AccessToken> GenerateAccessToken(int userId, string name, string email, CreativeRoles roles);
    string GenerateRefreshToken();
    Result<int> GetUserIdFromToken(string accessToken);
}