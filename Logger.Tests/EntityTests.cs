using Xunit;
using Moq;
using Logger.Entity;

namespace Logger.Tests;

public class EntityTests
{
    private Mock<IEntity> _EntityMock;
    public EntityTests()
    {
        _EntityMock = new Mock<IEntity>();
    }

    [Fact]
    public void Name_Init_Success()
    {
        _EntityMock
            .Setup(x => x.Name)
            .Returns("Jimmy");
        IEntity entity = _EntityMock.Object;
        Assert.Equal("Jimmy", _EntityMock.Object.Name);
    }

    [Fact]
    public void Id_Init_Success()
    {
        Guid guid = Guid.NewGuid();
        _EntityMock
            .Setup(x => x.Id)
            .Returns(guid);
        Assert.Equal(guid, _EntityMock.Object.Id);
    }

    [Fact]
    public void Id_Inits_IsGuid()
    {
        Mock<BaseEntity> baseEntity = new Mock<BaseEntity>();
        Guid guid = Guid.NewGuid();
        Mock<IEntity> ientity = baseEntity.As<IEntity>();
        ientity
            .Setup(x => (x.Id))
            .Returns(guid);
        Assert.True(ientity.Object.Id.Equals(guid));
    }
}
