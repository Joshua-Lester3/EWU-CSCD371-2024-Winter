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
        int result;
        Calculator.Add(left, right, out result);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(-1, 2, 3)]
    [InlineData(-4, -2, 2)]
    [InlineData(4, 2, -2)]
    [InlineData(0, -2, -2)]
    public void Subtract_NonNullValues_Success(int expected, int left, int right)
    {
        int result;
        Calculator.Subtract(left, right, out result);
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
        int result;
        Calculator.Multiply(left, right, out result);
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
        int result;
        Calculator.Divide(left, right, out result);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Divide_ByZero_ThrowsDivideByZeroException()
    {
        int result;
        Assert.Throws<DivideByZeroException>(() => Calculator.Divide(0, 0, out result));
    }

    [Fact]
    public void MathematicalOperations_GetAdd_Success()
    {
        // Arrange
        Calculator calculator = new();

        // Act
        Calculator.DelegateWithOut<int, int, int>? operation;
        calculator.MathematicalOperations.TryGetValue('+', out operation);

        // Assert
        Assert.Equal(Calculator.Add, operation);
    }

    [Theory]
    [MemberData(nameof(TestCases))]
    public void MathematicalOperations_GetSubtract_Success(Calculator.DelegateWithOut<int, int, int> expectedOperation, char key)
    {
        // Arrange
        Calculator calculator = new();

        // Act
        Calculator.DelegateWithOut<int, int, int>? operation;
        calculator.MathematicalOperations.TryGetValue(key, out operation);

        // Assert
        Assert.Equal(expectedOperation, operation);
    }

    public static IEnumerable<object[]> TestCases = new object[][] {
        new object[] { (Calculator.DelegateWithOut<int, int, int>)(Calculator.Add), '+' },
        new object[] { (Calculator.DelegateWithOut<int, int, int>)(Calculator.Subtract), '-' },
        new object[] { (Calculator.DelegateWithOut<int, int, int>)(Calculator.Multiply), '*' },
        new object[] { (Calculator.DelegateWithOut<int, int, int>)(Calculator.Divide), '/' }
    };
}
