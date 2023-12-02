using System.ComponentModel.DataAnnotations;

namespace ChatTeamChallenge.Contracts.Token;

public sealed class RefreshTokenRequest
{
    [Required]
    public required string AccessToken { get; init; }
    
    [Required]
    public required string RefreshToken { get; init; } 
}