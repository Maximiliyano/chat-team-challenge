using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Contracts.Enums;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Models;

namespace ChatTeamChallenge.Application.Requests.Users.Commands.ChangeRole;

public sealed record ChangeRoleUserCommand(int UserId, CreativeRoles Roles) : ICommand<Result<UserModel>>
{
    public int UserId { get; } = UserId;

    public CreativeRoles Roles { get; } = Roles;
}