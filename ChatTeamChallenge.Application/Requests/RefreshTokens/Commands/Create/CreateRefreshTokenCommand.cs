using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Domain.Core.Primities.Result;

namespace ChatTeamChallenge.Application.Requests.RefreshTokens.Commands.Create;

public sealed record CreateRefreshTokenCommand(string Token, int UserId) : ICommand<Result>;