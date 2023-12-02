using ChatTeamChallenge.Application.Core.Abstractions.Data;
using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Domain.Core.Errors;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Helpers;
using ChatTeamChallenge.Domain.Reviews;

namespace ChatTeamChallenge.Application.Requests.Chat.Commands.Delete;

public sealed class DeleteChatCommandHandler : IQueryHandler<DeleteChatCommand, Result>
{
    private readonly IChatRepository _chatRepository;
    private readonly IChatMemberRepository _chatMemberRepository;
    private readonly IMessageRepository _messageRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public DeleteChatCommandHandler(
        IChatRepository chatRepository, 
        IChatMemberRepository chatMemberRepository,
        IUnitOfWork unitOfWork, 
        IMessageRepository messageRepository)
    {
        _chatRepository = chatRepository;
        _chatMemberRepository = chatMemberRepository;
        _unitOfWork = unitOfWork;
        _messageRepository = messageRepository;
    }
    
    public async Task<Result> Handle(DeleteChatCommand request, CancellationToken cancellationToken)
    {
        var chat = await _chatRepository.ReadByIdWithMessagesAsync(request.Id);

        if (chat is null)
        {
            return Result.Failure(DomainErrors.Chat.NotFound);
        }

        if (chat.Members is null)
        {
            return Result.Failure(DomainErrors.ChatMember.NotFound);
        }
        
        if (chat.Messages is null)
        {
            return Result.Failure(DomainErrors.Message.NotFound(request.Id));
        }

        foreach (var message in chat.Messages)
        {
            await _messageRepository.RemoveAsync(message);
        }
        
        foreach (var chatMember in chat.Members)
        {
            await _chatMemberRepository.RemoveAsync(chatMember);
        }
        
        await _chatRepository.RemoveAsync(chat);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}