using System;

namespace Logger.Entity;

public record class Employee(Guid Id, FullName FullName) : Person(Id, FullName)
{
    protected override string CalculateName()
    {
        return nameof(Employee);
    }
}

