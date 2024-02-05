namespace Logger.Entity;

public abstract record class BaseEntity : IEntity
{
    Guid IEntity.Id { get; init; } = Guid.NewGuid();
    public abstract string Name { get; }
}
