using System;

namespace Logger.Entity;

//for generalizing code between Employee and Student
public abstract record class Person(Guid Id, FullName FullName) : BaseEntity(Id);
