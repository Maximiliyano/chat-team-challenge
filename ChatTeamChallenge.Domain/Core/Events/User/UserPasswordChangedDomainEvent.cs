namespace ChatTeamChallenge.Domain.Core.Events.User;

public sealed class UserPasswordChangedDomainEvent : IDomainEvent
{
    internal UserPasswordChangedDomainEvent(Apartments.User user) => User = user;
    
    public Apartments.User User { get; }
}