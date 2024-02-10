using Xunit;

namespace GenericsHomework.Tests;
public class NodeTests
{
    [Fact]
    public void Element_NodeInitialized_Success()
    {
        Node<string> node = new("Jimmy");
        Assert.Equal("Jimmy", node.Element);
    }

    [Fact]
    public void ToString_NonNullElement_Success()
    {
        Node<string> node = new("Jimmy");
        Assert.Equal("Jimmy", node.ToString());
    }

    [Fact]
    public void ToString_NullElement_ThrowsArgumentNullException()
    {
        Node<string> node = new(null!);
        Assert.Throws<InvalidOperationException>(() => node.ToString());
    }

    [Fact]
    public void Next_NoNextSet_ReturnsSameReference()
    {
        Node<string> node = new("Jimmy");
        Assert.Equal(node, node.Next);
    }

    [Fact]
    public void Next_NextSet_Success()
    {
        string element = "John";
        Node<string> node = new("Jimmy");
        node.Append(element);
        Assert.Equal(element, node.Next.Element);
    }

    [Fact]
    public void Append_NonNullElement_Success()
    {
        Node<string> node = new("Jimmy");
        node.Append("John");
        Assert.Equal("John", node.Next.Element);
        Assert.Equal(node, node.Next.Next);
    }

    [Fact]
    public void Clear_ListOfTwo_Success()
    {
        Node<string> node = new("Jimmy");
        node.Append("John");
        node.Clear();
        Assert.Equal(node, node.Next);
    }

    [Fact]
    public void Exists_DoesNotExist_ReturnsFalse()
    {
        Node<string> node = new("Jimmy");
        node.Append("John");
        Assert.False(node.Exists("Jimothy"));
    }

    [Fact]
    public void Exists_DoesExist_ReturnsTrue()
    {
        Node<string> node = new("Jimmy");
        node.Append("John");
        Assert.True(node.Exists("Jimmy"));
    }

    [Fact]
    public void Append_AppendingDuplicateValue_ThrowsInvalidOperationException()
    {
        Node<string> node = new("Jimmy");
        node.Append("John");
        Assert.Throws<InvalidOperationException>(() => node.Append("Jimmy"));
    }
}
