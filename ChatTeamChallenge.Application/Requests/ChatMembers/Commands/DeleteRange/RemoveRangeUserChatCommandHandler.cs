using AutoMapper;
using ChatTeamChallenge.Application.Core.Abstractions.Data;
using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Domain.Apartments;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Reviews;

namespace ChatTeamChallenge.Application.Requests.ChatMembers.Commands.DeleteRange;

public sealed class RemoveRangeUserChatCommandHandler : ICommandHandler<RemoveRangeUserChatCommand, Result>
{
    private readonly IChatMemberRepository _chatMemberRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public RemoveRangeUserChatCommandHandler(
        IChatMemberRepository chatMemberRepository, 
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _chatMemberRepository = chatMemberRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result> Handle(RemoveRangeUserChatCommand request, CancellationToken cancellationToken)
    {
        var chatMembers = _mapper.Map<IEnumerable<ChatMember>>(request.ChatMemberRequests);
        
        await _chatMemberRepository.RemoveRange(chatMembers);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}