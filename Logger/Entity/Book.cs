using System;


namespace Logger.Entity;

public record Book(Guid Id) : BaseEntity(Id)
{
    internal override string CalculateName()
    {
        return nameof(Book);
    }
}
