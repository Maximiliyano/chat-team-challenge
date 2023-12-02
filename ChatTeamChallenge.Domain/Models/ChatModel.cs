using ChatTeamChallenge.Domain.Core.Primities;

namespace ChatTeamChallenge.Domain.Models;

public sealed class ChatModel : BaseModel
{
    public required string Topic { get; set; }
    public required bool IsPublic { get; set; }

    // TODO replace IEnumerable to PagedList
    public IEnumerable<ChatMemberModel>? Members { get; set; }
}