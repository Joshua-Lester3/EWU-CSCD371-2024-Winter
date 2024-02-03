using Xunit;

namespace Logger.Tests;

public class FullNameTests
{
    [Fact]
    public void FullNameConstructor_NonNullParams_Success()
    {
        FullName fullName = new("Jimmy", "Johns");
        Assert.True(fullName is FullName);
    }

    [Fact]
    public void FullNameConstructor_NonNullParamsWithMiddleName_Success()
    {
        FullName fullName = new("Jimmy", "Johns", "Flippin'");
        Assert.True(fullName is FullName);
    }

    [Fact]
    public void FullNameConstructor_NullParams_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new FullName("Jimmy?", null!));
    }
}