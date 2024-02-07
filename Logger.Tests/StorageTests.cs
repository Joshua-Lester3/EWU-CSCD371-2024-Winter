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
    public void Contains_DoesContain_ReturnsTrue()
    {
        _Storage.Add(_Employee2);
        Assert.True(_Storage.Contains(_Employee2));
    }

    [Fact]
    public void Contains_DoesNotContain_ReturnsFalse()
    {
        _Storage.Add(_Employee2);
        Assert.False(_Storage.Contains(_Employee1));
    }

    [Fact]
    public void Remove_DoesContain_DoesNotContainAnymore()
    {
        _Storage.Add(_Employee2);
        Assert.True(_Storage.Contains(_Employee2));
        _Storage.Remove(_Employee2);
        Assert.False(_Storage.Contains(_Employee2));
    }

    [Fact]
    public void Add_DoesNotContain_DoesContainNow()
    {
        Assert.False(_Storage.Contains(_Employee1));
        _Storage.Add(_Employee1);
        Assert.True(_Storage.Contains(_Employee1));
    }

    [Fact]
    public void Get_DoesNotContain_ReturnsNull()
    {
        _Storage.Add(_Employee1);
        Assert.True(_Storage.Contains(_Employee1));
        Assert.Null(_Storage.Get(((IEntity)_Employee2).Id));
    }

    [Fact]
    public void Get_DoesContain_ReturnsEmployee1()
    {
        _Storage.Add(_Employee1);
        Assert.Equal(_Employee1, _Storage.Get(((IEntity)_Employee1).Id));
    }
}