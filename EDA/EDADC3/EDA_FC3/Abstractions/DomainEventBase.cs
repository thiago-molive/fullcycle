namespace EDA_FC3.Abstractions;

public abstract class DomainEventBase : IDomainEvent
{
    protected DomainEventBase()
    {
        Id = Guid.NewGuid().ToString();
        Timestamp = DateTime.UtcNow;
    }

    public string Id { get; set; }

    public DateTime Timestamp { get; set; }
}