namespace Calculate.Tests;

public class ProgramTests
{
    #region Console (Main) Tests
    [Fact]
    public void Main_Welcome_Success()
    {
        // Arrange
        TextWriter oldOut = Console.Out;
        StringWriter newOut = new StringWriter();
        Console.SetOut(newOut);
        TextReader oldIn = Console.In;
        StringReader newIn = new("q" + Environment.NewLine);
        Console.SetIn(newIn);

        // Act
        Program.Main();
        Console.SetOut(oldOut);
        Console.SetIn(oldIn);

        // Assert
        string expected = "Welcome to the Calculator!" + Environment.NewLine
            + "Please enter a calculation (or 'q' to quit):" + Environment.NewLine;
        Assert.Contains(expected, newOut.ToString());
    }

    [Fact]
    public void Main_ProgramCalculates_Success()
    {
        // Arrange
        TextReader oldIn = Console.In;
        StringReader newIn = new("4 + 2" + Environment.NewLine + "q" + Environment.NewLine);
        Console.SetIn(newIn);
        TextWriter oldOut = Console.Out;
        StringWriter newOut = new StringWriter();
        Console.SetOut(newOut);

        // Act
        Program.Main();
        Console.SetIn(oldIn);
        Console.SetOut(oldOut);

        // Assert
        Assert.Contains("What a lovely calculation! The answer... is 6", newOut.ToString());
    }

    [Fact]
    public void Main_ProgramQuit_Success()
    {
        // Arrange
        TextReader oldIn = Console.In;
        StringReader newIn = new($"4 + 2{Environment.NewLine}q{Environment.NewLine}");
        Console.SetIn(newIn);
        TextWriter oldOut = Console.Out;
        StringWriter newOut = new StringWriter();
        Console.SetOut(newOut);

        // Act
        Program.Main();
        Console.SetIn(oldIn);
        Console.SetOut(oldOut);

        // Assert
        Assert.Contains("Exiting...", newOut.ToString());
    }

    [Fact]
    public void Main_ProgramRepeat_Success()
    {
        // Arrange
        TextReader oldIn = Console.In;
        StringReader newIn = new($"4 + 2{Environment.NewLine}5 + 4{Environment.NewLine}q{Environment.NewLine}");
        Console.SetIn(newIn);
        using TextWriter oldOut = Console.Out;
        StringWriter newOut = new StringWriter();
        Console.SetOut(newOut);

        // Act
        Program.Main();
        Console.SetIn(oldIn);
        Console.SetOut(oldOut);

        // Assert
        Assert.Contains("The answer... is 6", newOut.ToString());
        Assert.Contains("The answer... is 9", newOut.ToString());
    }

    [Theory]
    [MemberData(nameof(TestCases1))]
    public void Main_InvalidInput_AsksToTryAgain(string input)
    {
        // Arrange
        TextReader oldIn = Console.In;
        StringReader newIn = new(input);
        Console.SetIn(newIn);
        TextWriter oldOut = Console.Out;
        StringWriter newOut = new StringWriter();
        Console.SetOut(newOut);

        // Act
        Program.Main();
        Console.SetIn(oldIn);
        Console.SetOut(oldOut);

        // Assert
        Assert.Contains("Invalid input. Please try again.", newOut.ToString());
        Assert.Contains("Exiting...", newOut.ToString());
    }

    // Used for MemberData in Main_InvalidInput_AsksToTryAgain()
    public static IEnumerable<object[]> TestCases1 { get; } = new object[][]
    {
        new object[] { $"4 e 4{Environment.NewLine}q" },
        new object[] { $"eeee{Environment.NewLine}q" }
    };

    [Theory]
    [MemberData(nameof(TestCases2))]
    public void Main_NoInput_SuccessfullyQuits(string input)
    {
        // Arrange
        TextReader oldIn = Console.In;
        StringReader newIn = new(input);
        Console.SetIn(newIn);
        TextWriter oldOut = Console.Out;
        StringWriter newOut = new StringWriter();
        Console.SetOut(newOut);

        // Act
        Program.Main();
        Console.SetIn(oldIn);
        Console.SetOut(oldOut);

        // Assert
        Assert.Contains("Exiting...", newOut.ToString());
    }

    // Used for MemberData in Main_NoInput_SuccessfullyQuits()
    // Used to simulate either no more lines to read or a Ctrl + Z shortcut to end the application
    public static IEnumerable<object[]> TestCases2 { get; } = new object[][]
    {
        new object[] { $"4 + 4{Environment.NewLine}q" },
        new object[] { $"{Environment.NewLine}q" }
    };
    #endregion
}
