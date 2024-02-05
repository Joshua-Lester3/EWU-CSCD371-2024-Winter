using System;


namespace Logger.Entity;

public record Book : BaseEntity, IEntity
{
    string IEntity.Name { get => nameof(Book); }
}
