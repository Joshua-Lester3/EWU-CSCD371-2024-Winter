using Xunit;

namespace Calculate.Tests;

public class ProgramTests
{

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
        using StringReader reader = new(actual);
        using TextReader oldIn = Console.In;
        Console.SetIn(reader);
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
        using StringReader reader = new(actual);
        using TextReader oldIn = Console.In;
        Console.SetIn(reader);
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

    private void WriteLineTester(object? value)
    {
        Console.WriteLine(value?.ToString());
    }

    private string? ReadLineTester()
    {
        return Console.ReadLine();
    }
}
