public class Day19
{
    public static void Run()
    {
        var lines = File.ReadAllLines("day19/input.txt");
        var towelPatterns = new HashSet<string>();
        var designs = new List<string>();
        int i = 0;

        // Read towel patterns
        while (i < lines.Length)
        {
            var line = lines[i];
            i++;
            if (string.IsNullOrWhiteSpace(line))
                break;
            var patterns = line.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (var pattern in patterns)
            {
                towelPatterns.Add(pattern);
            }
        }

        // Read designs
        while (i < lines.Length)
        {
            var line = lines[i].Trim();
            i++;
            if (!string.IsNullOrEmpty(line))
            {
                designs.Add(line);
            }
        }

        int possibleCount = 0;
        foreach (var design in designs)
        {
            var memo = new Dictionary<string, bool>();
            if (CanFormDesign(design, towelPatterns, memo))
            {
                possibleCount++;
            }
        }

        Console.WriteLine(possibleCount);

        long totalWays = 0;
        foreach (var design in designs)
        {
            var memo = new Dictionary<string, long>();
            long ways = CountWays(design, towelPatterns, memo);
            totalWays += ways;
        }

        Console.WriteLine(totalWays);
    }

    private static bool CanFormDesign(string design, HashSet<string> patterns, Dictionary<string, bool> memo)
    {
        if (design == "") return true;
        if (memo.ContainsKey(design)) return memo[design];

        foreach (var pattern in patterns)
        {
            if (design.StartsWith(pattern))
            {
                if (CanFormDesign(design.Substring(pattern.Length), patterns, memo))
                {
                    memo[design] = true;
                    return true;
                }
            }
        }

        memo[design] = false;
        return false;
    }

    private static long CountWays(string design, HashSet<string> patterns, Dictionary<string, long> memo)
    {
        if (design == "") return 1;
        if (memo.ContainsKey(design)) return memo[design];

        long total = 0;
        foreach (var pattern in patterns)
        {
            if (design.StartsWith(pattern))
            {
                total += CountWays(design.Substring(pattern.Length), patterns, memo);
            }
        }

        memo[design] = total;
        return total;
    }
}