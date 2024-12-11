using System.Numerics;

public class Day11
{
    public static void Run()
    {
        List<string> stones = new List<string>();

        // Read initial stones from the input file
        foreach (var line in File.ReadLines("day11/input.txt"))
        {
            var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            stones.AddRange(numbers);
        }

        Part1(stones);
        // Part2(stones.Select(BigInteger.Parse).ToList());
    }

    private static void Part1(List<string> stones)
    {
        int blinks = 25;
        for (int i = 0; i < blinks; i++)
        {
            List<string> newStones = new List<string>();
            Console.WriteLine($"Blink {i + 1}");
            Console.WriteLine("Stones: " + string.Join(", ", stones));

            foreach (var stone in stones)
            {
                if (stone == "0")
                {
                    // Rule 1: Replace 0 with 1
                    newStones.Add("1");
                }
                else if (stone.Length % 2 == 0)
                {
                    // Rule 2: Split the stone
                    int mid = stone.Length / 2;
                    string leftHalf = stone.Substring(0, mid).TrimStart('0');
                    string rightHalf = stone.Substring(mid).TrimStart('0');

                    if (leftHalf == "") leftHalf = "0";
                    if (rightHalf == "") rightHalf = "0";

                    newStones.Add(leftHalf);
                    newStones.Add(rightHalf);
                }
                else
                {
                    // Rule 3: Multiply by 2024
                    long number = long.Parse(stone);
                    number *= 2024;
                    newStones.Add(number.ToString());
                }
            }

            stones = newStones;
        }

        Console.WriteLine("Part 1: " + stones.Count);
    }

    private static void Part2(List<BigInteger> stones)
    {
        int blinks = 75;

        for (int i = 0; i < blinks; i++)
        {
            var newStones = new List<BigInteger>();

            foreach (var stone in stones)
            {
                var result = new List<BigInteger>();

                if (stone == 0)
                {
                    result.Add(1);
                }
                else if ((Math.Floor(BigInteger.Log10(stone)) + 1) % 2 == 0)
                {
                    var stoneStr = stone.ToString();
                    int mid = stoneStr.Length / 2;
                    string leftHalf = stoneStr.Substring(0, mid).TrimStart('0');
                    string rightHalf = stoneStr.Substring(mid).TrimStart('0');

                    if (leftHalf == "") leftHalf = "0";
                    if (rightHalf == "") rightHalf = "0";

                    result.Add(BigInteger.Parse(leftHalf));
                    result.Add(BigInteger.Parse(rightHalf));
                }
                else
                {
                    result.Add(stone * 2024L);
                }

                newStones.AddRange(result);
            }

            stones = newStones;
        }

        Console.WriteLine("Part 2: " + stones.Count);
    }
}