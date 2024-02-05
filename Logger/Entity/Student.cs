using System;


namespace Logger.Entity;

public record Student(Guid Id, FullName FullName) : Person(Id, FullName)
{
    internal override string CalculateName()
    {
        return nameof(Student);
    }
}

