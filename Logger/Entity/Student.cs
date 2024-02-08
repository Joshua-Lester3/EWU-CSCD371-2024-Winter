﻿using System;


namespace Logger.Entity;

public record class Student(Guid Id, FullName FullName) : Person(Id, FullName)
{
    // Id is implemented explicitly because we don't want the developers using this
    // API to confuse it with a different Id, such as student id

    // Name is implemented explicitly because we don't want developers using this
    // API to confuse it with a different name, such as FullName
    protected override string CalculateName()
    {
        return nameof(Student);
    }
}

