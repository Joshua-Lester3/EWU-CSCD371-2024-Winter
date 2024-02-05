namespace Logger.Entity;

public abstract record class BaseEntity(Guid Id) : IEntity
{
    Guid IEntity.Id { get; init; } = Guid.NewGuid();
    string IEntity.Name { get => CalculateName(); }
    internal abstract string CalculateName();
}
