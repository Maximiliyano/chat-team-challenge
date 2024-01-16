using System.ComponentModel.DataAnnotations;
using ChatTeamChallenge.Contracts.Common.Constants;

namespace ChatTeamChallenge.Contracts.Authentication;

public sealed class LoginRequest 
{
    [MaxLength(250)]
    public required string Credential { get; init; }
    
    [RegularExpression(PatternConstants.PasswordPattern)]
    public required string Password { get; init; }
}