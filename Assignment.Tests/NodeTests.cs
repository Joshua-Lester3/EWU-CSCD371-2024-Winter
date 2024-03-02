using System.Collections.Generic;
using System.Collections;
using Xunit;
using System;

namespace Assignment.Tests;

public class NodeTests
{
    [Fact]
    public void ForEach_GenericIEnumerator_Success()
    {
        // Arrange
        Node<string> node = CreateTestNode();

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
        Node<string> node = CreateTestNode();

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
        Node<string> node = CreateTestNode();

        // Act
        IEnumerable<string> childItems = node.ChildItems(4);

        // Assert
        IEnumerable<string> expected = new[]
        {
            "jimbob", "I'm", "jimbobby"
        };
        Assert.Equal(expected, childItems);
    }

    [Fact]
    public void ChildItems_InvalidMaximum_ThrowsIllegalArgumentException()
    {
        // Arrange
        Node<string> node = CreateTestNode();

        // Act

        // Assert
        Assert.Throws<ArgumentException>(() => node.ChildItems(0));
    }

    [Fact]
    public void ChildItems_MaximumIsOverNumberOfChildren_Success()
    {
        // Arrange
        Node<string> node = CreateTestNode();

        // Act
        IEnumerable<string> childItems = node.ChildItems(20);

        // Assert
        IEnumerable<string> expected = new[]
        {
            "jimbob", "I'm", "jimbobby"
        };
        Assert.Equal(expected, childItems);
    }

    [Fact]
    public void ChildItems_NextNodeIsThis_ReturnsEmptyArray()
    {
        // Arrange
        Node<string> node = new("I'm the only one!");

        // Act
        IEnumerable<string> children = node.ChildItems(2);

        // Assert
        Assert.Empty(children);
    }

    private static Node<string> CreateTestNode()
    {
        Node<string> node = new("Hi");
        node.Append("jimbobby");
        node.Append("I'm");
        node.Append("jimbob");
        return node;
    }

    #region Tests from assn5 to get good coverage
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
        Assert.Equal<Node<string>>(node, node.Next);
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

    [Fact]
    public void Exists_ElementIsNull_ThrowsInvalidOperationException()
    {
        Node<string> node = new(null!);
        Assert.False(node.Exists("Jimbobby"));
    }

    [Fact]
    public void ToString_ElementIsNull_ReturnsNull()
    {
        Node<string> node = new(null!);
        Assert.Null(node.ToString());
    }
    #endregion
}