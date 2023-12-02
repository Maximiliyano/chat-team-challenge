using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Models;

namespace ChatTeamChallenge.Application.Requests.ChatMembers.Queries.GetAll;

public sealed class GetAllChatsByUserIdQuery : IQuery<Result<PagedList<ChatMemberModel>>>
{
    public GetAllChatsByUserIdQuery(int page, int pageSize, int? chatId, int? userId)
    {
        Page = page;
        PageSize = pageSize;
        ChatId = chatId;
        UserId = userId;
    }
    
    public int Page { get; }
    public int PageSize { get; }
    public int? ChatId { get; }
    public int? UserId { get; }
}