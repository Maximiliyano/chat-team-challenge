namespace ChatTeamChallenge.Domain.Core.Events.User;

public sealed class UserCreatedDomainEvent : IDomainEvent
{
    internal UserCreatedDomainEvent(Apartments.User user) => User = user;
    
    public Apartments.User User { get; }
} 