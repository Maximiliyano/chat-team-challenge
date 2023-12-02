using ChatTeamChallenge.Contracts.Authentication;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Contracts.User;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Models;

namespace ChatTeamChallenge.Domain.Interfaces;

public interface IUserService
{
    Task<Result<UserModel>> CreateAsync(RegisterRequest model);
    Task<Result<PagedList<UserModel>>> ReadAllAsync(int page, int pageSize);
    Task<Result<UserModel>> ReadByIdAsync(int userId);
    Task<Result<UserModel>> ReadByEmailAsync(string email);
    Task<Result> ChangePasswordAsync(int userId, string newPassword);
    Task<Result> UpdateAsync(UpdateUserRequest updateUserRequest);
    Task<Result> DeleteAsync(int userId);
}