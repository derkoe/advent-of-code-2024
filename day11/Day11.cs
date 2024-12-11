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
        Part2(stones);
    }

    private static void Part1(List<string> stones)
    {
        int blinks = 25;
        for (int i = 0; i < blinks; i++)
        {
            List<string> newStones = new List<string>();

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

    private static void Part2(List<string> stones)
    {
        var stoneCounts = new Dictionary<string, long>();
        for (int i = 0; i < stones.Count; i++)
        {
            if (stoneCounts.ContainsKey(stones[i]))
            {
                stoneCounts[stones[i]]++;
            }
            else
            {
                stoneCounts[stones[i]] = 1;
            }
        }

        int blinks = 75;
        for (int i = 0; i < blinks; i++)
        {
            var newStoneCounts = new Dictionary<string, long>();

            foreach (var stoneEntry in stoneCounts)
            {
                var stone = stoneEntry.Key;
                if (stone == "0")
                {
                    newStoneCounts["1"] = newStoneCounts.GetValueOrDefault("1", 0) + stoneEntry.Value;
                }
                else if (stone.Length % 2 == 0)
                {
                    // Rule 2: Split the stone
                    int mid = stone.Length / 2;
                    string leftHalf = stone.Substring(0, mid).TrimStart('0');
                    string rightHalf = stone.Substring(mid).TrimStart('0');

                    if (leftHalf == "") leftHalf = "0";
                    if (rightHalf == "") rightHalf = "0";

                    newStoneCounts[leftHalf] = newStoneCounts.GetValueOrDefault(leftHalf, 0) + stoneEntry.Value;
                    newStoneCounts[rightHalf] = newStoneCounts.GetValueOrDefault(rightHalf, 0) + stoneEntry.Value;
                }
                else
                {
                    // Rule 3: Multiply by 2024
                    var number = BigInteger.Parse(stone);
                    number *= 2024;
                    newStoneCounts[number.ToString()] = newStoneCounts.GetValueOrDefault(number.ToString(), 0) + stoneEntry.Value;
                }
            }

            stoneCounts = newStoneCounts;
        }

        Console.WriteLine("Part 2: " + stoneCounts.Aggregate(0L, (acc, entry) => acc + entry.Value));
    }
}