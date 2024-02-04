using System;

namespace Logger;

//for generalizing code between Employee and Student
public record Person(Guid Id, FullName FullName) : IEntity
{
    public string Name => $"{FullName.FirstName} {FullName.LastName}";
}
