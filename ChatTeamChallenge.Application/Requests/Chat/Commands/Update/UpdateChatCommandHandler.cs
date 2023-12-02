using AutoMapper;
using ChatTeamChallenge.Application.Core.Abstractions.Data;
using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Contracts.Chat;
using ChatTeamChallenge.Domain.Core.Errors;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Reviews;

namespace ChatTeamChallenge.Application.Requests.Chat.Commands.Update;

public sealed class UpdateChatCommandHandler : ICommandHandler<UpdateChatCommand, Result>
{
    private readonly IChatRepository _chatRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public UpdateChatCommandHandler(IChatRepository chatRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _chatRepository = chatRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result> Handle(UpdateChatCommand request, CancellationToken cancellationToken)
    {
        // Check chat existing
        var chat = await _chatRepository.ReadByIdAsync(request.Id);
        if (chat is null)
        {
            return Result.Failure(DomainErrors.Chat.NotFound);
        }
        
        // Check topic unique
        var chatUnique = await _chatRepository.IsTopicUniqueAsync(request.Topic);
        if (chatUnique is false)
        {
            return Result.Failure(DomainErrors.Chat.TopicIsNotUnique(request.Topic));
        }

        var updatedChatDto = _mapper.Map<UpdateChatRequest>(request);
        
        await _chatRepository.UpdateAsync(updatedChatDto);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}