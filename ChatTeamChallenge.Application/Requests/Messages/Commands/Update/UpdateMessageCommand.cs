using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Domain.Apartments;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Models;

namespace ChatTeamChallenge.Application.Requests.Messages.Commands.Update;

public sealed record UpdateMessageCommand : ICommand<Result<Message>>
{
    public UpdateMessageCommand(MessageModel messageModel)
    {
        Id = messageModel.Id;
        UserId = messageModel.SenderId;
        CreatedAt = messageModel.CreatedAt;
        UpdatedAt = messageModel.UpdatedAt;
        Body = messageModel.Body;
    }
    
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Body { get; set; }
}