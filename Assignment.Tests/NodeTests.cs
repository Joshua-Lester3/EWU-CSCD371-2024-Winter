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

    private Node<string> CreateTestNode()
    {
        Node<string> node = new("Hi");
        node.Append("jimbobby");
        node.Append("I'm");
        node.Append("jimbob");
        return node;
    }
}