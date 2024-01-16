using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Contracts.ChatMember;
using ChatTeamChallenge.Domain.Core.Primities.Result;

namespace ChatTeamChallenge.Application.Requests.ChatMembers.Commands.DeleteRange;

public sealed record RemoveRangeUserChatCommand(IEnumerable<ChatMemberRequest> ChatMemberRequests) : ICommand<Result>
{
    public IEnumerable<ChatMemberRequest> ChatMemberRequests { get; } = ChatMemberRequests;
}