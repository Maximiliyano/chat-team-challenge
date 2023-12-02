using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Domain.Core.Primities.Result;

namespace ChatTeamChallenge.Application.Requests.Chat.Commands.Delete;

public sealed record DeleteChatCommand(int Id) : IQuery<Result>;