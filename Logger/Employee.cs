using System;

namespace Logger;

public record Employee(Guid Id, FullName FullName) : Person(Id, FullName);

