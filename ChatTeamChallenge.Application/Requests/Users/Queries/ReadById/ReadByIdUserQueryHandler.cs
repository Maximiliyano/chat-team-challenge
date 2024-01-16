using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Domain.Core.Errors;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Reviews;

namespace ChatTeamChallenge.Application.Requests.Users.Queries.ReadById;

public sealed class ReadByIdUserQueryHandler : IQueryHandler<ReadByIdUserQuery, Result>
{
    private readonly IUserRepository _userRepository;
    
    public ReadByIdUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<Result> Handle(ReadByIdUserQuery request, CancellationToken cancellationToken)
    {
        var response = await _userRepository.ReadByIdAsync(request.Id);
        
        return response is null ? 
            Result.Failure(DomainErrors.User.NotFound(request.Id)) :
            Result.Success(response);
    }
}