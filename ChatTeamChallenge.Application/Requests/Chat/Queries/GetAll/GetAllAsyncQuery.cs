using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Models;

namespace ChatTeamChallenge.Application.Requests.Chat.Queries.GetAll;

public sealed class GetAllAsyncQuery : IQuery<Result<PagedList<ChatModel>>>
{
    public GetAllAsyncQuery(int page, int pageSize, DateTime? createdAt, bool? isPublic, int? userId)
    {
        UserId = userId;
        IsPublic = isPublic;
        CreatedAt = createdAt;
        Page = page;
        PageSize = pageSize;
    }
    
    public int? UserId { get; }
    public bool? IsPublic { get; }
    public DateTime? CreatedAt { get; }
    public int Page { get; }
    public int PageSize { get; }
}