namespace Logger.Entity;
public interface IEntity
{
    public string Name { get; }
    public Guid ID { get; init; }
}
