using System.Net;
using ChatTeamChallenge.Application.Infrastructure;
using ChatTeamChallenge.Contracts.ChatMember;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Domain.Interfaces;
using ChatTeamChallenge.Services.Api.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace ChatTeamChallenge.Services.Api.Bookings.Chat;

public sealed class ChatMemberController : ApiController
{
    private readonly IChatMemberService _chatMemberService;

    public ChatMemberController(IChatMemberService chatMemberService)
    {
        _chatMemberService = chatMemberService;
    }
    
    [HttpPost(ApiRoutes.ChatMember.Add)]
    public async Task<IActionResult> AddUser([FromBody] ChatMemberRequest chatMemberRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = await _chatMemberService.AddUserToChatAsync(chatMemberRequest);
        return this.FromResult(result, nameof(AddUser), HttpStatusCode.Created);
    }
    
    [HttpGet(ApiRoutes.ChatMember.Get)]
    public async Task<IActionResult> GetUserDetailsByChat([FromRoute] int userId, [FromRoute] int chatId)
    {
        var result = await _chatMemberService.GetUserFromChatAsync(userId,  chatId);
        return this.FromResult(result);
    }
    
    [HttpGet(ApiRoutes.ChatMember.GetAll)]
    public async Task<IActionResult> GetAllUsersByChat(
        [FromQuery] int? chatId, 
        [FromQuery] int? userId,
        [FromQuery] int page = 1, 
        [FromQuery] int pageSize = 10)
    {
        var result = await _chatMemberService.GetAllAsync(page, pageSize, chatId, userId);
        return this.FromResult(result);
    }
    
    [HttpDelete(ApiRoutes.ChatMember.Remove)]
    public async Task<IActionResult> RemoveUsers([FromBody] ChatMemberRequest chatMemberRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = await _chatMemberService.RemoveUserToChatAsync(chatMemberRequest);
        return this.FromResult(result, successCode: HttpStatusCode.NoContent);
    }
    
    
    [HttpDelete(ApiRoutes.ChatMember.RemoveRange)]
    public async Task<IActionResult> RemoveRangeUsers([FromBody] IEnumerable<ChatMemberRequest> chatMemberRequests)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = await _chatMemberService.RemoveRangeAsync(chatMemberRequests);
        return this.FromResult(result, successCode: HttpStatusCode.NoContent);
    }
}