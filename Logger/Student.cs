using System;


namespace Logger;

public record Student(Guid Id, FullName FullName) : Person(Id, FullName);

