using ChatTeamChallenge.Application.Core.Abstractions.Data;
using ChatTeamChallenge.Domain.Apartments;
using ChatTeamChallenge.Domain.Reviews;
using Microsoft.EntityFrameworkCore;

namespace ChatTeamChallenge.Persistence.Reviews;

public sealed class RefreshTokenRepository : RepositoryBase<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(IDbContext context) : base(context)
    {
    }

    public async Task<RefreshToken?> ReadByUserTokenAsync(string refreshToken, int userId) =>
        await DbContext.Set<RefreshToken>()
            .FirstOrDefaultAsync(t => t.Token == refreshToken && t.UserId == userId);
}