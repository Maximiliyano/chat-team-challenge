using ChatTeamChallenge.Domain.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ChatTeamChallenge.Application.Hubs;

public sealed class ChatHub : Hub<IChatClient>
{
    public async Task SendMessage(string message)
    {
        await Clients.All.ReceiveMessage($"{Context.ConnectionId}: {message}");
    }

    public override async Task OnConnectedAsync()
    {
        await Clients.All.ReceiveMessage($"{Context.ConnectionId} has joined");
    }
}