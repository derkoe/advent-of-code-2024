public class Day08
{
    public static void Run()
    {
        var input = File.ReadAllLines("day08/input.txt");
        char[][] map = input.Select(line => line.ToCharArray()).ToArray();
        // restructure to Dictionary with char as key and list of (line, col) as value, filter '.'
        var nodes = new Dictionary<char, List<(int, int)>>();
        for (int line = 0; line < map.Length; line++)
        {
            for (int col = 0; col < map[line].Length; col++)
            {
                char c = map[line][col];
                if (char.IsLetterOrDigit(c))
                {
                    if (!nodes.ContainsKey(c))
                    {
                        nodes[c] = new List<(int, int)>();
                    }
                    nodes[c].Add((line, col));
                }
            }
        }

        Console.WriteLine(Part1(nodes));
        Console.WriteLine(Part2(nodes));
    }


    public static int Part1(Dictionary<char, List<(int, int)>> nodes)
    {
        var antinodes = new HashSet<(int, int)>();
        foreach (var node in nodes)
        {
            foreach (var pos1 in node.Value)
            {
                foreach (var pos2 in node.Value)
                {
                    if (pos1 != pos2)
                    {
                        var x = pos1.Item1 * 2 - pos2.Item1;
                        var y = pos1.Item2 * 2 - pos2.Item2;
                        if (x >= 0 && x < 50 && y >= 0 && y < 50)
                        {
                            antinodes.Add((x, y));
                        }
                    }
                }
            }
        }

        return antinodes.Count;
    }

    // Part 2
    // anitnodes are all the nodes plus the 
    // an antinode occurs at any grid position exactly in line with at least two antennas of the same frequency, regardless of distance
    public static int Part2(Dictionary<char, List<(int, int)>> nodes)
    {
        var antinodes = new HashSet<(int, int)>();
        foreach (var node in nodes)
        {
            antinodes.UnionWith(node.Value);
            foreach (var pos1 in node.Value)
            {
                foreach (var pos2 in node.Value)
                {
                    if (pos1 != pos2)
                    {
                        var x = pos1.Item1 + pos1.Item1 - pos2.Item1;
                        var y = pos1.Item1 + pos1.Item1 - pos2.Item2;
                        while (x >= 0 && x < 50 && y >= 0 && y < 50)
                        {
                            antinodes.Add((x, y));
                            x += pos1.Item1 - pos2.Item1;
                            y += pos1.Item2 - pos2.Item2;
                        }
                    }
                }
            }
        }
        return antinodes.Count;
    }
}