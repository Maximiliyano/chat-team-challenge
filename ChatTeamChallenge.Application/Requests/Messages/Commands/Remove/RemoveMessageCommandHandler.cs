using AutoMapper;
using ChatTeamChallenge.Application.Core.Abstractions.Data;
using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Domain.Apartments;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Reviews;

namespace ChatTeamChallenge.Application.Requests.Messages.Commands.Remove;

public sealed class RemoveMessageCommandHandler : ICommandHandler<RemoveMessageCommand, Result>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    
    public RemoveMessageCommandHandler(IMessageRepository messageRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _messageRepository = messageRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RemoveMessageCommand request, CancellationToken cancellationToken)
    {
        var result = _mapper.Map<Message>(request);

        await _messageRepository.RemoveAsync(result);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}