using AutoMapper;
using ChatTeamChallenge.Application.Core.Abstractions.Data;
using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Domain.Apartments;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Reviews;

namespace ChatTeamChallenge.Application.Requests.Messages.Commands.Update;

public sealed class UpdateMessageCommandHandler : ICommandHandler<UpdateMessageCommand, Result<Message>>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateMessageCommandHandler(IMessageRepository messageRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _messageRepository = messageRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Message>> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
    {
        var result = _mapper.Map<Message>(request);

        result.UpdatedAt = DateTime.UtcNow;
        
        await _messageRepository.UpdateAsync(result);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(result);
    }
}