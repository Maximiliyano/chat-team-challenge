using ChatTeamChallenge.Application.Core.Abstractions.Data;
using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Domain.Apartments;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Reviews;

namespace ChatTeamChallenge.Application.Requests.Messages.Commands.Create;

public sealed class CreateMessageCommandHandler : ICommandHandler<CreateMessageCommand, Result<int>>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateMessageCommandHandler(
        IMessageRepository messageRepository,
        IUnitOfWork unitOfWork)
    {
        _messageRepository = messageRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<int>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var message = Message.Create(
            request.SenderId,
            request.ChatId,
            request.UserName,
            request.Body,
            false,
            DateTime.UtcNow,
            null,
            request.ReceiverId);
        
        await _messageRepository.InsertAsync(message);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(message.Id);
    }
}