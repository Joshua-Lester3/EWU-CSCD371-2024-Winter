using Xunit;
using Moq;
using Logger.Entity;

namespace Logger.Tests;

public class EntityTests
{
#pragma warning disable CS8618 // This will never be null in the tests that use it because of private InitIEntity
    private Mock<IEntity> _EntityMock;
#pragma warning restore CS8618 // This will never be null in the tests that use it because of private InitIEntity

    private void InitIEntity()
    {
        _EntityMock = new Mock<IEntity>();
    }

    #region IEntity Tests
    [Fact]
    public void Name_Init_Success()
    {
        InitIEntity();
        _EntityMock
            .Setup(x => x.Name)
            .Returns("Jimmy");
        IEntity entity = _EntityMock.Object;
        Assert.Equal("Jimmy", _EntityMock.Object.Name);
    }

    [Fact]
    public void Id_Init_Success()
    {
        InitIEntity();
        Guid guid = Guid.NewGuid();
        _EntityMock
            .Setup(x => x.Id)
            .Returns(guid);
        Assert.Equal(guid, _EntityMock.Object.Id);
    }
    #endregion

    #region BaseEntity Tests
    [Fact]
    public void Id_Inits_IsGuid()
    {
        Guid guid = Guid.NewGuid();
        BaseEntityTester tester = new(guid);
        Assert.Equal(guid.ToString(), ((IEntity)tester).Id.ToString());
    }
    private sealed record class BaseEntityTester(Guid Id) : BaseEntity(Id)
    {
        protected override string CalculateName()
        {
            throw new NotImplementedException();
        }
        
    }


    #endregion

    #region Person Tests

    [Fact]
    public void FullName_Instantiated_Success()
    {
        FullName fullName = new ("Jim", "John");
        PersonTester tester = new(Guid.NewGuid(), fullName);
        Assert.Equal(fullName, tester.FullName);
    }

    private sealed record class PersonTester(Guid Id, FullName FullName) : Person(Id, FullName)
    {
        protected override string CalculateName()
        {
            throw new NotImplementedException();
        }

    }

    [Fact]
    public void PersonEquals_SameData_ReturnsTrue()
    {
        Guid guid = Guid.NewGuid();
        FullName fullName = new("Jim", "John");
        PersonTester tester1 = new(guid, fullName);
        PersonTester tester2 = new(guid, fullName);
        Assert.True(tester1.Equals(tester2));
    }

    [Fact]
    public void PersonEquals_DifferentData_ReturnsFalse()
    {
        Guid guid1 = Guid.NewGuid();
        Guid guid2 = Guid.NewGuid();
        FullName fullName1 = new("Jim", "John");
        FullName fullName2 = new("Jimmy", "John");
        PersonTester tester1 = new(guid1, fullName1);
        PersonTester tester2 = new(guid2, fullName2);
        Assert.False(tester1.Equals(tester2));
    }
    #endregion

    #region Concrete Entity Tests
    [Fact]
    public void BookCalculateName_Instantiated_Success()
    {
        Book book = new(Guid.NewGuid());
        Assert.Equal(nameof(Book), ((IEntity)book).Name);
    }

    [Fact]
    public void StudentCalculateName_Instantiated_Success()
    {
        Student student = new(Guid.NewGuid(), new FullName("Jimmy", "Johns"));
        Assert.Equal(nameof(Student), ((IEntity)student).Name);
    }

    [Fact]
    public void EmployeeCalculateName_Instantiated_Success()
    {
        Employee employee = new(Guid.NewGuid(), new FullName("James", "Johns"));
        Assert.Equal(nameof(Employee), ((IEntity)employee).Name);
    }

    [Fact]
    public void BookEquals_SameData_ReturnsTrue()
    {
        Guid guid = Guid.NewGuid();
        Book book1 = new(guid);
        Book book2 = new(guid);
        Assert.True(book1.Equals(book2));
    }

    [Fact]
    public void BookEquals_DifferentData_ReturnsFalse()
    {
        Guid guid1 = Guid.NewGuid();
        Guid guid2 = Guid.NewGuid();
        Book book1 = new(guid1);
        Book book2 = new(guid2);
        Assert.False(book1.Equals(book2), guid1 + " " + guid2);
    }
    #endregion
}
