using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Diagnostics.Metrics;

namespace Assignment;
public class Node<T> : IEnumerable<T>
{
    public Node<T> Next { get; private set; }

    public T Element { get; }

    public Node(T element)
    {
        Element = element;
        Next = this;
    }

    public override string? ToString()
    {
        return Element?.ToString();
    }

    public void Append(T element)
    {
        if (Exists(element))
        {
            throw new InvalidOperationException($"This linked list already contains {element}");
        }

        Node<T> node = new(element)
        {
            Next = this.Next
        };
        Next = node;
    }

    // You do not need to worry about garbage collection because
    //      there are no static or local variable references that you can
    //      control from this class. It would be on whoever has those references.
    //      Additionally, you do not need to worry about setting each Node back on
    //      itself because as long as there are no static or local variable references
    //      to any of them, they will be garbage collected.
    public void Clear()
    {
        Next = this;
    }

    public bool Exists(T element)
    {
        Node<T> temporaryNode = this;
        do
        {
            bool isFound = temporaryNode.Element?.Equals(element) ?? false;
            if (isFound)
            {
                return true;
            }
            temporaryNode = temporaryNode.Next;
        } while (temporaryNode != this);
        return false;
    }

    public IEnumerator<T> GetEnumerator()
    {
        Node<T> currentNode = this;
        do
        {
            yield return currentNode.Element;
            currentNode = currentNode.Next;
        } while (currentNode != this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    // This method is complicated because the requirements say to go off
    // of Assignment5's Node implementation, which specified we must Append
    // each new element after the initial Node. It makes making a sub-linked list
    // a little more difficult than it should be.
    // I'm also returning another linked list because it makes sense to me that
    // a Node class would return an IEnumerable with underlying type Node.
    public IEnumerable<T> ChildItems(int maximum)
    {
        if (maximum < 1)
        {
            throw new ArgumentException($"{nameof(maximum)} cannot be less than 1.");
        }
        if (Next == this)
        {
            return new List<T>(0);
        }
        Node<T> currentNode = Next;
        Node<T> children = new(currentNode.Element);
        int counter = 2;
        currentNode = currentNode.Next;
        while (currentNode != this && counter < maximum)
        {
            children.Append(currentNode.Element);
            children = children.Next;
            currentNode = currentNode.Next;
            counter++;
        }
        children = children.Next;
        return children;
    }
}
