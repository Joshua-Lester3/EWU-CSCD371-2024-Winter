using System;

namespace CanHazFunny
{
    public class Program
    {
        static void Main(string[] args)
        {
            Jester jester = new Jester(new OutputService(), new JokeService());
            jester.TellJoke();
        }
    }
}
