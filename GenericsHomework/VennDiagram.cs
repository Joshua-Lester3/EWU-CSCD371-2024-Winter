using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericsHomework;

public class VennDiagram<T>
{
    public Node<Circle<T>> Circles { get; private set; }
    public VennDiagram(Circle<T> circle)
    {
        ArgumentNullException.ThrowIfNull(circle);
        Circles = new(circle);
    }

    public void AddCircle(Circle<T> circle)
    {
        ArgumentNullException.ThrowIfNull(circle);
        Circles.Append(circle);
    }

    public override string ToString()
    {
        string initialElement = Circles.Element.ToString();
        StringBuilder result = new($"{{{initialElement}}}");
        Node<Circle<T>> currentNode = Circles.Next;
        while (currentNode != Circles)
        {
            string element = currentNode.Element.ToString();
            result.Append($", {{{element}}}");
            currentNode = currentNode.Next;
        }
        return result.ToString();
    }
}
