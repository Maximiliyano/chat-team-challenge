using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Domain.Core.Primities.Result;

namespace ChatTeamChallenge.Application.Requests.Messages.Commands.Remove;

public sealed record RemoveMessageCommand : ICommand<Result>
{
    public required int Id { get; set; }
    public required int UserId { get; set; }
    public required DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public required string Body { get; set; }
}