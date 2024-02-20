
namespace Calculate;

public class Program
{
    public Action<object?> WriteLine { get; init; } = Console.WriteLine;
    public Func<string?> ReadLine { get; init; } = Console.ReadLine;

    public Program() { }

    public static void Main()
    {
        Program program = new();
        Calculator calculator = new();

        program.WriteLine("Welcome to the Calculator!");
        while (true)
        {
            program.WriteLine("Please enter a calculation (or 'q' to quit):");
            string? input = program.ReadLine();

            if (string.IsNullOrEmpty(input) || input.Contains('q'))
            {
                program.WriteLine("Exiting...");
                break;
            }

            if (calculator.TryCalculate(input, out int answer))
            {
                program.WriteLine($"The answer is: {answer}");
            }
            else
            {
                program.WriteLine("Invalid input. Please try again.");
            }
        }
    }
}





//if we refactor for extra credit;



//namespace Calculate
//{
//    using ConsoleUtilities;

//    public class Program
//    {
//        public static void Main()
//        {
//            ProgramBase program = new ProgramBase();
//            program.Run();
//        }
//    }
//}

