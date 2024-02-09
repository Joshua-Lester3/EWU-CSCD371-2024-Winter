using Xunit;

namespace GenericsHomework.Tests;
public class NodeTests
{
    [Fact]
    public void Element_NodeInitialized_ReturnsCorrectType()
    {
        Node<string> node = new();
        Assert.True(node.Element is string);
    }
}
