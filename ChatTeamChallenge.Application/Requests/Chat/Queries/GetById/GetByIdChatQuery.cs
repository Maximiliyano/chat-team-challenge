using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Models;

namespace ChatTeamChallenge.Application.Requests.Chat.Queries.GetById;

public sealed class GetByIdChatQuery : IQuery<Result<ChatModel>>
{
    public GetByIdChatQuery(int id) => Id = id;
    
    public int Id { get; }
}