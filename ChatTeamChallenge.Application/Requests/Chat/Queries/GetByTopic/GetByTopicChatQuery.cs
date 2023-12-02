using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Models;

namespace ChatTeamChallenge.Application.Requests.Chat.Queries.GetByTopic;

public sealed class GetByTopicChatQuery : IQuery<Result<ChatModel>>
{
    public GetByTopicChatQuery(string topic) => Topic = topic;
    
    public string Topic { get; }
}