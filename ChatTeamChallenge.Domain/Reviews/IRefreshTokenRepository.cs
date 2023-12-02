using ChatTeamChallenge.Domain.Apartments;

namespace ChatTeamChallenge.Domain.Reviews;

public interface IRefreshTokenRepository
{
    Task InsertAsync(RefreshToken refreshToken);
    Task RemoveAsync(RefreshToken refreshToken);
    Task<RefreshToken?> ReadByUserTokenAsync(string refreshToken, int userId);
}