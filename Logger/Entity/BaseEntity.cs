namespace Logger.Entity;

public abstract record class BaseEntity : IEntity
{
    // Id is implemented explicitly because we don't want the developers using this
    // API to confuse it with a different Id, such as a student or employee id
    Guid IEntity.ID { get => _InternalId; init => _InternalId = value; }
    private Guid _InternalId = Guid.NewGuid();
    // Name is implemented explicitly because we don't want developers using this
    // API to confuse it with a different name, such as FullName in person
    // and its deriving types
    string IEntity.Name { get => CalculateName(); }
    public BaseEntity(Guid id)
    {
        _InternalId = id;
    }
    protected abstract string CalculateName();
}
