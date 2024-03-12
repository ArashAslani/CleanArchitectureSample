using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Domain;

public class AggregateRoot<TKey> : BaseEntity<TKey>
{
    private readonly List<BaseDomainEvent> _domainEvents = [];

    [NotMapped]
    public IEnumerable<BaseDomainEvent> DomainEvents => _domainEvents;

    public void AddDomainEvent(BaseDomainEvent eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void RemoveDomainEvent(BaseDomainEvent eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }
}

public class AggregateRoot : BaseEntity<Guid>
{
    private readonly List<BaseDomainEvent> _domainEvents = [];

    [NotMapped]
    public IEnumerable<BaseDomainEvent> DomainEvents => _domainEvents;

    public void AddDomainEvent(BaseDomainEvent eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void RemoveDomainEvent(BaseDomainEvent eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }
}

