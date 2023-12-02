using System.ComponentModel.DataAnnotations;

namespace ChatTeamChallenge.Contracts.Chat;

public sealed class UpdateChatRequest : ChatRequest
{
    [Required]
    public required int Id { get; init; }
}