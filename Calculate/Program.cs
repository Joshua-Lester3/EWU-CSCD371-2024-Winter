
namespace Calculate;

public class Program
{
    public Action<object?> WriteLine { get; init; } = Console.WriteLine;
    public Func<string?> ReadLine { get; init; } = Console.ReadLine;

    public Program() { }
}
