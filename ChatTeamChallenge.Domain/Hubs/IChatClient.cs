namespace ChatTeamChallenge.Domain.Hubs;

public interface IChatClient
{
    Task ReceiveMessage(string message);
}