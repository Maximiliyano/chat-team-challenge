using System.Net;
using ChatTeamChallenge.Application.Infrastructure;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Contracts.Token;
using ChatTeamChallenge.Domain.Interfaces;
using ChatTeamChallenge.Services.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatTeamChallenge.Services.Api.Bookings;

public sealed class TokenController : ApiController
{
    private readonly IAuthService _authService;
    
    public TokenController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [AllowAnonymous]
    [HttpPost(ApiRoutes.Token.Refresh)]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest refreshTokenRequest)
    {
        var result = await _authService.RefreshToken(refreshTokenRequest);
        return this.FromResult(result);
    }
    
    [HttpPost(ApiRoutes.Token.Revoke)]
    public async Task<IActionResult> RevokeRefreshToken([FromBody] RevokeRefreshTokenRequest request)
    {
        var userIdFromTokenResult = this.GetUserIdFromToken();

        if (userIdFromTokenResult.IsFailure)
            return BadRequest(userIdFromTokenResult.Error);
        
        var result = await _authService.RevokeRefreshToken(request.RefreshToken, userIdFromTokenResult.Value);
        return this.FromResult(result, successCode: HttpStatusCode.NoContent);
    }
}