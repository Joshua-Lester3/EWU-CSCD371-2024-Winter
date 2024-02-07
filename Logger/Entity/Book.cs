using System;


namespace Logger.Entity;

public record Book(Guid Id) : BaseEntity(Id), IEntity
{
    protected override string CalculateName()
    {
        return nameof(Book);
    }
}
