using System.ComponentModel.DataAnnotations;

namespace ChatTeamChallenge.Contracts.User;

public sealed class UpdateUserRequest : UserRequest
{
    [Required]
    public required int Id { get; init; }
}