using ChatTeamChallenge.Domain.Core.Primities;

namespace ChatTeamChallenge.Application.Contracts;

public class ApiErrorResponse
{
    public ApiErrorResponse(IReadOnlyCollection<Error> errors) => Errors = errors;
    
    public IReadOnlyCollection<Error> Errors { get; }
}