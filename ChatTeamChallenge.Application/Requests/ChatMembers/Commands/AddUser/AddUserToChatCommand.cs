using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Domain.Core.Primities.Result;

namespace ChatTeamChallenge.Application.Requests.ChatMembers.Commands.AddUser;

public sealed record AddUserToChatCommand(int UserId, int ChatId) : ICommand<Result<int>>
{
    public int UserId { get; set; } = UserId;
    public int ChatId { get; set; } = ChatId;
}