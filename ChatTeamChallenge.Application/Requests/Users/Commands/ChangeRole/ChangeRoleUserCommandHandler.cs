using AutoMapper;
using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Contracts.Common.Constants;
using ChatTeamChallenge.Domain.Core.Errors;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Interfaces;
using ChatTeamChallenge.Domain.Models;
using ChatTeamChallenge.Domain.Reviews;

namespace ChatTeamChallenge.Application.Requests.Users.Commands.ChangeRole;

public sealed class ChangeRoleUserCommandHandler : ICommandHandler<ChangeRoleUserCommand, Result<UserModel>>
{
    private readonly IUserRepository _userRepository;
    private readonly IChatMemberRepository _chatMemberRepository;
    private readonly IChatMemberService _chatMemberService;
    private readonly IMapper _mapper;
    
    public ChangeRoleUserCommandHandler(
        IUserRepository userRepository, 
        IChatMemberRepository chatMemberRepository,
        IMapper mapper, IChatMemberService chatMemberService)
    {
        _userRepository = userRepository;
        _chatMemberRepository = chatMemberRepository;
        _mapper = mapper;
        _chatMemberService = chatMemberService;
    }
    
    public async Task<Result<UserModel>> Handle(ChangeRoleUserCommand request, CancellationToken cancellationToken)
    {
        var userRecord = await _userRepository.ReadByIdAsync(request.UserId);

        if (userRecord is null)
            return Result.Failure<UserModel>(DomainErrors.User.NotFound(request.UserId));
        
        if (userRecord.Roles == request.Roles)
            return Result.Failure<UserModel>(DomainErrors.User.RoleIsAlreadySet);
        
        var userChats = await _chatMemberRepository.GetAllAsync(userId: request.UserId);

        if (userChats.TotalRows is 0 || userChats.Items.FirstOrDefault(c => c.ChatId is EntityConstants.GeneralChatId) is null)
            return Result.Failure<UserModel>(DomainErrors.ChatMember.ChatIsNotFounded);

        var chatMembers = userChats.Items.Where(c => c.ChatId is not EntityConstants.GeneralChatId);
        await _chatMemberRepository.RemoveRange(chatMembers);
        
        await _chatMemberService.AddToRequiredChatsAsync(request.UserId, request.Roles);

        var userModel = _mapper.Map<UserModel>(userRecord);
        return Result.Success(userModel);
    }
}