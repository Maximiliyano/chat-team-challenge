using System.Net;
using ChatTeamChallenge.Application.Infrastructure;
using ChatTeamChallenge.Contracts.Authentication;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Domain.Interfaces;
using ChatTeamChallenge.Services.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatTeamChallenge.Services.Api.Bookings;

[AllowAnonymous]
public sealed class AuthController : ApiController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost(ApiRoutes.Auth.Login)]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = await _authService.Authorize(loginRequest);
        return this.FromResult(result);
    }
    
    [HttpPost(ApiRoutes.Auth.Register)]
    public async Task<IActionResult> Create([FromBody] RegisterRequest registerRequest)
    { 
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = await _authService.Register(registerRequest);
        return this.FromResult(result, nameof(Create), HttpStatusCode.Created);
    }
}