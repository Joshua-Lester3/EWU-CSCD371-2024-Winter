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
}
