using ChatTeamChallenge.Domain.Core.Events;

namespace ChatTeamChallenge.Domain.Core.Primities;

public abstract class AggregateRoot : Entity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public IEnumerable<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent) =>
        _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() =>
        _domainEvents.Clear();
}