public record Calculation
{
    public required long Result;
    public required List<long> Numbers;
}

public enum Operation
{
    Add,
    Multiply,

    Concatenate
}


public class Day07
{
    static string toBaseThree(int number, int length)
    {
        string result = "";
        while (number > 0)
        {
            result = (number % 3).ToString() + result;
            number /= 3;
        }
        return result.PadLeft(length, '0');
    }

    public static bool isValid(long result, List<long> numbers, List<Operation> operations)
    {
        // Console.WriteLine("Checking " + string.Join(" ", numbers2) + " with " + string.Join(" ", operations));
        long currentResult = numbers[0];
        for (int i = 0; i < operations.Count; i++)
        {
            if (operations[i] == Operation.Add)
            {
                currentResult += numbers[i + 1];
            }
            else if (operations[i] == Operation.Multiply)
            {
                currentResult *= numbers[i + 1];
            }
            else if (operations[i] == Operation.Concatenate)
            {
                currentResult = long.Parse(currentResult.ToString() + numbers[i + 1].ToString());
            }
        }

        return currentResult == result;
    }

    public static void Run()
    {
        var input = File.ReadAllLines("day07/input.txt");
        var calculations = input.Select(line =>
        {
            var parts = line.Split(": ");
            return new Calculation
            {
                Result = long.Parse(parts[0]),
                Numbers = parts[1].Split(" ").Select(long.Parse).ToList()
            };
        }).ToArray();

        // Part 1: filter expressions that are valid - operations are  add (+) and multiply (*)
        var part1Sum = 0L;
        foreach (var calculation in calculations)
        {
            var possibleOperations = new List<List<Operation>>();
            for (int i = 0; i < Math.Pow(2, calculation.Numbers.Count - 1); i++)
            {
                string binary = Convert.ToString(i, 2).PadLeft(calculation.Numbers.Count - 1, '0');
                possibleOperations.Add(binary.Select(c => c == '0' ? Operation.Add : Operation.Multiply).ToList());
            }

            foreach (var operations in possibleOperations)
            {
                if (isValid(calculation.Result, calculation.Numbers, operations))
                {
                    part1Sum += calculation.Result;
                    break;
                }
            }
        }
        Console.WriteLine($"Part 1: {part1Sum}");

        // Part 2: the same as part one but with additional operation - concatenate
        var part2Sum = 0L;
        foreach (var calculation in calculations)
        {
            // Console.WriteLine("Calculating for " + calculation.Result + "(" + calculation.Numbers.Count + ")");

            var possibleOperations = new List<List<Operation>>();
            for (int i = 0; i < Math.Pow(3, calculation.Numbers.Count - 1); i++)
            {
                string ternary = toBaseThree(i, calculation.Numbers.Count - 1);
                possibleOperations.Add(ternary.Select(c =>
                {
                    if (c == '0') return Operation.Add;
                    if (c == '1') return Operation.Multiply;
                    return Operation.Concatenate;
                }).ToList());
            }

            foreach (var operations in possibleOperations)
            {
                // hanlde concatenate operation
                // var modified = handleConcatOperation(calculation.Numbers, operations);
                if (isValid(calculation.Result, calculation.Numbers, operations))
                {
                    part2Sum += calculation.Result;
                    break;
                }
            }
        }
        Console.WriteLine($"Part 2: {part2Sum}");

    }
}