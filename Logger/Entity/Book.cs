using System;


namespace Logger.Entity;

public record Book : BaseEntity
{
    public override string Name { get => nameof(Book); }
}
