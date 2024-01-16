using ChatTeamChallenge.Application.Core.Abstractions.Data;
using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Contracts.Common.Constants;
using ChatTeamChallenge.Contracts.Enums;
using ChatTeamChallenge.Domain.Apartments;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Reviews;

namespace ChatTeamChallenge.Application.Requests.ChatMembers.Commands.AddUserToRequiredChats;

public sealed class AddUserToRequiredChatsCommandHandler : ICommandHandler<AddUserToRequiredChatsCommand, Result>
{
    private readonly IChatRepository _chatRepository;
    private readonly IChatMemberRepository _chatMemberRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public AddUserToRequiredChatsCommandHandler(
        IChatRepository chatRepository,
        IChatMemberRepository chatMemberRepository, 
        IUnitOfWork unitOfWork)
    {
        _chatMemberRepository = chatMemberRepository;
        _unitOfWork = unitOfWork;
        _chatRepository = chatRepository;
    }
    
    public async Task<Result> Handle(AddUserToRequiredChatsCommand request, CancellationToken cancellationToken)
    {
        var userRoles = Enum
            .GetValues(request.Roles.GetType())
            .Cast<CreativeRoles>()
            .Where(c => (request.Roles & c) == c && c != CreativeRoles.None)
            .ToList();

        if (request.GlobalChat)
        {
            await _chatMemberRepository.InsertAsync(ChatMember.Create(request.UserId, EntityConstants.GeneralChatId, ChatMemberRoles.User));
        }
        
        foreach (var userRole in userRoles)
        {
            var chatId = (await _chatRepository.ReadByTopicAsync(userRole.ToString()))!.Id;
            await _chatMemberRepository.InsertAsync(ChatMember.Create(request.UserId, chatId, ChatMemberRoles.User));
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}