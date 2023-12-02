using AutoMapper;
using ChatTeamChallenge.Application.Core.Abstractions.Data;
using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Contracts.Enums;
using ChatTeamChallenge.Domain.Apartments;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Helpers;
using ChatTeamChallenge.Domain.Reviews;

namespace ChatTeamChallenge.Application.Requests.Users.Commands.Delete;

public sealed class RemoveUserCommandHandler : ICommandHandler<RemoveUserCommand, Result<int>>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IChatRepository _chatRepository;
    private readonly IChatMemberRepository _chatMemberRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public RemoveUserCommandHandler(
        IMessageRepository messageRepository,
        IChatRepository chatRepository,
        IChatMemberRepository chatMemberRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _messageRepository = messageRepository;
        _chatRepository = chatRepository;
        _chatMemberRepository = chatMemberRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    // TODO
    public async Task<Result<int>> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(
            request.Username, 
            request.Email, 
            request.Password, 
            request.City, 
            request.IsRemote, 
            request.Roles,
            request.Description,
            request.InstagramLink,
            request.DiscordLink,
            request.TelegramLink,
            request.SpotifyLink);
        
        // Delete chat members
        if (user.Members is not null)
        {
            foreach (var chatMember in user.Members)
            {
                if (chatMember.Chat is not null && user.Roles == CreativeRoles.Moderator)
                {
                    
                }
                
                await _chatMemberRepository.RemoveAsync(chatMember);
            }
        }
        
        // Delete user messages
        var userMessages = await _messageRepository.ReadAllAsync(
            GlobalBuilderHelper.BuildSearchMessageDto(user.Id));

        foreach (var message in userMessages.Items)
        {
            if (message is { ReceiverId: not null, Chat: not null })
            {
                await _chatRepository.RemoveAsync(message.Chat);
            }
            await _messageRepository.RemoveAsync(message);
        }
        
        await _userRepository.RemoveAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(user.Id);
    }
}