using ChatTeamChallenge.Domain.Apartments;
using ChatTeamChallenge.Domain.Core.Primities;

namespace ChatTeamChallenge.Domain.Models;

public sealed class MessageModel : BaseModel
{
    public required int SenderId { get; set; }
    public required int ChatId { get; set; }
    public required bool IsRead { get; set; }
    public required string SenderUserName { get; set; }
    public required string Body { get; set; }
    
    public int? ReceiverId { get; set; }
    
    public IEnumerable<MessageFile>? Files { get; set; }
}