using AutoMapper;
using ChatTeamChallenge.Application.Hubs;
using ChatTeamChallenge.Application.Requests.Messages.Commands.Create;
using ChatTeamChallenge.Application.Requests.Messages.Commands.Remove;
using ChatTeamChallenge.Application.Requests.Messages.Commands.Update;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Contracts.Message;
using ChatTeamChallenge.Domain.Core.Errors;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Hubs;
using ChatTeamChallenge.Domain.Interfaces;
using ChatTeamChallenge.Domain.Models;
using ChatTeamChallenge.Domain.Reviews;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace ChatTeamChallenge.Application.Disputes;

public sealed class MessageService : IMessageService
{
    private readonly IChatService _chatService;
    private readonly IChatMemberService _chatMemberService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IMessageRepository _messageRepository;
    private readonly IUserService _userService;
    private readonly IHubContext<ChatHub, IChatClient> _context;
    
    public MessageService(
        IChatService chatService,
        IChatMemberService chatMemberService,
        IMessageRepository messageRepository,
        IMapper mapper,
        IMediator mediator,
        IUserService userService, 
        IHubContext<ChatHub, IChatClient> context)
    {
        _chatService = chatService;
        _mapper = mapper;
        _messageRepository = messageRepository;
        _mediator = mediator;
        _userService = userService;
        _context = context;
        _chatMemberService = chatMemberService;
    }

    public async Task<Result<int>> CreateAsync(int senderUserId, CreationMessageRequest creationMessageRequest)
    {
        if (string.IsNullOrEmpty(creationMessageRequest.Body))
            return Result.Failure<int>(DomainErrors.Message.EmptyText);

        // Validation chat
        var chatResult = await _chatService.ReadByIdAsync(creationMessageRequest.ChatId);
        
        if (chatResult.IsFailure)
            return Result.Failure<int>(chatResult.Error);
        
        // Validation sender Id
        var senderUserResult = await _userService.ReadByIdAsync(senderUserId);
        
        if (senderUserResult.IsFailure)
            return Result.Failure<int>(senderUserResult.Error);

        var chatMemberResult = await _chatMemberService.GetUserFromChatAsync(
            senderUserResult.Value.Id, creationMessageRequest.ChatId);

        if (chatMemberResult.IsFailure)
            return Result.Failure<int>(chatMemberResult.Error);
        
        if (chatMemberResult.Value.ChatId != chatResult.Value.Id)
            return Result.Failure<int>(DomainErrors.ChatMember.IsNotAdded);

        // Validation receiver Id
        if (creationMessageRequest.ReceiverId.HasValue)
        {
            var receiverUserResult = await _userService.ReadByIdAsync(creationMessageRequest.ReceiverId.Value);
            
            if (receiverUserResult.IsFailure)
                return Result.Failure<int>(receiverUserResult.Error);
        }

        var senderUser = senderUserResult.Value;

        await _context.Clients.All.ReceiveMessage(creationMessageRequest.Body);
        
        var createCommand = new CreateMessageCommand(senderUserId, senderUser.Username, creationMessageRequest);
        var result = await _mediator.Send(createCommand);
        
        return result;
    }

    public async Task<Result> EditAsync(int messageId, string body)
    {
        if (string.IsNullOrEmpty(body) || string.IsNullOrWhiteSpace(body))
            return Result.Failure(DomainErrors.Message.EmptyText);
        
        var existMessageResult = await ReadByIdAsync(messageId);

        if (existMessageResult.IsFailure)
            return Result.Failure(existMessageResult.Error);

        var existMessage = existMessageResult.Value;
        
        existMessage.Body = body.Trim();

        var updateCommand = new UpdateMessageCommand(existMessage);
        var result = await _mediator.Send(updateCommand);
        
        return result;
    }

    public async Task<Result<PagedList<MessageModel>>> ReadAllAsync(int page, int pageSize, SearchMessageRequest? searchMessageDto)
    {
        if (page < 1 || pageSize < 1)
            return Result.Failure<PagedList<MessageModel>>(DomainErrors.Message.ZeroPageCount);

        var messageRecords = await _messageRepository.ReadAllAsync(searchMessageDto, page, pageSize);
        
        var pagedListWithMessage = _mapper.Map<PagedList<MessageModel>>(messageRecords);
        return Result.Success(pagedListWithMessage);
    }

    public async Task<Result<MessageModel>> ReadByIdAsync(int messageId)
    {
        var messageRecord = await _messageRepository.ReadByIdAsync(messageId);

        if (messageRecord is null)
            return Result.Failure<MessageModel>(DomainErrors.Message.NotFound(messageId));

        var message = _mapper.Map<MessageModel>(messageRecord);
        return Result.Success(message);
    }

    public async Task<Result> DeleteAsync(int messageId)
    {
        var message = await _messageRepository.ReadByIdAsync(messageId);

        if (message is null)
            return Result.Failure<MessageModel>(DomainErrors.Message.NotFound(messageId));
        
        var removeCommand = _mapper.Map<RemoveMessageCommand>(message);
        var result = await _mediator.Send(removeCommand);
        return result;
    }
}