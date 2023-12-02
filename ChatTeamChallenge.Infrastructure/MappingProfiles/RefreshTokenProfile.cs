using AutoMapper;
using ChatTeamChallenge.Application.Requests.RefreshTokens.Commands.Remove;
using ChatTeamChallenge.Domain.Apartments;

namespace ChatTeamChallenge.Infrastructure.MappingProfiles;

public sealed class RefreshTokenProfile : Profile
{
    public RefreshTokenProfile()
    {
        CreateMap<RefreshToken, RemoveRefreshTokenCommand>();
        CreateMap<RemoveRefreshTokenCommand, RefreshToken>();
    }
}