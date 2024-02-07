using System.Runtime.CompilerServices;
using Logger.Entity;
using Xunit;

namespace Logger.Tests;

public class StorageTests
{
    private readonly Storage _Storage;
    private readonly Employee _Employee1;
    private readonly Employee _Employee2;
    public StorageTests()
    {
        _Storage = new();
        _Employee1 = new Employee(Guid.NewGuid(), new FullName("Jim", "John"));
        _Employee2 = new Employee(Guid.NewGuid(), new FullName("Jimmy", "John"));
    }

    [Fact]
    public void Contains_DoesNotContain_ReturnsFalse()
    {
        _Storage.Add(_Employee2);
        Assert.False(_Storage.Contains(_Employee1));
    }
}