using System;

namespace Logger.Entity;

// for generalizing code between Employee and Student

// Id is implemented explicitly because we don't want the developers using this
// API to confuse it with a different Id, such as employee id or SSN

// Name is implemented explicitly because we don't want developers using this
// API to confuse it with a different name, such as FullName
public abstract record class Person(Guid Id, FullName FullName) : BaseEntity(Id);
