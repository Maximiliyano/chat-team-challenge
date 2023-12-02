namespace ChatTeamChallenge.Contracts.Message;

public sealed class SearchMessageRequest
{
    public int? SenderId { get; init; }
    public int? ReceiverId { get; init; }
    public int? ChatId { get; init; }
    public bool? IsRead { get; init; }
    public DateTime? Date { get; init; }
}