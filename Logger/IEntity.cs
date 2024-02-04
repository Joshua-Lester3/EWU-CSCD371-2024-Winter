namespace Logger;
public interface IEntity
{
    public string Name { get; }
    public Guid Id { get; init; }
}
