using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Contracts.Chat;
using ChatTeamChallenge.Domain.Core.Primities.Result;

namespace ChatTeamChallenge.Application.Requests.Chat.Commands.Update;

public sealed record UpdateChatCommand : ICommand<Result>
{
    public UpdateChatCommand(UpdateChatRequest updateChatRequest)
    {
        Id = updateChatRequest.Id;
        Topic = updateChatRequest.Topic;
        IsPublic = updateChatRequest.IsPublic;
    }
    
    public int Id { get; }
    public string Topic { get; }
    public bool IsPublic { get; set; }
}