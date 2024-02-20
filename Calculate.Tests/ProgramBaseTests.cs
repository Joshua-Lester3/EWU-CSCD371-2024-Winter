using ConsoleUtilities;

namespace Calculate.Tests;

public class ProgramBaseTests
{
    #region Properties Tests
    [Theory]
    [InlineData("Jimbob")]
    public void WriteLine_DefaultValue_Success(string expected)
    {
        // Arrange
        TextWriter oldOut = Console.Out;
        StringWriter newOut = new StringWriter();
        Console.SetOut(newOut);
        ProgramBase program = new();

        // Act
        program.WriteLine(expected);
        Console.SetOut(oldOut);

        // Assert
        Assert.Contains(expected, newOut.ToString());
    }

    [Theory]
    [InlineData("hi", "hi")]
    [InlineData(null, "")]
    public void ReadLine_DefaultValue_Success(string expected, string actual)
    {
        // Arrange
        StringReader newIn = new(actual);
        TextReader oldIn = Console.In;
        Console.SetIn(newIn);
        ProgramBase program = new();

        //Act
        string? result = program.ReadLine();
        Console.SetIn(oldIn);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Jimbob")]
    public void WriteLine_InitializedValue_Success(string expected)
    {
        // Arrange
        TextWriter oldOut = Console.Out;
        StringWriter newOut = new StringWriter();
        Console.SetOut(newOut);
        ProgramBase program = new()
        {
            WriteLine = WriteLineTester
        };

        // Act
        program.WriteLine(expected);
        Console.SetOut(oldOut);

        // Assert
        Assert.Contains(expected, newOut.ToString());
    }

    [Theory]
    [InlineData("hi", "hi")]
    [InlineData(null, "")]
    public void ReadLine_InitializedValue_Success(string expected, string actual)
    {
        // Arrange
        StringReader newIn = new(actual);
        TextReader oldIn = Console.In;
        Console.SetIn(newIn);
        ProgramBase program = new()
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
        ProgramBase program = new();

        // Act

        // Assert
        Assert.Equal(Console.WriteLine, program.WriteLine);
        Assert.Equal(Console.ReadLine, program.ReadLine);
    }
    #endregion
}
