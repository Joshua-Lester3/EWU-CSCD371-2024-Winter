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
            bool isFound = temporaryNode.Element?.Equals(element) ?? throw new InvalidOperationException(nameof(Element));
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
    // each new element after the initial element
    public IEnumerable<T> ChildItems(int maximum)
    {
        //Node<T> currentNode = Next;
        //if (currentNode == this)
        //{
        //    return Array.Empty<T>();
        //}
        //Node<T> children = new(currentNode.Element);
        //Stack<T> stack = new();
        //currentNode = currentNode.Next;
        //int counter = 2;
        //while (currentNode != this && counter < maximum)
        //{
        //    stack.Push(currentNode.Element);
        //    currentNode = currentNode.Next;
        //    counter++;
        //}

        //if (stack.Count > 0)
        //{
        //    while (stack.TryPop(out T? element))
        //    {
        //        children.Append(element);
        //    }
        //}
        //return children;

        Node<T> currentNode = Next;
        if (currentNode == this)
        {
            return Array.Empty<T>();
        }
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
