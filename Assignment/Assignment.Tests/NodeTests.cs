using Xunit;

namespace Assignment.Tests;

public class NodeTests
{
    [Fact]
    public void ForEach_NodeWithTwoChildren_Success()
    {
        // Arrange
        Node<int> node = new();
        string forEachResult = "";

        // Act

        string expected = "Hi jimbob I'm jimbobby";

        // Assert
        Assert.Equal(expected, forEachResult);
    }
}