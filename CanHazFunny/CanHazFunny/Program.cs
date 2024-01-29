using System;

namespace CanHazFunny;

public class Program
{
    public static void Main(string[] args)
    {
        Jester jester = new(new OutputService(), new JokeService());
        jester.TellJoke();
    }
}
