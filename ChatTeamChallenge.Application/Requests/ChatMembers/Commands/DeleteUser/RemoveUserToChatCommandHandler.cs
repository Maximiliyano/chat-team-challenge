using ChatTeamChallenge.Application.Core.Abstractions.Data;
using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Domain.Core.Errors;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Reviews;

namespace ChatTeamChallenge.Application.Requests.ChatMembers.Commands.DeleteUser;

public sealed class RemoveUserToChatCommandHandler : ICommandHandler<RemoveUserToChatCommand, Result>
{
    private readonly IChatMemberRepository _chatMemberRepository;
    private readonly IChatRepository _chatRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public RemoveUserToChatCommandHandler(
        IChatMemberRepository chatMemberRepository, 
        IUnitOfWork unitOfWork, 
        IChatRepository chatRepository, 
        IUserRepository userRepository)
    {
        _chatMemberRepository = chatMemberRepository;
        _unitOfWork = unitOfWork;
        _chatRepository = chatRepository;
        _userRepository = userRepository;
    }
    
    public async Task<Result> Handle(RemoveUserToChatCommand request, CancellationToken cancellationToken)
    {
        var chat = await _chatRepository.ReadByIdAsync(request.ChatId);
        if (chat is null)
        {
           return Result.Failure<int>(DomainErrors.Chat.NotFound);
        }

        var user = await _userRepository.ReadByIdAsync(request.UserId);
        if (user is null)
        {
            return Result.Failure<int>(DomainErrors.User.NotFound(request.UserId));
        }
        
        var chatMember = chat.Members!.FirstOrDefault(m => m.UserId == request.UserId);
        if (chatMember is null)
        {
            return Result.Failure(DomainErrors.ChatMember.NotFound);
        }
        
        await _chatMemberRepository.RemoveAsync(chatMember);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}