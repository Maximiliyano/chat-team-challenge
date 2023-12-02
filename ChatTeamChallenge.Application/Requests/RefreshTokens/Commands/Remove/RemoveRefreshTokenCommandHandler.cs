using AutoMapper;
using ChatTeamChallenge.Application.Core.Abstractions.Data;
using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Domain.Apartments;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Reviews;

namespace ChatTeamChallenge.Application.Requests.RefreshTokens.Commands.Remove;

public sealed class RemoveRefreshTokenCommandHandler : ICommandHandler<RemoveRefreshTokenCommand, Result>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RemoveRefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result> Handle(RemoveRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = _mapper.Map<RefreshToken>(request);

        await _refreshTokenRepository.RemoveAsync(refreshToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}