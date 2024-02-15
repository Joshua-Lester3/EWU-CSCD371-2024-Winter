using System.Globalization;
using System.Text;

namespace GenericsHomework;

public class Circle<T>
{
    public string Name { get; }
    public Node<T>? Items { get; private set; }

    public Circle(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public void AddItem(T item)
    {
        if (Items is null)
        {
            Items = new(item);
        } else
        {
            Items.Append(item);
        }
    }

    public string GetItems()
    {
        if (Items is null) { return string.Empty; }
        string initialElement = Items.Element?.ToString() ?? throw new InvalidOperationException(nameof(Items.Element));
        StringBuilder result = new(initialElement);
        Node<T> currentNode = Items.Next;
        while (currentNode != Items)
        {
            string element = currentNode.Element?.ToString() ?? throw new InvalidOperationException(nameof(currentNode.Element));
            result.Append(CultureInfo.CurrentCulture, $", {element}");
            currentNode = currentNode.Next;
        }
        return result.ToString();
    }

    public override string ToString()
    {
        return $"{Name}: {GetItems()}";
    }
}
