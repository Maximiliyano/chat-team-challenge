namespace ChatTeamChallenge.Domain.Core.Events.Message;

public sealed class CreateMessageDomainEvent : IDomainEvent
{
    internal CreateMessageDomainEvent(Apartments.Message message) => Message = message; 
    
    public Apartments.Message Message { get; }
}