namespace Logger.Entity;

public abstract record class BaseEntity : IEntity
{
    public BaseEntity(Guid id)
    {
        test = (IEntity)this;
        test.Id = id;
    }
    private IEntity test;
    Guid IEntity.Id { get; init; } = Guid.NewGuid();
    string IEntity.Name { get => CalculateName(); }
    protected abstract string CalculateName();
}
