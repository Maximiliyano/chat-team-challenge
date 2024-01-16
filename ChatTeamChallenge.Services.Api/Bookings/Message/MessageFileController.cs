using ChatTeamChallenge.Application.Infrastructure;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Contracts.Enums;
using ChatTeamChallenge.Domain.Core.Errors;
using ChatTeamChallenge.Domain.Interfaces;
using ChatTeamChallenge.Services.Api.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatTeamChallenge.Services.Api.Bookings.Message;

[AllowAnonymous]
public sealed class MessageFileController : ApiController
{
    private readonly IMessageFileService _messageFileService;
    
    public MessageFileController(IMessageFileService messageFileService)
    {
        _messageFileService = messageFileService;
    }
    
    [HttpPost(ApiRoutes.FileManager.Upload)]
    public async Task<IActionResult> Upload(IFormFile file, FileType fileType, [FromRoute] int messageId)
    {
        if (file is null)
        {
            return BadRequest(DomainErrors.File.Missing);
        }
        
        var result = await _messageFileService.Upload(file, fileType, messageId);
        return this.FromResult(result);
    }
    
    [HttpGet(ApiRoutes.FileManager.Download)]
    public async Task<IActionResult> Download([FromRoute] int id)
    {
        var imageResult = await _messageFileService.GetById(id);

        if (imageResult.IsFailure)
        {
            return BadRequest(imageResult.Error);
        }

        var image = imageResult.Value;

        return File(image.Data, image.ContentType);
    }

    [HttpGet(ApiRoutes.FileManager.All)]
    public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
    {
        var result = await _messageFileService.GetAll(page, pageSize);
        return result.IsFailure ? 
            BadRequest(result.Error) : 
            Ok(result.Value);
    }

    [HttpDelete(ApiRoutes.FileManager.Remove)]
    public async Task<IActionResult> Remove([FromRoute] int id)
    {
        var result = await _messageFileService.Remove(id);
        return result.IsFailure ? 
            BadRequest(result.Error) : 
            NoContent();
    }
}