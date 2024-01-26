using System;

namespace CanHazFunny
{
    public class OutputService : IOutputable
    {
        public void Output(string message)
        {
            ArgumentNullException.ThrowIfNull(message);
            Console.Write(message);
        }
    }
}
