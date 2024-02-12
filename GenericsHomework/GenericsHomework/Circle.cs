using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericsHomework;

public class Circle<T>
{
    public string Name { get; }
    public Node<T>? Items { get; } = null;

    public Circle(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public string GetItems()
    {
        throw new NotImplementedException();
    }
}
