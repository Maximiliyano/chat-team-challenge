using ChatTeamChallenge.Domain.Core.Abstractions;
using ChatTeamChallenge.Domain.Core.Events.Chat;
using ChatTeamChallenge.Domain.Core.Primities;

namespace ChatTeamChallenge.Domain.Apartments;

public sealed class Chat : AggregateRoot, IAuditableEntity
{
    public required string Topic { get; set; }
    public required bool IsPublic { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public IEnumerable<Message>? Messages { get; set; }
    public IEnumerable<ChatMember>? Members { get; set; }

    public static Chat Create(string topic, bool isPublic, int? id = null)
    {
        var chat = new Chat
        {
            Id = id ?? 0,
            Topic = topic,
            IsPublic = isPublic,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };

        chat.AddDomainEvent(new ChatCreatedDomainEvent(chat));
        
        return chat;
    }
}