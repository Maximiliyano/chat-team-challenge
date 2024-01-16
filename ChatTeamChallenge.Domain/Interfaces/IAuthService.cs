using ChatTeamChallenge.Contracts.Authentication;
using ChatTeamChallenge.Contracts.Enums;
using ChatTeamChallenge.Contracts.Token;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Responses;

namespace ChatTeamChallenge.Domain.Interfaces;

public interface IAuthService
{
    Task<Result<AuthUserResponse>> Authorize(LoginRequest credentials);
    Task<Result<AuthUserResponse>> Register(RegisterRequest registerUser);
    Task<Result<AccessTokenResponse>> RefreshToken(RefreshTokenRequest request);
    Task<Result> RevokeRefreshToken(string refreshToken, int userId);
}