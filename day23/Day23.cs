public class Day23
{
    public static void Run()
    {
        var input = File.ReadAllLines("day23/input.txt");
        var connections = input.Select(line => line.Trim().Split('-')).ToList();

        var graph = new Dictionary<string, HashSet<string>>();

        foreach (var connection in connections)
        {
            if (!graph.ContainsKey(connection[0]))
                graph[connection[0]] = new HashSet<string>();
            if (!graph.ContainsKey(connection[1]))
                graph[connection[1]] = new HashSet<string>();

            graph[connection[0]].Add(connection[1]);
            graph[connection[1]].Add(connection[0]);
        }

        Part1(graph);

        Part2(graph);
    }

    private static void Part1(Dictionary<string, HashSet<string>> graph)
    {
        var setsOfThree = new HashSet<string>();

        foreach (var node in graph.Keys)
        {
            foreach (var neighbor1 in graph[node])
            {
                foreach (var neighbor2 in graph[node])
                {
                    if (neighbor1 != neighbor2 && graph[neighbor1].Contains(neighbor2))
                    {
                        var set = new List<string> { node, neighbor1, neighbor2 };
                        set.Sort();
                        var setString = string.Join(",", set);
                        setsOfThree.Add(setString);
                    }
                }
            }
        }

        int setOfThreeWithT = setsOfThree.Count(set => set.Contains(",t") || set.StartsWith("t"));
        Console.WriteLine("Part 1: " + setOfThreeWithT);
    }

    private static void Part2(Dictionary<string, HashSet<string>> graph)
    {
        var largestSet = new List<string>();

        void FindComputerSets(List<string> potentialSet, List<string> remainingNodes, List<string> skipNodes)
        {
            if (remainingNodes.Count == 0 && skipNodes.Count == 0)
            {
                if (potentialSet.Count > largestSet.Count)
                {
                    largestSet = new List<string>(potentialSet.Order());
                }
                return;
            }

            for (int i = 0; i < remainingNodes.Count; i++)
            {
                var node = remainingNodes[i];
                var newPotentialSet = new List<string>(potentialSet) { node };
                var newRemainingNodes = remainingNodes.Where(n => graph[node].Contains(n)).ToList();
                var newSkipNodes = skipNodes.Where(n => graph[node].Contains(n)).ToList();

                FindComputerSets(newPotentialSet, newRemainingNodes, newSkipNodes);

                remainingNodes.Remove(node);
                skipNodes.Add(node);
            }
        }

        var nodes = graph.Keys.ToList();
        FindComputerSets(new List<string>(), nodes, new List<string>());
        Console.WriteLine("Part 2: " + string.Join(",", largestSet));
    }
}