namespace Calculate;

public class Calculator
{
    public IReadOnlyDictionary<char, DelegateWithOut<int, int, int>> MathematicalOperations { get; } = 
        new Dictionary<char, DelegateWithOut<int, int, int>>()
        {
            { '+', Add },
            { '-', Subtract },
            { '*', Multiply },
            { '/', Divide }
        };

    public delegate void DelegateWithOut<TParamOne, TParamTwo, TParamThree>(TParamOne paramOne, TParamTwo paramTwo, out TParamThree paramThree);
    public static void Add(int left, int right, out int result) => result = left + right;

    public static void Divide(int left, int right, out int result) => result = left / right;

    public static void Multiply(int left, int right, out int result) => result = left * right;

    public static void Subtract(int left, int right, out int result) => result = left - right;

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
            && MathematicalOperations.TryGetValue(key, out DelegateWithOut<int, int, int>? operation))
        {
            operation(operand1, operand2, out result);
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