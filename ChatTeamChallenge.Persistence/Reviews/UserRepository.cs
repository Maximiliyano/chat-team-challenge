using ChatTeamChallenge.Application.Core.Abstractions.Data;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Domain.Apartments;
using ChatTeamChallenge.Domain.Reviews;
using Microsoft.EntityFrameworkCore;

namespace ChatTeamChallenge.Persistence.Reviews;

public sealed class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(IDbContext context) : base(context)
    {
    }

    public async Task<bool> IsUsernameUniqueAsync(string username)
    {
        return !await DbContext.Set<User>().AnyAsync(u => u.Username == username);
    }

    public async Task<bool> IsEmailUniqueAsync(string email)
    {
        return !await DbContext.Set<User>().AnyAsync(u => u.Email == email);
    }

    public async Task<bool> IsUserExistAsync(int userId)
    {
        return await DbContext.Set<User>().AnyAsync(u => u.Id == userId);
    }

    public async Task<PagedList<User>> ReadAllAsync(int page, int pageSize)
    {
        var userListQuery = DbContext.Set<User>()
            .AsNoTracking()
            .AsQueryable();

        return await PagedList<User>.CreateAsync(userListQuery, page, pageSize);
    }

    public async Task UpdateAsync(User user) =>
        await DbContext.Set<User>()
            .Where(u => u.Id == user.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.UpdatedAt, DateTime.UtcNow)
                .SetProperty(u => u.IsRemote, user.IsRemote)
                .SetProperty(u => u.Username, user.Username)
                .SetProperty(u => u.City, user.City)
                .SetProperty(u => u.Roles, user.Roles)
                .SetProperty(u => u.Description, user.Description)
                .SetProperty(u => u.InstagramLink, user.InstagramLink)
                .SetProperty(u => u.DiscordLink, user.DiscordLink)
                .SetProperty(u => u.TelegramLink, user.TelegramLink)
                .SetProperty(u => u.SpotifyLink, user.SpotifyLink));
    
    public async Task<User?> ReadByEmailAsync(string email) =>
        await DbContext.Set<User>()
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User?> ReadByNameAsync(string username) =>
        await DbContext.Set<User>()
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Username == username);
}