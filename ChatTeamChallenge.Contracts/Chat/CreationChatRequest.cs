using System.ComponentModel.DataAnnotations;

namespace ChatTeamChallenge.Contracts.Chat;

public sealed class CreationChatRequest : ChatRequest
{
    [Required]
    public required int PrimaryUserId { get; set; }
}