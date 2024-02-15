using Xunit;

namespace GenericsHomework.Tests;

public class VennTests
{
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
}