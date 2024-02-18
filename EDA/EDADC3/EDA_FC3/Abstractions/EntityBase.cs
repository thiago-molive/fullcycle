namespace EDA_FC3.Abstractions;

public abstract class EntityBase<TIdType> : IEntity<TIdType>
{
    private readonly List<DomainEventBase> _domainEvents = new List<DomainEventBase>();

    public virtual TIdType Id { get; protected set; }

    public void SetId(TIdType id) => Id = id;

    public IReadOnlyCollection<DomainEventBase> GetEvents() => _domainEvents.AsReadOnly();

    public void ClearEvents() => _domainEvents.Clear();

    public void Publish(DomainEventBase @event) =>
        _domainEvents.Add(@event);
}