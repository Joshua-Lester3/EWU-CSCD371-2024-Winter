using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculate;

public class Calculator
{
    public static void Add(int left, int right, out int result) => result = left + right;

    public static void Divide(int left, int right, out int result) => result = left / right;

    public static int Multiply(int left, int right, out int result) => result = left * right;

    public static int Subtract(int left, int right, out int result) => result = left - right;
}