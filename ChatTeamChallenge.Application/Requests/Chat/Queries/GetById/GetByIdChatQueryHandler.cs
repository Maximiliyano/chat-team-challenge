using AutoMapper;
using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Domain.Core.Errors;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Models;
using ChatTeamChallenge.Domain.Reviews;

namespace ChatTeamChallenge.Application.Requests.Chat.Queries.GetById;

public sealed class GetByIdChatQueryHandler : IQueryHandler<GetByIdChatQuery, Result<ChatModel>>
{
    private readonly IChatRepository _chatRepository;
    private readonly IMapper _mapper;
    
    public GetByIdChatQueryHandler(IChatRepository chatRepository, IMapper mapper)
    {
        _chatRepository = chatRepository;
        _mapper = mapper;
    }
    
    public async Task<Result<ChatModel>> Handle(GetByIdChatQuery request, CancellationToken cancellationToken)
    {
        var chat = await _chatRepository.ReadByIdAsync(request.Id);
        var chatModel = _mapper.Map<ChatModel>(chat);
        return chatModel is null ? 
            Result.Failure<ChatModel>(DomainErrors.Chat.NotFound) : 
            Result.Success(chatModel);
    }
}