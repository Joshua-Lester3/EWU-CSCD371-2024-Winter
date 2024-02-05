using System;

namespace Logger.Entity;

//for generalizing code between Employee and Student
public record Person(Guid Id, FullName FullName) : BaseEntity, IEntity
{
    public string Name => $"{FullName.FirstName} {FullName.LastName}";
}
