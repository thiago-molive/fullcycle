namespace EDA_FC3.Abstractions;

public interface IEntity<TIdType> : IEntity
{
    TIdType Id { get; }

    void SetId(TIdType id);

}

public interface IEntity
{
    IReadOnlyCollection<DomainEventBase> GetEvents();

    void ClearEvents();

    void Publish(DomainEventBase @event);
}