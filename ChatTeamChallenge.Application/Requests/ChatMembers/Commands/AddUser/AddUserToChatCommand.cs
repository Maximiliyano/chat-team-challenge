using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Contracts.Enums;
using ChatTeamChallenge.Domain.Core.Primities.Result;

namespace ChatTeamChallenge.Application.Requests.ChatMembers.Commands.AddUser;

public sealed record AddUserToChatCommand(int UserId, int ChatId, ChatMemberRoles Role) : ICommand<Result<int>>
{
    public int UserId { get; set; } = UserId;
    public int ChatId { get; set; } = ChatId;
    public ChatMemberRoles Role { get; set; } = Role;
}