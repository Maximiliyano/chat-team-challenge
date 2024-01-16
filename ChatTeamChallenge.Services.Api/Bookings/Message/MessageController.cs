using System.Net;
using ChatTeamChallenge.Application.Infrastructure;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Contracts.Message;
using ChatTeamChallenge.Domain.Interfaces;
using ChatTeamChallenge.Services.Api.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace ChatTeamChallenge.Services.Api.Bookings.Message;

public sealed class MessageController : ApiController
{
    private readonly IMessageService _messageService;
    
    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }
    
    [HttpPost(ApiRoutes.Message.Create)]
    public async Task<IActionResult> Create([FromRoute] int senderId, [FromBody] CreationMessageRequest creationMessageRequest)
    {
        var result = await _messageService.CreateAsync(senderId, creationMessageRequest);
        return this.FromResult(result, nameof(Create), HttpStatusCode.Created);
    }

    [HttpGet(ApiRoutes.Message.SearchBy)]
    public async Task<IActionResult> SearchBy([FromRoute] int messageId)
    {
        var result = await _messageService.ReadByIdAsync(messageId);
        return this.FromResult(result);
    }
    
    [HttpPost(ApiRoutes.Message.GetAll)]
    public async Task<IActionResult> GetAll(
        [FromBody] SearchMessageRequest? searchMessageDto,
        [FromQuery] int page = 1, 
        [FromQuery] int pageSize = 10)
    {
        var pagedListResult = await _messageService.ReadAllAsync(page, pageSize, searchMessageDto);
        return this.FromResult(pagedListResult);
    }

    [HttpPut(ApiRoutes.Message.Update)]
    public async Task<IActionResult> Update([FromRoute] int messageId, [FromQuery] string body)
    {
        var result = await _messageService.EditAsync(messageId, body);
        return this.FromResult(result);
    }
    
    [HttpDelete(ApiRoutes.Message.Remove)]
    public async Task<IActionResult> Delete([FromRoute] int messageId)
    {
        var result = await _messageService.DeleteAsync(messageId);
        return this.FromResult(result, successCode: HttpStatusCode.NoContent);
    }
}