using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Contracts.Enums;
using ChatTeamChallenge.Domain.Core.Primities.Result;

namespace ChatTeamChallenge.Application.Requests.ChatMembers.Commands.AddUserToRequiredChats;

public sealed record AddUserToRequiredChatsCommand(int UserId, CreativeRoles Roles, bool GlobalChat) : ICommand<Result>
{
    public int UserId { get; } = UserId;

    public CreativeRoles Roles { get; } = Roles;

    public bool GlobalChat { get; } = GlobalChat;
}