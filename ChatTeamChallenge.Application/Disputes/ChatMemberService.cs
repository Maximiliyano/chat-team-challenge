using ChatTeamChallenge.Application.Requests.ChatMembers.Commands.AddUser;
using ChatTeamChallenge.Application.Requests.ChatMembers.Commands.DeleteUser;
using ChatTeamChallenge.Application.Requests.ChatMembers.Queries.GetAll;
using ChatTeamChallenge.Application.Requests.ChatMembers.Queries.GetDetails;
using ChatTeamChallenge.Contracts.ChatMember;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Interfaces;
using ChatTeamChallenge.Domain.Models;
using MediatR;

namespace ChatTeamChallenge.Application.Disputes;

public sealed class ChatMemberService : IChatMemberService
{
    private readonly IMediator _mediator;
    
    public ChatMemberService(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task<Result<int>> AddUserToChatAsync(ChatMemberRequest chatMemberRequest)
    {
        var addUserToChatCommand = new AddUserToChatCommand(chatMemberRequest.UserId, chatMemberRequest.ChatId);
        return await _mediator.Send(addUserToChatCommand);
    }

    public async Task<Result<ChatMemberModel>> GetUserFromChatAsync(int userId, int chatId)
    {
        var getUserFromChatCommand = new GetDetailsToChatCommand(userId, chatId);
        return await _mediator.Send(getUserFromChatCommand);
    }

    public async Task<Result<PagedList<ChatMemberModel>>> GetAll(int page, int pageSize, int? chatId, int? userId)
    {
        var getAllMemberByChatId = new GetAllChatsByUserIdQuery(page, pageSize, chatId, userId);
        return await _mediator.Send(getAllMemberByChatId);
    }

    public async Task<Result> RemoveUserToChatAsync(ChatMemberRequest chatMemberRequest)
    {
        var removeUserToChatCommand = new RemoveUserToChatCommand(chatMemberRequest.UserId, chatMemberRequest.ChatId);
        return await _mediator.Send(removeUserToChatCommand);
    }
}