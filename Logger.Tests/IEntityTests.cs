using Xunit;
using Moq;

namespace Logger.Tests;

public class IEntityTests
{
    private Mock<IEntity> _EntityMock;
    public IEntityTests()
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
}
