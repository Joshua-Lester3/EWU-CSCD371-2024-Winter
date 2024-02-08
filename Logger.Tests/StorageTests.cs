using System.Runtime.CompilerServices;
using Logger.Entity;
using Xunit;

namespace Logger.Tests;

public class StorageTests
{
    #region private fields
    private readonly Storage _Storage;
    private readonly Employee _Employee1;
    private readonly Employee _Employee2;
    private readonly Book _Book1;
    private readonly Book _Book2;
    private readonly Student _Student1;
    private readonly Student _Student2;
    #endregion
    public StorageTests()
    {
        _Storage = new();
        _Employee1 = new(Guid.NewGuid(), new FullName("Jim", "John"));
        _Employee2 = new(Guid.NewGuid(), new FullName("Jimmy", "John"));
        _Book1 = new(Guid.NewGuid());
        _Book2 = new(Guid.NewGuid());
        _Student1 = new(Guid.NewGuid(), new FullName("Jim", "John"));
        _Student2 = new(Guid.NewGuid(), new FullName("Jimmy", "John"));
    }

    #region Employee

    [Fact]
    public void EmployeeContains_DoesContain_ReturnsTrue()
    {
        _Storage.Add(_Employee2);
        Assert.True(_Storage.Contains(_Employee2));
    }

    [Fact]
    public void EmployeeContains_DoesNotContain_ReturnsFalse()
    {
        _Storage.Add(_Employee2);
        Assert.False(_Storage.Contains(_Employee1));
    }

    [Fact]
    public void EmployeeRemove_DoesContain_DoesNotContainAnymore()
    {
        _Storage.Add(_Employee2);
        Assert.True(_Storage.Contains(_Employee2));
        _Storage.Remove(_Employee2);
        Assert.False(_Storage.Contains(_Employee2));
    }

    [Fact]
    public void EmployeeAdd_DoesNotContain_DoesContainNow()
    {
        Assert.False(_Storage.Contains(_Employee1));
        _Storage.Add(_Employee1);
        Assert.True(_Storage.Contains(_Employee1));
    }

    [Fact]
    public void EmployeeGet_DoesNotContain_ReturnsNull()
    {
        _Storage.Add(_Employee1);
        Assert.True(_Storage.Contains(_Employee1));
        Assert.Null(_Storage.Get(((IEntity)_Employee2).Id));
    }

    [Fact]
    public void EmployeeGet_DoesContain_ReturnsEmployee1()
    {
        _Storage.Add(_Employee1);
        Assert.Equal(_Employee1, _Storage.Get(((IEntity)_Employee1).Id));
    }
    #endregion

    #region Book

    [Fact]
    public void BookContains_DoesContain_ReturnsTrue()
    {
        _Storage.Add(_Book2);
        Assert.True(_Storage.Contains(_Book2));
    }

    [Fact]
    public void BookContains_DoesNotContain_ReturnsFalse()
    {
        _Storage.Add(_Book1);
        Assert.False(_Storage.Contains(_Book2));
    }

    [Fact]
    public void BookRemove_DoesContain_DoesNotContainAnymore()
    {
        _Storage.Add(_Book2);
        Assert.True(_Storage.Contains(_Book2));
        _Storage.Remove(_Book2);
        Assert.False(_Storage.Contains(_Book2));
    }

    [Fact]
    public void BookAdd_DoesNotContain_DoesContainNow()
    {
        Assert.False(_Storage.Contains(_Book2));
        _Storage.Add(_Book2);
        Assert.True(_Storage.Contains(_Book2));
    }

    [Fact]
    public void BookGet_DoesNotContain_ReturnsNull()
    {
        _Storage.Add(_Book2);
        Assert.True(_Storage.Contains(_Book2));
        Assert.Null(_Storage.Get(((IEntity)_Book1).Id));
    }

    [Fact]
    public void BookGet_DoesContain_ReturnsEmployee1()
    {
        _Storage.Add(_Book2);
        Assert.Equal(_Book2, _Storage.Get(((IEntity)_Book2).Id));
    }
    #endregion

    #region Student
    [Fact]
    public void StudentContains_DoesContain_ReturnsTrue()
    {
        _Storage.Add(_Student2);
        Assert.True(_Storage.Contains(_Student2));
    }

    [Fact]
    public void StudentContains_DoesNotContain_ReturnsFalse()
    {
        _Storage.Add(_Student1);
        Assert.False(_Storage.Contains(_Student2));
    }

    [Fact]
    public void StudentRemove_DoesContain_DoesNotContainAnymore()
    {
        _Storage.Add(_Student2);
        Assert.True(_Storage.Contains(_Student2));
        _Storage.Remove(_Student2);
        Assert.False(_Storage.Contains(_Student2));
    }

    [Fact]
    public void StudentAdd_DoesNotContain_DoesContainNow()
    {
        Assert.False(_Storage.Contains(_Student2));
        _Storage.Add(_Student2);
        Assert.True(_Storage.Contains(_Student2));
    }

    [Fact]
    public void StudentGet_DoesNotContain_ReturnsNull()
    {
        _Storage.Add(_Student2);
        Assert.True(_Storage.Contains(_Student2));
        Assert.Null(_Storage.Get(((IEntity)_Student1).Id));
    }

    [Fact]
    public void StudentGet_DoesContain_ReturnsEmployee1()
    {
        _Storage.Add(_Student2);
        Assert.Equal(_Student2, _Storage.Get(((IEntity)_Student2).Id));
    }
    #endregion
}