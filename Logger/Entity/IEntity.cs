namespace Logger.Entity;
public interface IEntity
{
    public string Name { get; }
    public Guid Id { get; init; }
}
