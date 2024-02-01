using System;

namespace CanHazFunny;

public class OutputService : IOutputable
{
    public bool Output(string message)
    {
        ArgumentNullException.ThrowIfNull(message);
        Console.Write(message);
        return true;
    }
}
