using Xunit;

namespace GenericsHomework.Tests;

public class CircleTests
{
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

    [Fact]
    public void GetItems_InitialElementNull_ThrowsArgumentNullException()
    {
        Circle<string> circle = new("Jimbob");
        circle.AddItem(null!);
        Assert.Throws<InvalidOperationException>(() => circle.GetItems());
    }

    [Fact]
    public void GetItems_NonInitialElementNull_ThrowsArgumentNullException()
    {
        Circle<string> circle = new("Jimbob");
        circle.AddItem("Jimbobby");
        circle.AddItem(null!);
        Assert.Throws<InvalidOperationException>(() => circle.GetItems());
    }
}
