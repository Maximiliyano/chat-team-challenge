using System.ComponentModel.DataAnnotations;
using System.Net;
using ChatTeamChallenge.Application.Infrastructure;
using ChatTeamChallenge.Contracts.Chat;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Domain.Interfaces;
using ChatTeamChallenge.Services.Api.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ChatTeamChallenge.Services.Api.Bookings;

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
        if (!ModelState.IsValid) // TODO check user from token
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

    [HttpGet(ApiRoutes.Chat.Get)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var result = await _chatService.ReadByIdAsync(id);
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
        var result = await _chatService.RemoveAsync(id);
        return this.FromResult(result, successCode: HttpStatusCode.NoContent);
    }
}