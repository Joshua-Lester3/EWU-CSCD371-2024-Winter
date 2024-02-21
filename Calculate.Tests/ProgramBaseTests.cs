using ConsoleUtilities;
using IntelliTect.TestTools.Console;

namespace Calculate.Tests;

public class ProgramBaseTests
{
    #region Properties Tests
    [Theory]
    [InlineData("Jimbob")]
    public void WriteLine_DefaultValue_Success(string expected)
    {
        // Arrange
        ProgramBase program = new();

        // Act

        // Assert
        ConsoleAssert.Expect(expected, () => program.WriteLine(expected));
    }

    [Fact]
    public void ReadLine_InitializedValue_Success()
    {
        // Arrange
        ProgramBase program = new()
        {
            ReadLine = () => "hi"
        };

        //Act

        // Assert
        Assert.Equal("hi", program.ReadLine());
    }

    [Fact]
    public void ReadLine_DefaultValue_Success()
    {
        // Arrange
        ProgramBase program = new();
        string view = "<<hi>>";
        string? input = null;

        // Act

        // Assert
        ConsoleAssert.Expect(view, () => input = program.ReadLine());
        Assert.Equal("hi", input);
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
