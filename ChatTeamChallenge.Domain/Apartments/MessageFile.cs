namespace ChatTeamChallenge.Domain.Apartments;

public sealed class MessageFile : CommonFile
{
    public int MessageId { get; set; }
    
    public Message? Message { get; set; }

    public static MessageFile Create(int messageId, string name, string contentType, byte[] data)
    {
        var messageFile = new MessageFile
        {
            MessageId = messageId,
            Name = name,
            ContentType = contentType,
            Data = data
        };

        return messageFile;
    }
        
}