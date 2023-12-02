using System.ComponentModel.DataAnnotations;
using System.Net;
using ChatTeamChallenge.Application.Infrastructure;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Contracts.User;
using ChatTeamChallenge.Domain.Core.Errors;
using ChatTeamChallenge.Domain.Interfaces;
using ChatTeamChallenge.Services.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatTeamChallenge.Services.Api.Bookings;

public sealed class UserController : ApiController
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet(ApiRoutes.User.GetById)]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var entityResult = await _userService.ReadByIdAsync(id);
        return this.FromResult(entityResult);
    }
    
    [AllowAnonymous]
    [HttpGet(ApiRoutes.User.GetByEmail)]
    public async Task<IActionResult> GetByEmail(
        [FromRoute] string email)
    { 
        if (!ModelState.IsValid)
            return BadRequest(DomainErrors.Email.InvalidFormat);
        
        var entity = await _userService.ReadByEmailAsync(email);
        return this.FromResult(entity);
    }

    [AllowAnonymous]
    [HttpGet(ApiRoutes.User.GetAll)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var pagedListUsersResult = await _userService.ReadAllAsync(page, pageSize);
        return this.FromResult(pagedListUsersResult);
    }
    
    [HttpGet(ApiRoutes.User.GetFromToken)]
    public async Task<IActionResult> GetUserFromToken()
    {
        var userFromResult = this.GetUserIdFromToken();

        if (userFromResult.IsFailure)
            return BadRequest(userFromResult.Error);
        
        var userResult = await _userService.ReadByIdAsync(userFromResult.Value);
        return this.FromResult(userResult);
    }

    [AllowAnonymous]
    [HttpPut(ApiRoutes.User.ChangePassword)]
    public async Task<IActionResult> ChangePasswordAsync([FromRoute] int userId, 
        [FromQuery] // TODO 
        [MinLength(8)]
        [MaxLength(16)] string newPassword)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = await _userService.ChangePasswordAsync(userId, newPassword.Trim());
        return this.FromResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest updateUserRequest)
    {
        if (!ModelState.IsValid) // TODO
        {
            return BadRequest(ModelState);
        }
        
        var result = await _userService.UpdateAsync(updateUserRequest);
        return this.FromResult(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromQuery] int userId)
    {
        var result = await _userService.DeleteAsync(userId);
        return this.FromResult(result, successCode: HttpStatusCode.NoContent);
    }
}