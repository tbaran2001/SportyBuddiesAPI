namespace SportyBuddies.Domain.Common;

public abstract class Entity
{
    protected readonly List<IDomainEvent> DomainEvents = [];

    protected Entity(Guid id)
    {
        Id = id;
    }

    public Entity()
    {
    }

    public Guid Id { get; init; }

    public List<IDomainEvent> PopDomainEvents()
    {
        var copy = DomainEvents.ToList();

        DomainEvents.Clear();

        return copy;
    }
}