using ChatTeamChallenge.Contracts.Authentication;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Contracts.Enums;
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
    Task<bool> IsUserExistAsync(int userId);
    Task<Result> ChangePasswordAsync(int userId, string newPassword);
    Task<Result> ChangeRoleAsync(int userId, CreativeRoles role);
    Task<Result> UpdateAsync(UpdateUserRequest updateUserRequest);
    Task<Result> DeleteAsync(int userId);
}