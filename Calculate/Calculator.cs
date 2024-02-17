using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculate;

public class Calculator
{
    public IReadOnlyDictionary<char, DelegateWithOut<int, int, int>> MathematicalOperations { get; } = CreateMathematicalOperations();

    private static IReadOnlyDictionary<char, DelegateWithOut<int, int, int>> CreateMathematicalOperations()
    {
        Dictionary<char, DelegateWithOut<int, int, int>> result = new();
        result.Add('+', Add);
        result.Add('-', Subtract);
        result.Add('*', Multiply);
        result.Add('/', Divide);
        return result;
    }

    public delegate void DelegateWithOut<T, U, V>(T t, U u, out V v);
    public static void Add(int left, int right, out int result) => result = left + right;

    public static void Divide(int left, int right, out int result) => result = left / right;

    public static void Multiply(int left, int right, out int result) => result = left * right;

    public static void Subtract(int left, int right, out int result) => result = left - right;
}