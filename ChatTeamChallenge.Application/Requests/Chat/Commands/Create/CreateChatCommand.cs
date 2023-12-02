using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Contracts.Chat;
using ChatTeamChallenge.Domain.Core.Primities.Result;

namespace ChatTeamChallenge.Application.Requests.Chat.Commands.Create;

public sealed record CreateChatCommand : ICommand<Result<int>>
{
    public CreateChatCommand(CreationChatRequest creationChatRequest)
    {
        Topic = creationChatRequest.Topic;
        IsPublic = creationChatRequest.IsPublic;
    }
    
    public string Topic { get; }
    public bool IsPublic { get; }
}