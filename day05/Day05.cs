

using System.Runtime.CompilerServices;

public class Day05
{
    public static void Run()
    {
        var input = File.ReadAllText("day05/input.txt");
        var inputParts = input.Split($"{Environment.NewLine}{Environment.NewLine}");
        var rules = new Dictionary<int, List<int>>();
        var pageNumberList = new List<List<int>>();

        inputParts[0].Split(Environment.NewLine).ToList().ForEach(x =>
        {
            var split = x.Split("|");
            var from = int.Parse(split[0]);
            var to = int.Parse(split[1]);
            rules.TryGetValue(from, out var list);
            if (!rules.ContainsKey(from))
            {
                rules[from] = new List<int>() { to };
            }
            else
            {
                rules[from].Add(to);
            }
        });
        inputParts[1].Split(Environment.NewLine).Where(x => !string.IsNullOrEmpty(x)).ToList().ForEach(x =>
        {
            pageNumberList.Add(x.Split(",").Select(int.Parse).ToList());
        });

        var validPageNumberList = new List<List<int>>();
        var invalidPageNumberList = new List<List<int>>();
        foreach (var pageNumbers in pageNumberList)
        {
            var valid = true;
            for (int i = 0; i < pageNumbers.Count - 1; i++)
            {
                if (!rules[pageNumbers[i]].Contains(pageNumbers[i + 1]))
                {
                    valid = false;
                }
            }
            if (valid)
            {
                validPageNumberList.Add(pageNumbers);
            }
            else
            {
                invalidPageNumberList.Add(pageNumbers);
            }
        }

        // Part 1: get the middle number of each valid pages list and add them
        var sum = 0;
        foreach (var pages in validPageNumberList)
        {
            sum += pages[pages.Count / 2];
        }
        Console.WriteLine($"Part 1: {sum}");

        // Part 2: reorder the invalid pages so that they are valid
        foreach (var pages in invalidPageNumberList)
        {
            while (!IsValid(rules, pages))
            {
                for (var i = 0; i < pages.Count; i++)
                {
                    var page = pages[i];

                    for (var j = i + 1; j < pages.Count; j++)
                    {
                        var page2 = pages[j];

                        if (!rules[page2].Contains(page))
                            continue;

                        pages[i] = page2;
                        pages[j] = page;
                        break;
                    }
                }
            }
        }
        var sum2 = 0;
        foreach (var pages in invalidPageNumberList)
        {
            sum2 += pages[pages.Count / 2];
        }
        Console.WriteLine($"Part 2: {sum2}");
    }

    private static bool IsValid(Dictionary<int, List<int>> rules, List<int> pages)
    {
        for (int i = 0; i < pages.Count - 1; i++)
        {
            if (!rules[pages[i]].Contains(pages[i + 1]))
            {
                return false;
            }
        }
        return true;
    }
}
