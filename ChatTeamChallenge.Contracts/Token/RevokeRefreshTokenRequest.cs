using System.ComponentModel.DataAnnotations;

namespace ChatTeamChallenge.Contracts.Token;

public sealed class RevokeRefreshTokenRequest
{
    [Required]
    public required string RefreshToken { get; init; }
}