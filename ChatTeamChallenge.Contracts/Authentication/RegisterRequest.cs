using System.ComponentModel.DataAnnotations;
using ChatTeamChallenge.Contracts.Common.Constants;
using ChatTeamChallenge.Contracts.User;

namespace ChatTeamChallenge.Contracts.Authentication;

public sealed class RegisterRequest : UserRequest
{
    [MaxLength(250)]
    [RegularExpression(PatternConstants.EmailPattern)]
    public required string Email { get; init; }

    [RegularExpression(PatternConstants.PasswordPattern)]
    public required string Password { get; set; }
}