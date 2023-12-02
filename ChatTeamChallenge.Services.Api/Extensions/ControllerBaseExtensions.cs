using System.Net;
using ChatTeamChallenge.Domain.Core.Errors;
using ChatTeamChallenge.Domain.Core.Primities;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using Microsoft.AspNetCore.Mvc;

namespace ChatTeamChallenge.Services.Api.Extensions;

public static class ControllerBaseExtensions
{
    public static IActionResult FromResult<T>(this ControllerBase controller, Result<T> result,
        string? actionName = null, HttpStatusCode? successCode = HttpStatusCode.OK)
    {
        var code = GetStatusCode(result, successCode);
        return CreateIActionResult(controller, code, result, actionName);
    }

    public static IActionResult FromResult(this ControllerBase controller, Result result, // TODO rework to optimize
        string? actionName = null, HttpStatusCode? successCode = HttpStatusCode.OK)
    {
        var code = GetStatusCode(result, successCode);
        return CreateIActionResult<object>(controller, code, result, actionName);
    }

    private static int GetStatusCode(
        Result result, HttpStatusCode? successCode)
    {
        if (result.IsSuccess && successCode.HasValue)
        {
            return (int)successCode.Value;
        }
        
        return result.Error.Code;
    }

    private static IActionResult CreateIActionResult<T>(ControllerBase controller, int code, Result<T> result, string? actionName)
    {
        return code switch
        {
            (int)HttpStatusCode.BadRequest => controller.BadRequest(result.Error),
            (int)HttpStatusCode.Created => controller.CreatedAtAction(actionName, result.Value),
            (int)HttpStatusCode.Conflict => controller.Conflict(result.Error),
            (int)HttpStatusCode.OK => controller.Ok(result.Value),
            (int)HttpStatusCode.NoContent => controller.NoContent(),
            (int)HttpStatusCode.NotFound => controller.NotFound(result.Error),
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