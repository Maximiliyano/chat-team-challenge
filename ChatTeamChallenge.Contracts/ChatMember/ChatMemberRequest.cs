using System.ComponentModel.DataAnnotations;

namespace ChatTeamChallenge.Contracts.ChatMember;

public sealed class ChatMemberRequest
{
    [Required]
    public required int UserId { get; init; }
    
    [Required]
    public required int ChatId { get; init; }
}