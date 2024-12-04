using System.Text.RegularExpressions;

public class Day03
{
    public static void Run()
    {
        var input = File.ReadAllText("day03/input.txt").Replace("\n", "");
        var mulPattern = @"mul\((\d+),(\d+)\)";
        var dontPattern = @"don't\(\)";
        var doPattern = @"do\(\)";
        var part1 = Regex.Matches(input, mulPattern)
            .Select(m => new { X = int.Parse(m.Groups[1].Value), Y = int.Parse(m.Groups[2].Value) })
            .Select(p => p.X * p.Y)
            .Sum();

        Console.WriteLine($"Part 1: {part1}");

        bool mulEnabled = true;
        int part2 = 0;
        while (input.Length > 0)
        {
            if (mulEnabled)
            {
                Match firstMul = Regex.Match(input, mulPattern);
                Match firstDont = Regex.Match(input, dontPattern);
                if (firstMul.Index < firstDont.Index || (firstMul.Success && !firstDont.Success))
                {
                    Console.WriteLine("Found mul:" + firstMul.Value);
                    part2 += int.Parse(firstMul.Groups[1].Value) * int.Parse(firstMul.Groups[2].Value);
                    input = input.Substring(firstMul.Index + firstMul.Length);
                }
                else
                {
                    mulEnabled = false;
                    Console.WriteLine("Found don't:" + firstDont.Index);
                    input = input.Substring(firstDont.Index + firstDont.Length);
                }
            }
            else
            {
                Match firstDo = Regex.Match(input, doPattern);
                if (firstDo.Success)
                {
                    mulEnabled = true;
                    Console.WriteLine("Found do:" + firstDo.Index);
                    input = input.Substring(firstDo.Index + firstDo.Length);
                }
                else
                {
                    break;
                }
            }
        }

        Console.WriteLine($"Part 2: {part2}");
    }

}

