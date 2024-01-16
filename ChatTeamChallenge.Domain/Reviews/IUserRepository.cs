using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Domain.Apartments;

namespace ChatTeamChallenge.Domain.Reviews;

public interface IUserRepository
{
    Task InsertAsync(User user);
    Task<bool> IsUsernameUniqueAsync(string username);
    Task<bool> IsEmailUniqueAsync(string email);
    Task<bool> IsUserExistAsync(int userId);
    Task RemoveAsync(User user);
    Task<User?> ReadByIdAsync(int id);
    Task<PagedList<User>> ReadAllAsync(int page, int pageSize);
    Task UpdateAsync(User user);
    Task<User?> ReadByEmailAsync(string email);
    Task<User?> ReadByNameAsync(string username);
}