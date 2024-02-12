using Xunit;

namespace GenericsHomework.Tests;

public class VennTests
{
    #region Circle Tests
    [Fact]
    public void Name_Set_Success()
    {
        Circle<int> circle = new("Jimmy");
        Assert.Equal("Jimmy", circle.Name);
    }

    [Fact]
    public void Items_Empty_Success()
    {
        Circle<int> circle = new("Jimmy");
        Assert.Equal("", circle.GetItems());
    }
    #endregion
    //#region VennDiagram Tests
    //[Fact]
    ////public void Circles_Empty_Success()
    ////{
    ////    VennDiagram vennDiagram = new();
    ////    Assert.Equal(string.Empty, vennDiagram.Circles);
    ////}
    //#endregion
}