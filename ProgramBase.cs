//namespace ConsoleUtilities
//{
//    public class ProgramBase
//    {
//        public Action<object?> WriteLine { get; init; } = Console.WriteLine;
//        public Func<string?> ReadLine { get; init; } = Console.ReadLine;

//        public void Run()
//        {
//            WriteLine("Welcome to the Calculator!");

//            while (true)
//            {
//                WriteLine("Please enter a calculation (or 'q' to quit):");
//                string? input = ReadLine();

//                if (string.IsNullOrEmpty(input) || input.Contains('q'))
//                {
//                    WriteLine("Exiting...");
//                    break;
//                }

//                if (TryCalculate(input, out int answer))
//                {
//                    WriteLine($"The answer is: {answer}");
//                }
//                else
//                {
//                    WriteLine("Invalid input. Please try again.");
//                }
//            }
//        }

//        private bool TryCalculate(string input, out int result)
//        {
//            result = 0;
//            string[] inputSplit = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

//            if (inputSplit.Length != 3 || !TryParseInt(inputSplit[0], out int operand1) || !TryParseInt(inputSplit[2], out int operand2))
//            {
//                return false;
//            }

//            if (char.TryParse(inputSplit[1], out char key)
//                && MathematicalOperations.TryGetValue(key, out DelegateWithOut<int, int, int>? operation))
//            {
//                operation(operand1, operand2, out result);
//                return true;
//            }

//            return false;
//        }

//        private bool TryParseInt(string input, out int result)
//        {
//            result = 0;
//            for (int index = input.Length - 1, multiplier = 1; index >= 0; index--, multiplier *= 10)
//            {
//                int appended = TryParseIntHelper(input[index]);
//                if (appended < 0)
//                {
//                    return false;
//                }
//                result += appended * multiplier;
//            }
//            return true;
//        }

//        private int TryParseIntHelper(char character)
//        {
//            return character switch
//            {
//                '0' => 0,
//                '1' => 1,
//                '2' => 2,
//                '3' => 3,
//                '4' => 4,
//                '5' => 5,
//                '6' => 6,
//                '7' => 7,
//                '8' => 8,
//                '9' => 9,
//                _ => -1
//            };
//        }

//        private IReadOnlyDictionary<char, DelegateWithOut<int, int, int>> MathematicalOperations { get; } = CreateMathematicalOperations();

//        private static IReadOnlyDictionary<char, DelegateWithOut<int, int, int>> CreateMathematicalOperations()
//        {
//            Dictionary<char, DelegateWithOut<int, int, int>> result = new()
//            {
//                { '+', Add },
//                { '-', Subtract },
//                { '*', Multiply },
//                { '/', Divide }
//            };
//            return result;
//        }

//        public delegate void DelegateWithOut<T, U, V>(T t, U u, out V v);
//        private static void Add(int left, int right, out int result) => result = left + right;
//        private static void Divide(int left, int right, out int result) => result = left / right;
//        private static void Multiply(int left, int right, out int result) => result = left * right;
//        private static void Subtract(int left, int right, out int result) => result = left - right;
//    }
//}
