using ChatTeamChallenge.Application.Requests.Chat.Commands.Create;
using ChatTeamChallenge.Application.Requests.Chat.Commands.Delete;
using ChatTeamChallenge.Application.Requests.Chat.Commands.Update;
using ChatTeamChallenge.Application.Requests.Chat.Queries.GetAll;
using ChatTeamChallenge.Application.Requests.Chat.Queries.GetById;
using ChatTeamChallenge.Application.Requests.Chat.Queries.GetByTopic;
using ChatTeamChallenge.Contracts.Chat;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Contracts.Enums;
using ChatTeamChallenge.Domain.Core.Errors;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Helpers;
using ChatTeamChallenge.Domain.Interfaces;
using ChatTeamChallenge.Domain.Models;
using MediatR;

namespace ChatTeamChallenge.Application.Disputes.Chats;

public sealed class ChatService : IChatService
{
    private readonly IMediator _mediator;
    private readonly IChatMemberService _chatMemberService;
    
    public ChatService(
        IMediator mediator, 
        IChatMemberService chatMemberService)
    {
        _mediator = mediator;
        _chatMemberService = chatMemberService;
    }
    
    public async Task<Result<int>> Create(CreationChatRequest creationChatRequest)
    {
        if (string.IsNullOrEmpty(creationChatRequest.Topic) || string.IsNullOrWhiteSpace(creationChatRequest.Topic))
        {
            return Result.Failure<int>(DomainErrors.Chat.TopicIsRequired);
        }
        
        var createChatCommand = new CreateChatCommand(creationChatRequest);
        var chatIdResult = await _mediator.Send(createChatCommand);

        if (chatIdResult.IsSuccess)
        {
            var chatMemberResult = await _chatMemberService.AddUserToChatAsync(
                GlobalBuilderHelper.BuildChatMemberRequest(creationChatRequest.PrimaryUserId, chatIdResult.Value, ChatMemberRoles.User));

            if (chatMemberResult.IsFailure)
            {
                return chatMemberResult;
            }
        }

        return chatIdResult;
    }

    public async Task<Result> Update(UpdateChatRequest updateChatRequest)
    {
        var updateChatCommand = new UpdateChatCommand(updateChatRequest);
        return await _mediator.Send(updateChatCommand);
    }

    public async Task<Result<ChatModel>> ReadByIdAsync(int id)
    {
        var readByIdChatQuery = new GetByIdChatQuery(id);
        return await _mediator.Send(readByIdChatQuery);
    }

    public async Task<Result<ChatModel>> ReadByTopicAsync(string topic)
    {
        var readByTopicChatQuery = new GetByTopicChatQuery(topic);
        return await _mediator.Send(readByTopicChatQuery);
    }

    public async Task<Result<PagedList<ChatModel>>> ReadAllAsync(int page, int pageSize, DateTime? dateTime, bool? isPublic, int? userId)
    {
        var readAllChatQuery = new GetAllAsyncQuery(page, pageSize, dateTime, isPublic, userId);
        return await _mediator.Send(readAllChatQuery);
    }

    public async Task<Result> RemoveAsync(int id)
    {
        var deleteChatCommand = new DeleteChatCommand(id);
        return await _mediator.Send(deleteChatCommand);
    }
}