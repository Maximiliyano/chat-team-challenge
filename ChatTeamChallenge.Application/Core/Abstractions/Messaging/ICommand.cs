using MediatR;

namespace ChatTeamChallenge.Application.Core.Abstractions.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}