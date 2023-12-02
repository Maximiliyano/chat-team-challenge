using MediatR;

namespace ChatTeamChallenge.Application.Core.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}