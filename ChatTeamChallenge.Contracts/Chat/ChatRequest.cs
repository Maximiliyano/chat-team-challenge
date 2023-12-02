using System.ComponentModel.DataAnnotations;

namespace ChatTeamChallenge.Contracts.Chat;

public abstract class ChatRequest
{
    [Required]
    [MinLength(1)]
    [MaxLength(32)]
    public required string Topic { get; init; }
    
    [Required]
    public required bool IsPublic { get; init; }
}