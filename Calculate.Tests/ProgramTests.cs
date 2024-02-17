namespace Calculate.Tests;

public class ProgramTests
{
    #region Properties Tests
    [Theory]
    [InlineData("Jimbob is the best\r\n", "Jimbob is the best")]
    [InlineData("\r\n", "")]
    public void WriteLine_DefaultValue_Success(string expected, string actual)
    {
        // Arrange
        using TextWriter oldOut = Console.Out;
        using StringWriter newOut = new StringWriter();
        Console.SetOut(newOut);
        Program program = new();

        // Act
        program.WriteLine(actual);
        Console.SetOut(oldOut);

        // Assert
        Assert.Equal(expected, newOut.ToString());
    }

    [Theory]
    [InlineData("hi", "hi")]
    [InlineData(null, "")]
    public void ReadLine_DefaultValue_Success(string expected, string actual)
    {
        // Arrange
        using StringReader newIn = new(actual);
        using TextReader oldIn = Console.In;
        Console.SetIn(newIn);
        Program program = new();

        //Act
        string? result = program.ReadLine();
        Console.SetIn(oldIn);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Jimbob is the best\r\n", "Jimbob is the best")]
    [InlineData("\r\n", "")]
    public void WriteLine_InitializedValue_Success(string expected, string actual)
    {
        // Arrange
        using TextWriter oldOut = Console.Out;
        using StringWriter newOut = new StringWriter();
        Console.SetOut(newOut);
        Program program = new()
        {
            WriteLine = WriteLineTester
        };

        // Act
        program.WriteLine(actual);
        Console.SetOut(oldOut);

        // Assert
        Assert.Equal(expected, newOut.ToString());
    }

    [Theory]
    [InlineData("hi", "hi")]
    [InlineData(null, "")]
    public void ReadLine_InitializedValue_Success(string expected, string actual)
    {
        // Arrange
        using StringReader newIn = new(actual);
        using TextReader oldIn = Console.In;
        Console.SetIn(newIn);
        Program program = new()
        {
            ReadLine = ReadLineTester
        };

        //Act
        string? result = program.ReadLine();
        Console.SetIn(oldIn);

        // Assert
        Assert.Equal(expected, result);
    }

    private void WriteLineTester(object? value)
    {
        Console.WriteLine(value?.ToString());
    }

    private string? ReadLineTester()
    {
        return Console.ReadLine();
    }
    #endregion

    #region Constructor Tests
    [Fact]
    public void Program_DefaultConstructor_Success()
    {
        // Arrange
        Program program = new();

        // Act

        // Assert
        Assert.Equal(Console.WriteLine, program.WriteLine);
        Assert.Equal(Console.ReadLine, program.ReadLine);
    }
    #endregion

    #region Console (Main) Tests
    [Fact]
    public void Main_Welcome_Success()
    {
        // Arrange
        using TextWriter oldOut = Console.Out;
        using StringWriter newOut = new StringWriter();
        Console.SetOut(newOut);
        using TextReader oldIn = Console.In;
        using StringReader newIn = new(""); // Used to make program quit because of no new lines
        Console.SetIn(newIn);

        // Act
        Program.Main();
        Console.SetOut(oldOut);

        // Assert
        string expected = "Welcome to the Calculator!" + Environment.NewLine
            + "Pretty please enter a calculation you would like me to do:" + Environment.NewLine;
        Assert.Contains(expected, newOut.ToString());
    }

    [Fact]
    public void Main_ProgramCalculates_Success()
    {
        // Arrange
        using TextReader oldIn = Console.In;
        using StringReader newIn = new("4 + 2");
        Console.SetIn(newIn);
        using TextWriter oldOut = Console.Out;
        using StringWriter newOut = new StringWriter();
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
        using TextReader oldIn = Console.In;
        using StringReader newIn = new($"4 + 2{Environment.NewLine}q{Environment.NewLine}");
        Console.SetIn(newIn);
        using TextWriter oldOut = Console.Out;
        using StringWriter newOut = new StringWriter();
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
        using TextReader oldIn = Console.In;
        using StringReader newIn = new($"4 + 2{Environment.NewLine}5 + 4{Environment.NewLine}");
        Console.SetIn(newIn);
        using TextWriter oldOut = Console.Out;
        using StringWriter newOut = new StringWriter();
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
    [MemberData(nameof(TestCases))]
    public void Main_NoInput_SuccessfullyQuits(string input)
    {
        // Arrange
        using TextReader oldIn = Console.In;
        using StringReader newIn = new(input);
        Console.SetIn(newIn);
        using TextWriter oldOut = Console.Out;
        using StringWriter newOut = new StringWriter();
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
    public static IEnumerable<object[]> TestCases = new object[][]
    {
        new object[] { $"4 + 4{Environment.NewLine}" },
        new object[] { $"{Environment.NewLine}" }
    };
    #endregion
}
