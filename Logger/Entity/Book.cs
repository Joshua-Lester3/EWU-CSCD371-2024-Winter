using System;


namespace Logger.Entity;

public record Book(Guid Id) : BaseEntity(Id)
{
    protected override string CalculateName()
    {
        return nameof(Book);
    }
}
