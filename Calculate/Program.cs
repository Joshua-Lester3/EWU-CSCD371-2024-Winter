namespace Calculate;

using ConsoleUtilities;

public class Program
{
    public static void Main()
    {
        ProgramBase programBase = new();
        Calculator calculator = new();

        programBase.WriteLine("Welcome to the Calculator!");
        while (true)
        {
            programBase.WriteLine("Please enter a calculation (or 'q' to quit):");
            string? input = programBase.ReadLine();

            if (input is null || input.Contains('q'))
            {
                programBase.WriteLine("Exiting...");
                break;
            }

            if (calculator.TryCalculate(input, out int answer))
            {
                programBase.WriteLine($"What a lovely calculation! The answer... is {answer}");
            }
            else
            {
                programBase.WriteLine("Invalid input. Please try again.");
            }
        }
    }
}