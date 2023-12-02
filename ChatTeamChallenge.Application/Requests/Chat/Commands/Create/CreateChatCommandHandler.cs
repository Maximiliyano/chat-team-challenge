using ChatTeamChallenge.Application.Core.Abstractions.Data;
using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Domain.Core.Errors;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Reviews;

namespace ChatTeamChallenge.Application.Requests.Chat.Commands.Create;

public sealed class CreateChatCommandHandler : ICommandHandler<CreateChatCommand, Result<int>>
{
    private readonly IChatRepository _chatRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateChatCommandHandler(IChatRepository chatRepository, IUnitOfWork unitOfWork)
    {
        _chatRepository = chatRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<int>> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        var chat = await _chatRepository.IsTopicUniqueAsync(request.Topic);
            
        if (chat is false)
        {
            return Result.Failure<int>(DomainErrors.Chat.TopicIsNotUnique(request.Topic));
        }
        
        var chatInstance = Domain.Apartments.Chat.Create(
            request.Topic,
            request.IsPublic,
            DateTime.UtcNow);
        
        await _chatRepository.InsertAsync(chatInstance);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success(chatInstance.Id);
    }
}