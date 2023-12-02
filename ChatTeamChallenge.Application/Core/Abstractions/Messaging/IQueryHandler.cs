using MediatR;

namespace ChatTeamChallenge.Application.Core.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
}