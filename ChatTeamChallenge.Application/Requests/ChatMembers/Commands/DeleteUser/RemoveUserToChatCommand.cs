using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Domain.Core.Primities.Result;

namespace ChatTeamChallenge.Application.Requests.ChatMembers.Commands.DeleteUser;

public sealed record RemoveUserToChatCommand(int UserId, int ChatId) : ICommand<Result>;