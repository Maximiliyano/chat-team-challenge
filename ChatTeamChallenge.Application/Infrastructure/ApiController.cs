using ChatTeamChallenge.Application.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Error = ChatTeamChallenge.Domain.Core.Primities.Error;

namespace ChatTeamChallenge.Application.Infrastructure;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("api/[controller]")]
public class ApiController : ControllerBase
{
    protected IActionResult BadRequest(Error error) => BadRequest(new ApiErrorResponse(new []{ error }));

    protected new IActionResult Ok(object value) => base.Ok(value);
}