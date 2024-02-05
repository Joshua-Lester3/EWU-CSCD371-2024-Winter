using System;

namespace Logger.Entity;

//for generalizing code between Employee and Student
public abstract record Person(Guid Id, FullName FullName) : BaseEntity(Id)
{
}
