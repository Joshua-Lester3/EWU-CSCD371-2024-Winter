using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

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

    public IEnumerable<T> ChildItems(int maximum)
    {
        Node<T> currentNode = Next;
        int counter = 0;
        T[] arrayOfChildren = new T[maximum - 1];
        while (currentNode != this && counter < maximum - 1)
        {
            arrayOfChildren[counter] = currentNode.Element;
            currentNode = currentNode.Next;
            counter++;
        }
        Node<T> childrenResult = new(arrayOfChildren[0]);
        for (counter = arrayOfChildren.Count() - 1; counter > 0; counter--)
        {
            childrenResult.Append(arrayOfChildren[counter]);
        }
        return childrenResult;
    }
}
