using AutoMapper;
using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Application.Requests.Chat.Queries.GetById;
using ChatTeamChallenge.Domain.Core.Errors;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Models;
using ChatTeamChallenge.Domain.Reviews;

namespace ChatTeamChallenge.Application.Requests.Chat.Queries.GetByTopic;

public sealed class GetByTopicChatQueryHandler : IQueryHandler<GetByTopicChatQuery, Result<ChatModel>>
{
    private readonly IChatRepository _chatRepository;
    private readonly IMapper _mapper;
    
    public GetByTopicChatQueryHandler(IChatRepository chatRepository, IMapper mapper)
    {
        _chatRepository = chatRepository;
        _mapper = mapper;
    }
    
    public async Task<Result<ChatModel>> Handle(GetByTopicChatQuery request, CancellationToken cancellationToken)
    {
        var chat = await _chatRepository.ReadByTopicAsync(request.Topic);
        var chatModel = _mapper.Map<ChatModel>(chat);
        return chatModel is null ? 
            Result.Failure<ChatModel>(DomainErrors.Chat.NotFound) : 
            Result.Success(chatModel);
    }
}