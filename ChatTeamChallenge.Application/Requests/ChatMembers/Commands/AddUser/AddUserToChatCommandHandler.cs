using ChatTeamChallenge.Application.Core.Abstractions.Data;
using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Domain.Apartments;
using ChatTeamChallenge.Domain.Core.Errors;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Reviews;

namespace ChatTeamChallenge.Application.Requests.ChatMembers.Commands.AddUser;

public sealed class AddUserToChatCommandHandler : ICommandHandler<AddUserToChatCommand, Result<int>>
{
    private readonly IChatMemberRepository _chatMemberRepository;
    private readonly IChatRepository _chatRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddUserToChatCommandHandler(
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

    public async Task<Result<int>> Handle(AddUserToChatCommand request, CancellationToken cancellationToken = default)
    {
        var chatMemberExist = await _chatMemberRepository.GetDetailsAsync(request.UserId, request.ChatId);
        if (chatMemberExist is not null)
        {
            return Result.Failure<int>(DomainErrors.ChatMember.AlreadyAdded);
        }
        
        var chatExist = await _chatRepository.ReadByIdAsync(request.ChatId);
        if (chatExist is null)
        {
            return Result.Failure<int>(DomainErrors.Chat.NotFound);
        }

        var userExist = await _userRepository.ReadByIdAsync(request.UserId);
        if (userExist is null)
        {
            return Result.Failure<int>(DomainErrors.User.NotFound(request.UserId));
        }
        
        var chatMember = ChatMember.Create(request.UserId, request.ChatId, request.Role);

        await _chatMemberRepository.InsertAsync(chatMember);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(chatMember.Id);
    }
}