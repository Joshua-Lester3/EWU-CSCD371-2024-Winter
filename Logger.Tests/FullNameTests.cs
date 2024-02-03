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
    public void FullNameConstructor_NullLastName_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentException>(() => new FullName("Jimmy?", null!));
    }

    [Fact]
    public void FullNameConstructor_NullFirstName_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentException>(() => new FullName(null!, "Johns?"));
    }

    [Fact]
    public void FullNameConstructor_WhiteSpaceLastName_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentException>(() => new FullName("Jimmy?", "    "));
    }

    [Fact]
    public void FullNameConstructor_WhiteSpaceFirstName_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentException>(() => new FullName("   ", "Johns?"));
    }
}