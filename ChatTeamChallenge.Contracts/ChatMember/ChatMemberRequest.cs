using System.ComponentModel.DataAnnotations;
using ChatTeamChallenge.Contracts.Enums;

namespace ChatTeamChallenge.Contracts.ChatMember;

public sealed class ChatMemberRequest
{
    [Required]
    public required int UserId { get; init; }
    
    [Required]
    public required int ChatId { get; init; }
    
    [Required]
    public required ChatMemberRoles Role { get; init; }
}