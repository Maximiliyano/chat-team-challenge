using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Models;

namespace ChatTeamChallenge.Application.Requests.ChatMembers.Queries.GetDetails;

public sealed class GetDetailsToChatCommand : IQuery<Result<ChatMemberModel>>
{
    public GetDetailsToChatCommand(int userId, int chatId)
    {
        UserId = userId;
        ChatId = chatId;
    }
    
    public int UserId { get; }
    public int ChatId { get; }
}