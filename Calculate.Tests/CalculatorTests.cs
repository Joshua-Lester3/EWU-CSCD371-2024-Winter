namespace Calculate.Tests;

public class CalculatorTests
{
    [Theory]
    [InlineData(5, 2, 3)]
    [InlineData(0, -2, 2)]
    [InlineData(0, 2, -2)]
    [InlineData(-4, -2, -2)]
    public void Add_NonNullValues_Success(int expected, int left, int right)
    {
        int result = Calculator.Add(left, right);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(-1, 2, 3)]
    [InlineData(-4, -2, 2)]
    [InlineData(4, 2, -2)]
    [InlineData(0, -2, -2)]
    public void Subtract_NonNullValues_Success(int expected, int left, int right)
    {
        int result = Calculator.Subtract(left, right);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(6, 2, 3)]
    [InlineData(-4, -2, 2)]
    [InlineData(-4, 2, -2)]
    [InlineData(4, -2, -2)]
    [InlineData(0, 0, 4)]
    public void Multiply_NonNullValues_Success(int expected, int left, int right)
    {
        int result = Calculator.Multiply(left, right);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 2, 3)]
    [InlineData(-1, -2, 2)]
    [InlineData(-1, 2, -2)]
    [InlineData(1, -2, -2)]
    [InlineData(0, 0, 4)]
    public void Divide_NonNullValues_Success(int expected, int left, int right)
    {
        int result = Calculator.Divide(left, right);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Divide_ByZero_ThrowsDivideByZeroException()
    {
        Assert.Throws<DivideByZeroException>(() => Calculator.Divide(0, 0));
    }

    [Theory]
    [MemberData(nameof(TestCases))]
    public void MathematicalOperations_GetOperations_Success(Func<int, int, int> expectedOperation, char key)
    {
        // Arrange
        Calculator calculator = new();

        // Act
        Func<int, int, int>? operation;
        calculator.MathematicalOperations.TryGetValue(key, out operation);

        // Assert
        Assert.Equal(expectedOperation, operation);
    }

    public static IEnumerable<object[]> TestCases { get; } = new object[][] {
        new object[] { (Func<int, int, int>)Calculator.Add, '+' },
        new object[] { (Func<int, int, int>)Calculator.Subtract, '-' },
        new object[] { (Func<int, int, int>)Calculator.Multiply, '*' },
        new object[] { (Func<int, int, int>)Calculator.Divide, '/' }
    };

    [Fact]
    public void TryCalculate_OnlyOneOperand_ReturnsFalseAndOutParameterIsZero()
    {
        InvalidInputTest("4 +  ");
    }

    [Theory]
    [InlineData("4e + 4")]
    [InlineData("4 + 4e")]
    [InlineData("4e + 4e")]
    public void TryCalculate_OperandsAreNotInt_ReturnsFalseAndOutParameterIsZero(string input)
    {
        InvalidInputTest(input);
    }

    [Fact]
    public void TryCalculate_OperantNotValid_ReturnsFalseAndOutParameterIsZero()
    {
        InvalidInputTest("4 & 3");
    }

    [Fact]
    public void TryCalculate_OperantNotAChar_ReturnsFalseAndOutParameterIsZero()
    {
        InvalidInputTest("4 +++++ 4");
    }

    [Theory]
    [InlineData(6, "4 + 2")]
    [InlineData(2, "4 - 2")]
    [InlineData(8, "4 * 2")]
    [InlineData(2, "4 / 2")]
    public void TryCalculate_ValidInput_Success(int expected, string input)
    {
        // Arrange
        Calculator calculator = new();
        int result;

        // Act
        bool didSucceed = calculator.TryCalculate(input, out result);

        // Assert
        Assert.True(didSucceed);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TryCalculate_DividesByZero_CatchesExceptionAndReturnsNullAndZero()
    {
        // Arrange
        Calculator calculator = new();

        // Act

        // Assert
        Assert.False(calculator.TryCalculate("0 / 0", out int result));
        Assert.Equal(0, result);
    }

    private static void InvalidInputTest(string input)
    {
        // Arrange
        Calculator calculator = new();

        // Act
        bool didSucceed = calculator.TryCalculate(input, out int result);

        // Assert
        Assert.False(didSucceed);
        Assert.Equal(0, result);
    }
}
