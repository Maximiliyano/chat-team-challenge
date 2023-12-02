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

    public static Chat Create(string topic, bool isPublic, DateTime createdAt)
    {
        var chat = new Chat
        {
            Topic = topic,
            IsPublic = isPublic,
            CreatedAt = createdAt,
            UpdatedAt = null
        };

        chat.AddDomainEvent(new ChatCreatedDomainEvent(chat));
        
        return chat;
    }
}