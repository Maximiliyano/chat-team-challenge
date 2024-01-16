using System.Net;
using ChatTeamChallenge.Domain.Core.Errors;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using Microsoft.AspNetCore.Mvc;

namespace ChatTeamChallenge.Services.Api.Utilities;

public static class ControllerBaseExtensions
{
    public static IActionResult FromResult<T>(this ControllerBase controller, Result<T> result,
        string? actionName = null, HttpStatusCode? successCode = HttpStatusCode.OK)
    {
        var code = GetStatusCode(result, successCode);
        return CreateIActionResult(controller, code, result, actionName);
    }

    public static IActionResult FromResult(this ControllerBase controller, Result result, // TODO rework to optimize
        HttpStatusCode? successCode = HttpStatusCode.OK)
    {
        var code = GetStatusCode(result, successCode);
        return CreateIActionResult(controller, code, result);
    }

    private static HttpStatusCode GetStatusCode(
        Result result, HttpStatusCode? successCode)
    {
        if (result.IsSuccess && successCode.HasValue)
        {
            return successCode.Value;
        }
        
        return (HttpStatusCode)result.Error.Code;
    }

    // TODO code smell, find solution to fix this, try to use dictionary
    private static IActionResult CreateIActionResult(ControllerBase controller, HttpStatusCode code, Result result)
    {
        return code switch
        {
            HttpStatusCode.BadRequest => controller.BadRequest(result.Error),
            HttpStatusCode.Conflict => controller.Conflict(result.Error),
            HttpStatusCode.OK => controller.Ok(),
            HttpStatusCode.NoContent => controller.NoContent(),
            HttpStatusCode.NotFound => controller.NotFound(result.Error),
            _ => throw new Exception("An unhandled result has occurred as a result of a service call.")
        };
    }
    // TODO code smell, find solution to fix this, try to use dictionary
    private static IActionResult CreateIActionResult<T>(ControllerBase controller, HttpStatusCode code, Result<T> result, string? actionName)
    {
        return code switch
        {
            HttpStatusCode.BadRequest => controller.BadRequest(result.Error),
            HttpStatusCode.Created => controller.CreatedAtAction(actionName, result.Value),
            HttpStatusCode.Conflict => controller.Conflict(result.Error),
            HttpStatusCode.OK => controller.Ok(result.Value),
            HttpStatusCode.NoContent => controller.NoContent(),
            HttpStatusCode.NotFound => controller.NotFound(result.Error),
            _ => throw new Exception("An unhandled result has occurred as a result of a service call.")
        };
    }

    public static Result<int> GetUserIdFromToken(this ControllerBase controller)
    {
        var claimsUserId = controller.User.Claims.FirstOrDefault(x => x.Type == "id")?.Value;

        return string.IsNullOrEmpty(claimsUserId) ?
            Result.Failure<int>(DomainErrors.AccessToken.InvalidToken) : 
            Result.Success(int.Parse(claimsUserId));
    }
}