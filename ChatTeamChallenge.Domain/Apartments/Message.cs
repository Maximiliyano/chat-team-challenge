using ChatTeamChallenge.Domain.Core.Abstractions;
using ChatTeamChallenge.Domain.Core.Events.Message;
using ChatTeamChallenge.Domain.Core.Primities;

namespace ChatTeamChallenge.Domain.Apartments;

public sealed class Message : AggregateRoot, IAuditableEntity
{
    public required int SenderId { get; set; }
    public required int ChatId { get; set; }
    public required string SenderUserName { get; set; }
    public required string Body { get; set; }
    public required bool IsRead { get; set; }
    public required DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    public int? ReceiverId { get; set; }
    
    public IEnumerable<MessageFile>? Files { get; set; }
    public Chat? Chat { get; set; }

    public static Message Create(
        int senderId, int chatId, string senderUserName, string body, bool isRead,
        DateTime? updatedAt = null, int? receiveId = null, int? id = null)
    {
        var message = new Message
        {
            Id = id ?? 0,
            SenderId = senderId,
            ChatId = chatId,
            SenderUserName = senderUserName,
            Body = body,
            IsRead = isRead,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = updatedAt,
            ReceiverId = receiveId
        };
        
        message.AddDomainEvent(new CreateMessageDomainEvent(message));

        return message;
    }
}