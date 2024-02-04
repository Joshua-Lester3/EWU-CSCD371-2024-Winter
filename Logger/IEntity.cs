namespace Logger;
public interface IEntity
{
    public string Name { get; set; }
    public Guid Id { get; init; }
}
