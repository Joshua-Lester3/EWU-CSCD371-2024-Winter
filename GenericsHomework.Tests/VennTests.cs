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
    public void Name_NullValue_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new Circle<string>(null!));
    }

    [Fact]
    public void AddItem_NonNullValue_Success()
    {
        Circle<string> circle = new("Jimmy");
        circle.AddItem("Jimbob");
        Assert.Equal("Jimbob", circle.Items!.Element);
    }

    [Fact]
    public void Items_Empty_Success()
    {
        Circle<int> circle = new("Jimmy");
        Assert.Equal("", circle.GetItems());
    }

    [Fact]
    public void Items_TwoItems_Success()
    {
        Circle<string> circle = new("Jimmy");
        circle.AddItem("jimmy's son");
        circle.AddItem("jimmy's daughter");
        Assert.Equal("jimmy's son, jimmy's daughter", circle.GetItems());
    }

    [Fact]
    public void ToStringOneItem_Success()
    {
        Circle<string> circle = new("Jimmy");
        Assert.Equal("Jimmy: ", circle.ToString());
    }

    [Fact]
    public void ToString_TwoItems_Success()
    {
        Circle<string> circle = new("Jimmy");
        circle.AddItem("jimmy's son");
        circle.AddItem("jimmy's daughter");
        Assert.Equal("Jimmy: jimmy's son, jimmy's daughter", circle.ToString());
    }
    #endregion
    #region VennDiagram Tests
    [Fact]
    public void ToString_OneEmptyCircle_Success()
    {
        Circle<string> circle = new("Jimmy's Dad");
        VennDiagram<string> vennDiagram = new(circle);
        Assert.Equal("{Jimmy's Dad: }", vennDiagram.ToString());
    }

    [Fact]
    public void AddCircle_NullCircle_ThrowsArgumentNullException()
    {
        VennDiagram<string> vennDiagram = new(new Circle<string>("Jimmy"));
        Assert.Throws<ArgumentNullException>(() => { vennDiagram.AddCircle(null!); });
    }

    [Fact]
    public void AddCircle_NonNullCircle_Success()
    {
        Circle<string> circle1 = new("Jimmy's Dad");
        VennDiagram<string> vennDiagram = new(circle1);
        Circle<string> circle2 = new("Jimmy's Mom");
        vennDiagram.AddCircle(circle2);
        Assert.True(vennDiagram.Circles.Exists(circle2));
    }

    [Fact]
    public void ToString_TwoCircles_Success()
    {
        Circle<string> circle1 = new("Jimmy's Dad");
        VennDiagram<string> vennDiagram = new(circle1);
        Circle<string> circle2 = new("Jimmy's Mom");
        vennDiagram.AddCircle(circle2);
        Assert.Equal("{Jimmy's Dad: }, {Jimmy's Mom: }", vennDiagram.ToString());
    }
    #endregion
}