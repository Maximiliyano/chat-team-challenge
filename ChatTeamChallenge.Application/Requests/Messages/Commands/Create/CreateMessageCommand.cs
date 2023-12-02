using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Contracts.Message;
using ChatTeamChallenge.Domain.Core.Primities.Result;

namespace ChatTeamChallenge.Application.Requests.Messages.Commands.Create;

public sealed record CreateMessageCommand : ICommand<Result<int>>
{
    public CreateMessageCommand(
        int senderUserId, string userName, CreationMessageRequest creationMessageRequest)
    {
        UserName = userName;
        Body = creationMessageRequest.Body.Trim();
        ChatId = creationMessageRequest.ChatId;
        SenderId = senderUserId;
        ReceiverId = creationMessageRequest.ReceiverId;
    }
    
    public int ChatId { get; set; }
    public int SenderId { get; set; }
    public int? ReceiverId { get; set; }
    public string UserName { get; set; }
    public string Body { get; set; }
}