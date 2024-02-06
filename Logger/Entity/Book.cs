using System;


namespace Logger.Entity;

public record Book : BaseEntity
{
    public Book(Guid id)
    {
        
    }
    protected override string CalculateName()
    {
        return nameof(Book);
    }
}
