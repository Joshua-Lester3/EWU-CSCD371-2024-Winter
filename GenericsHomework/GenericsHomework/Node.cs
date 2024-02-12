using System.Collections;
using System.Collections.Generic;

namespace GenericsHomework;
public class Node<T> : ICollection<T>
{
    public Node<T> Next { get; private set; }

    public T Element { get; }
    public int Count { get; }
    public bool IsReadOnly { get; }

    public Node(T element)
    {
        Element = element;
        Next = this;
    }


    public override string ToString()
    {
        return Element?.ToString() ?? throw new InvalidOperationException($"{nameof(Element)} is null.");
    }

    public void Append(T element)
    {
        if (Exists(element))
        {
            throw new InvalidOperationException($"This linked list already contains {element}");
        }
        Node<T> node = new(element);
        node.Next = Next;
        Next = node;
    }

    // You do not need to worry about garbage collection because
    // there are no static or local variable references that you can
    // control from this class. It would be on whoever has those references
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

    public void Add(T item)
    {
        Append(item);
    }

    public bool Contains(T item)
    {
        return Exists(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public bool Remove(T item)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}