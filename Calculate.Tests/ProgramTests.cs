using IntelliTect.TestTools.Console;

namespace Calculate.Tests;

public class ProgramTests
{
    #region Console (Main) Tests
    [Fact]
    public void Main_Welcome_Success()
    {
        // Arrange
        string view = @"Welcome to the Calculator!
Please enter a calculation (or 'q' to quit):<<
>>
Exiting...";

        // Act


        // Assert
        ConsoleAssert.Expect(view, Program.Main);
    }

    [Fact]
    public void Main_ProgramCalculates_Success()
    {
        // Arrange
        string view = @"Welcome to the Calculator!
Please enter a calculation (or 'q' to quit):<<4 + 2
>>
What a lovely calculation! The answer... is 6
Please enter a calculation (or 'q' to quit):<<q
>>
Exiting...";
        // Act

        // Assert
        ConsoleAssert.Expect(view, Program.Main);
    }

    [Fact]
    public void Main_ProgramQuit_Success()
    {
        // Arrange
        string view = @"Welcome to the Calculator!
Please enter a calculation (or 'q' to quit):<<q
>>
Exiting...";

        // Act

        // Assert
        ConsoleAssert.Expect(view, Program.Main);
    }

    [Fact]
    public void Main_ProgramRepeat_Success()
    {
        // Arrange
        string view = @"Welcome to the Calculator!
Please enter a calculation (or 'q' to quit):<<4 + 2
>>
What a lovely calculation! The answer... is 6
Please enter a calculation (or 'q' to quit):<<6 + 3
>>
What a lovely calculation! The answer... is 9
Please enter a calculation (or 'q' to quit):
Exiting...";

        // Act

        // Assert
        ConsoleAssert.Expect(view, Program.Main);
    }

    [Theory]
    [MemberData(nameof(TestCases))]
    public void Main_InvalidInput_AsksToTryAgain(string view)
    {
        // Arrange

        // Act

        // Assert
        ConsoleAssert.Expect(view, Program.Main);
    }

    // Used for MemberData in Main_InvalidInput_AsksToTryAgain()
    public static IEnumerable<object[]> TestCases { get; } = new object[][]
    {
        new object[] { @"Welcome to the Calculator!
Please enter a calculation (or 'q' to quit):<<4eee4
>>
Invalid input. Please try again.
Please enter a calculation (or 'q' to quit):<<q
>>
Exiting..." },
        new object[] { @"Welcome to the Calculator!
Please enter a calculation (or 'q' to quit):<<4 eee 4
>>
Invalid input. Please try again.
Please enter a calculation (or 'q' to quit):<<q
>>
Exiting..." }
    };

    [Fact]
    public void Main_NoInput_SuccessfullyQuits()
    {
        // Arrange
        string view = @"Welcome to the Calculator!
Please enter a calculation (or 'q' to quit):<<
>>
Exiting...";

        // Act

        // Assert
        ConsoleAssert.Expect(view, Program.Main);
    }
    #endregion
}
