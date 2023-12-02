using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Models;
using ChatTeamChallenge.Domain.Reviews;

namespace ChatTeamChallenge.Application.Requests.Chat.Queries.GetAllAsync;

public sealed class GetAllAsyncQueryHandler : IQueryHandler<GetAllAsyncQuery, Result<PagedList<ChatModel>>>
{
    private readonly IChatRepository _chatRepository;
    
    public GetAllAsyncQueryHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }
    
    public async Task<Result<PagedList<ChatModel>>> Handle(GetAllAsyncQuery request, CancellationToken cancellationToken)
    {
        var pagedListChats = await _chatRepository.ReadByAllAsync(request.Page, request.PageSize, request.CreatedAt, request.IsPublic, request.UserId);
        return Result.Success(pagedListChats);
    }
}