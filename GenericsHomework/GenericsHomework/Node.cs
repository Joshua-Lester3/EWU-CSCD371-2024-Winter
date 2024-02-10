using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace GenericsHomework;
public class Node<T>
{
    public Node<T> Next { get; private set; }

    public T Element { get; }

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
        Node<T> temporaryNode = this.Next;
        while (temporaryNode != this)
        {
            bool isFound = temporaryNode.Element?.Equals(element) ?? throw new InvalidOperationException(nameof(Element));
            if (isFound)
            {
                return true;
            }
            temporaryNode = temporaryNode.Next;
        }
        return false;
    }
}