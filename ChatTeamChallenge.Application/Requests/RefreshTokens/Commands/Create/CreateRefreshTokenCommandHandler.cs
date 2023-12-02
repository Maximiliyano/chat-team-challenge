using ChatTeamChallenge.Application.Core.Abstractions.Data;
using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Helpers;
using ChatTeamChallenge.Domain.Reviews;

namespace ChatTeamChallenge.Application.Requests.RefreshTokens.Commands.Create;

public class CreateRefreshTokenCommandHandler : ICommandHandler<CreateRefreshTokenCommand, Result>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateRefreshTokenCommandHandler(
        IRefreshTokenRepository refreshTokenRepository, 
        IUnitOfWork unitOfWork)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result> Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = GlobalBuilderHelper.BuildRefreshToken(
            request.Token,
            request.UserId);

        refreshToken.UpdatedAt = DateTime.UtcNow;
        
        await _refreshTokenRepository.InsertAsync(refreshToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}