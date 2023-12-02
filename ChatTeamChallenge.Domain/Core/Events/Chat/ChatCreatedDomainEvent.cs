namespace ChatTeamChallenge.Domain.Core.Events.Chat;

public sealed class ChatCreatedDomainEvent : IDomainEvent
{
    internal ChatCreatedDomainEvent(Apartments.Chat chat) => Chat = chat;
        
    public Apartments.Chat Chat { get; }
}