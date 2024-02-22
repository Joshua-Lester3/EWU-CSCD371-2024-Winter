namespace Calculate;

using ConsoleUtilities;

public class Program
{
    public static void Main()
    {
        ProgramBase programBase = new();
        Calculator calculator = new();
        bool keepRunning = true;

        programBase.WriteLine("Welcome to the Calculator!");
        do
        {
            programBase.WriteLine("Please enter a calculation (or 'q' to quit):");
            string? input = programBase.ReadLine();

            if (input is null || input.Contains('q'))
            {
                programBase.WriteLine("Exiting...");
                keepRunning = false;
            }
            else if (calculator.TryCalculate(input, out int answer))
            {
                programBase.WriteLine($"What a lovely calculation! The answer... is {answer}");
            }
            else
            {
                programBase.WriteLine("Invalid input. Please try again.");
            }
        } while (keepRunning);
    }
}