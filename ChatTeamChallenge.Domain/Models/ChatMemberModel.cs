using ChatTeamChallenge.Contracts.Enums;

namespace ChatTeamChallenge.Domain.Models;

public sealed class ChatMemberModel
{
    private readonly DateTime _joinedAt;

    public ChatMemberModel()
    {
        JoinedAt = DateTime.UtcNow;
    }

    public int Id { get; set; }
    public int UserId { get; set; }
    public int ChatId { get; set; }
    public ChatMemberRoles Role { get; set; }
    
    public UserModel? User { get; set; }

    public DateTime JoinedAt
    {
        get => _joinedAt;
        init => _joinedAt = value == DateTime.MinValue ? DateTime.UtcNow : value;
    }
}