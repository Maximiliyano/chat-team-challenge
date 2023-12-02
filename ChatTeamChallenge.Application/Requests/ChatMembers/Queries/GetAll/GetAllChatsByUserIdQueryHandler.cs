using AutoMapper;
using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Models;
using ChatTeamChallenge.Domain.Reviews;

namespace ChatTeamChallenge.Application.Requests.ChatMembers.Queries.GetAll;

public sealed class GetAllChatsByUserIdQueryHandler : IQueryHandler<GetAllChatsByUserIdQuery, Result<PagedList<ChatMemberModel>>>
{
    private readonly IChatMemberRepository _chatMemberRepository;
    private readonly IMapper _mapper;

    public GetAllChatsByUserIdQueryHandler(IChatMemberRepository chatMemberRepository, IMapper mapper)
    {
        _chatMemberRepository = chatMemberRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedList<ChatMemberModel>>> Handle(GetAllChatsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _chatMemberRepository.GetAllAsync(request.Page, request.PageSize, request.UserId, request.ChatId);
        
        var mappedPagedListWithChatMemberModel = _mapper.Map<PagedList<ChatMemberModel>>(result);
        return Result.Success(mappedPagedListWithChatMemberModel);
    }
}