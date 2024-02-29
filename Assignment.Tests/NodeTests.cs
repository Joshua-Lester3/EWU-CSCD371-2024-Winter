using System.Collections.Generic;
using System.Collections;
using Xunit;

namespace Assignment.Tests;

public class NodeTests
{
    [Fact]
    public void ForEach_GenericIEnumerator_Success()
    {
        // Arrange
        Node<string> node = new("Hi");
        node.Append("jimbobby");
        node.Append("I'm");
        node.Append("jimbob");

        // Act
        string forEachResult = string.Join(" ", node);

        // Assert
        string expected = "Hi jimbob I'm jimbobby";
        Assert.Equal(expected, forEachResult);
    }

    [Fact]
    public void ForEach_NonGenericIEnumerator_Success()
    {
        // Arrange
        Node<string> node = new("Hi");
        node.Append("jimbobby");
        node.Append("I'm");
        node.Append("jimbob");

        // Act
        string forEachResult = string.Empty;
        foreach (string element in ((IEnumerable)node))
        {
            forEachResult += element + " ";
        }

        // Assert
        string expected = "Hi jimbob I'm jimbobby ";
        Assert.Equal(expected, forEachResult);
    }

    [Fact]
    public void ChildItems_ThreeItems_Success()
    {
        // Arrange
        Node<string> node = new("Hi");
        node.Append("jimbobby");
        node.Append("I'm");
        node.Append("jimbob");

        // Act
        IEnumerable<string> childItems = node.ChildItems(4);

        // Assert
        IEnumerable<string> expected = new[]
        {
            "jimbob", "I'm", "jimbobbyy"
        };
        Assert.Equal(expected, childItems);
    }
}