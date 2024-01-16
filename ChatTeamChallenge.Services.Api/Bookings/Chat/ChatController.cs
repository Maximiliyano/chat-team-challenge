using System.Net;
using ChatTeamChallenge.Application.Infrastructure;
using ChatTeamChallenge.Contracts.Chat;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Contracts.Common.Constants;
using ChatTeamChallenge.Contracts.Enums;
using ChatTeamChallenge.Domain.Core.Errors;
using ChatTeamChallenge.Domain.Interfaces;
using ChatTeamChallenge.Services.Api.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace ChatTeamChallenge.Services.Api.Bookings.Chat;

public sealed class ChatController : ApiController
{
    private readonly IChatService _chatService;
    
    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpPost(ApiRoutes.Chat.Create)]
    public async Task<IActionResult> Create([FromBody] CreationChatRequest creationChatRequest)
    {
        var userIdFromTokenResult = this.GetUserIdFromToken();

        if (userIdFromTokenResult.IsFailure)
            return BadRequest(userIdFromTokenResult.Error);

        var userId = userIdFromTokenResult.Value;
        
        if (creationChatRequest.PrimaryUserId != userId)
        {
            return BadRequest(DomainErrors.User.NotFound(creationChatRequest.PrimaryUserId));
        }
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var chatIdResult = await _chatService.Create(creationChatRequest);
        return this.FromResult(chatIdResult, nameof(Create), HttpStatusCode.Created);
    }

    [HttpPut(ApiRoutes.Chat.Update)]
    public async Task<IActionResult> Update([FromBody] UpdateChatRequest updateChatRequest)
    {
        var result = await _chatService.Update(updateChatRequest);
        return this.FromResult(result);
    }

    [HttpGet(ApiRoutes.Chat.GetById)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var result = await _chatService.ReadByIdAsync(id);
        return this.FromResult(result);
    }
    
    [HttpGet(ApiRoutes.Chat.GetByTopic)]
    public async Task<IActionResult> GetByTopic([FromRoute] string topic)
    {
        var result = await _chatService.ReadByTopicAsync(topic);
        return this.FromResult(result);
    }
    
    [HttpGet(ApiRoutes.Chat.GetAll)]
    public async Task<IActionResult> GetAll(
        [FromQuery] int? userId,
        [FromQuery] DateTime? dateTime, 
        [FromQuery] bool? isPublic,
        [FromQuery] int page = 1, 
        [FromQuery] int pageSize = 10)
    {
        var result = await _chatService.ReadAllAsync(page, pageSize, dateTime, isPublic, userId);
        return this.FromResult(result);
    }

    [HttpDelete(ApiRoutes.Chat.Remove)]
    public async Task<IActionResult> Remove([FromRoute] int id)
    {
        if (id >= EntityConstants.GeneralChatId && id <= Enum.GetValues<CreativeRoles>().Length)
        {
            return BadRequest(DomainErrors.Chat.ImpossibleToDelete);
        }
        
        var result = await _chatService.RemoveAsync(id);
        return this.FromResult(result, successCode: HttpStatusCode.NoContent);
    }
}