using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Domain.Core.Primities.Result;

namespace ChatTeamChallenge.Application.Requests.Users.Queries.ReadById;

public sealed record ReadByIdUserQuery(int Id) : IQuery<Result>
{
    public int Id { get; } = Id;
}