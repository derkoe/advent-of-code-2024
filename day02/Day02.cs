

class Day02
{
    static bool IsSafeReport(System.Collections.Generic.List<int> levels)
    {
        bool? isIncreasing = null;

        for (int i = 1; i < levels.Count; i++)
        {
            int diff = Math.Abs(levels[i] - levels[i - 1]);
            if (diff < 1 || diff > 3)
            {
                return false;
            }
            if (levels[i] >= levels[i - 1])
            {
                if (isIncreasing == false)
                {
                    return false;
                }
                isIncreasing = true;
            }
            else
            {
                if (isIncreasing == true)
                {
                    return false;
                }
                isIncreasing = false;
            }
        }

        return true;
    }

    public static void Run()
    {
        string[] lines = File.ReadAllLines("day02/input.txt");
        int safeReportCount = 0;

        foreach (var line in lines)
        {
            var levels = line.Split(' ').Select(int.Parse).ToList();
            if (IsSafeReport(levels))
            {
                safeReportCount++;
            }
        }

        Console.WriteLine($"Part 1: {safeReportCount}");
    }

}