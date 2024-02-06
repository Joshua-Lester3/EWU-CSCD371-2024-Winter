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

        //Mock<BaseEntity> baseEntity = new Mock<BaseEntity>();
        //Guid guid = Guid.NewGuid();
        //Mock<IEntity> ientity = baseEntity.As<IEntity>();
        //ientity
        //    .Setup(x => x.Id)
        //    .Returns(guid);
        //Assert.True(ientity.Object.Id.Equals(guid));
        Guid guid = Guid.NewGuid();
        BaseEntityTester tester = new BaseEntityTester(guid);
        Assert.Equal(guid.ToString(), ((IEntity)tester).Id.ToString());
    }
    public record class BaseEntityTester(Guid Id) : BaseEntity(Id)
    {
        protected override string CalculateName()
        {
            throw new NotImplementedException();
        }
        
    }
    #endregion

    #region Concrete Entity Tests
    [Fact]
    public void BookCalculateName_Instantiated_Success()
    {
        Book book = new Book(Guid.NewGuid());
        Assert.Equal(nameof(Book), ((IEntity)book).Name);
    }

    [Fact]
    public void StudentCalculateName_Instantiated_Success()
    {
        Student student = new Student(Guid.NewGuid(), new FullName("Jimmy", "Johns"));
        Assert.Equal(nameof(Student), ((IEntity)student).Name);
    }

    [Fact]
    public void EmployeeCalculateName_Instantiated_Success()
    {
        Employee employee = new Employee(Guid.NewGuid(), new FullName("James", "Johns"));
        Assert.Equal(nameof(Employee), ((IEntity)employee).Name);
    }
    #endregion
}
