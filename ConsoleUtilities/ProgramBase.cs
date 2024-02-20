namespace ConsoleUtilities;

public class ProgramBase
{
    public Action<object?> WriteLine { get; init; } = Console.WriteLine;
    public Func<string?> ReadLine { get; init; } = Console.ReadLine;

    public ProgramBase() { }
}
