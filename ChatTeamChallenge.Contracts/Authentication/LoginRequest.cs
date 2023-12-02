using System.ComponentModel.DataAnnotations;
using ChatTeamChallenge.Contracts.Common;

namespace ChatTeamChallenge.Contracts.Authentication;

public sealed class LoginRequest
{
    [MaxLength(250)]
    [RegularExpression(PatternConstants.EmailPattern)]
    public required string Email { get; init; }
    
    [RegularExpression(PatternConstants.PasswordPattern)]
    public required string Password { get; init; }
}