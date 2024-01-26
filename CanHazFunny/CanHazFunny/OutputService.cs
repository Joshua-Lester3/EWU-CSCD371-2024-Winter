using System;

namespace CanHazFunny
{
    public class OutputService
    {
        public void Output(string message)
        {
            ArgumentNullException.ThrowIfNull(message);
            Console.Write(message);
        }
    }
}
