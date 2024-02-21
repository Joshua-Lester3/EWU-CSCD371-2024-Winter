namespace Calculate;

public class Calculator
{
    public IReadOnlyDictionary<char, Func<int, int, int>> MathematicalOperations { get; } = 
        new Dictionary<char, Func<int, int, int>>()
        {
            { '+', Add },
            { '-', Subtract },
            { '*', Multiply },
            { '/', Divide }
        };

    public static int Add(int left, int right) => left + right;

    public static int Divide(int left, int right) => left / right;

    public static int Multiply(int left, int right) => left * right;

    public static int Subtract(int left, int right) => left - right;

    public bool TryCalculate(string input, out int result)
    {
        result = 0;
        string[] inputSplit = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

        if (inputSplit.Length != 3 || !TryParseInt(inputSplit[0], out int operand1)
            || !TryParseInt(inputSplit[2], out int operand2))
        {
            return false;
        }

        if (char.TryParse(inputSplit[1], out char key)
            && MathematicalOperations.TryGetValue(key, out Func<int, int, int>? operation))
        {
            result = operation(operand1, operand2);
            return true;
        }

        return false;
    }

    private static bool TryParseInt(string input, out int result)
    {
        result = 0;
        for (int index = input.Length - 1, multiplier = 1; index >= 0; index--, multiplier *= 10)
        {
            int appended = TryParseIntHelper(input[index]);
            if (appended < 0)
            {
                result = 0;
                return false;
            }
            result += appended * multiplier;
        }
        return true;
    }

    private static int TryParseIntHelper(char character)
    {
        return character switch
        {
            '0' => 0,
            '1' => 1,
            '2' => 2,
            '3' => 3,
            '4' => 4,
            '5' => 5,
            '6' => 6,
            '7' => 7,
            '8' => 8,
            '9' => 9,
            _ => -1
        };
    }
}