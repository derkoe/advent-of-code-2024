public class Day16
{
    public static void Run()
    {
        var lines = File.ReadAllLines("day16/input.txt");
        int rows = lines.Length;
        int cols = lines[0].Length;
        char[,] maze = new char[rows, cols];
        int startX = 0, startY = 0, endX = 0, endY = 0;


        // Load maze and find start and end positions
        for (int i = 0; i < rows; i++)
        {
            var line = lines[i];
            for (int j = 0; j < cols; j++)
            {
                maze[i, j] = line[j];
                if (maze[i, j] == 'S')
                {
                    startX = i;
                    startY = j;
                }
                else if (maze[i, j] == 'E')
                {
                    endX = i;
                    endY = j;
                }
            }
        }
        Part1(maze, startX, startY, endX, endY);
        Part2(maze, startX, startY, endX, endY);
    }

    public static void Part1(char[,] maze, int startX, int startY, int endX, int endY)
    {
        // Directions: 0-North, 1-East, 2-South, 3-West
        int[] dx = { -1, 0, 1, 0 };
        int[] dy = { 0, 1, 0, -1 };
        int rows = maze.GetLength(0);
        int cols = maze.GetLength(1);

        var pq = new SortedSet<(int cost, int x, int y, int dir)>();
        var visited = new HashSet<(int x, int y, int dir)>();
        pq.Add((0, startX, startY, 1)); // Start facing East

        while (pq.Count > 0)
        {
            var current = pq.Min;
            pq.Remove(current);
            int cost = current.cost;
            int x = current.x;
            int y = current.y;
            int dir = current.dir;

            if (x == endX && y == endY)
            {
                Console.WriteLine("Part 1: " + cost);
                return;
            }

            if (visited.Contains((x, y, dir)))
                continue;

            visited.Add((x, y, dir));

            // Move forward
            int nx = x + dx[dir];
            int ny = y + dy[dir];
            if (nx >= 0 && nx < rows && ny >= 0 && ny < cols && maze[nx, ny] != '#')
            {
                pq.Add((cost + 1, nx, ny, dir));
            }

            // Turn left
            int leftDir = (dir + 3) % 4;
            pq.Add((cost + 1000, x, y, leftDir));

            // Turn right
            int rightDir = (dir + 1) % 4;
            pq.Add((cost + 1000, x, y, rightDir));
        }

        Console.WriteLine("Part 1: No path found.");
    }


    public static void Part2(char[,] maze, int startX, int startY, int endX, int endY)
    {
        int rows = maze.GetLength(0);
        int cols = maze.GetLength(1);

        // Directions: 0-North, 1-East, 2-South, 3-West
        int[] dx = { -1, 0, 1, 0 };
        int[] dy = { 0, 1, 0, -1 };

        var pq = new SortedSet<(int cost, int x, int y, int dir)>();
        var nodeInfo = new Dictionary<(int x, int y, int dir), NodeInfo>();
        int minTotalCost = int.MaxValue;

        pq.Add((0, startX, startY, 1)); // Start facing East

        while (pq.Count > 0)
        {
            var current = pq.Min;
            pq.Remove(current);
            int cost = current.cost;
            int x = current.x;
            int y = current.y;
            int dir = current.dir;

            if (x == endX && y == endY)
            {
                if (cost < minTotalCost)
                {
                    minTotalCost = cost;
                }
            }

            if (nodeInfo.TryGetValue((x, y, dir), out var info))
            {
                if (cost > info.Cost)
                    continue;
                else if (cost == info.Cost)
                {
                    // Equal cost, do not update predecessors
                }
                else
                {
                    info.Cost = cost;
                    info.Predecessors.Clear();
                }
            }
            else
            {
                info = new NodeInfo(cost);
                nodeInfo[(x, y, dir)] = info;
            }

            // Move forward
            int nx = x + dx[dir];
            int ny = y + dy[dir];
            if (nx >= 0 && nx < rows && ny >= 0 && ny < cols && maze[nx, ny] != '#')
            {
                int newCost = cost + 1;
                var neighbor = (nx, ny, dir);
                if (!nodeInfo.ContainsKey(neighbor) || newCost <= nodeInfo[neighbor].Cost)
                {
                    if (!nodeInfo.ContainsKey(neighbor))
                    {
                        nodeInfo[neighbor] = new NodeInfo(newCost);
                        nodeInfo[neighbor].Predecessors.Add((x, y, dir));
                        pq.Add((newCost, nx, ny, dir));
                    }
                    else if (newCost < nodeInfo[neighbor].Cost)
                    {
                        nodeInfo[neighbor].Cost = newCost;
                        nodeInfo[neighbor].Predecessors.Clear();
                        nodeInfo[neighbor].Predecessors.Add((x, y, dir));
                        pq.Add((newCost, nx, ny, dir));
                    }
                    else if (newCost == nodeInfo[neighbor].Cost)
                    {
                        nodeInfo[neighbor].Predecessors.Add((x, y, dir));
                    }
                }
            }

            // Turn left
            int leftDir = (dir + 3) % 4;
            int leftCost = cost + 1000;
            var leftNode = (x, y, leftDir);
            if (!nodeInfo.ContainsKey(leftNode) || leftCost <= nodeInfo[leftNode].Cost)
            {
                if (!nodeInfo.ContainsKey(leftNode))
                {
                    nodeInfo[leftNode] = new NodeInfo(leftCost);
                    nodeInfo[leftNode].Predecessors.Add((x, y, dir));
                    pq.Add((leftCost, x, y, leftDir));
                }
                else if (leftCost < nodeInfo[leftNode].Cost)
                {
                    nodeInfo[leftNode].Cost = leftCost;
                    nodeInfo[leftNode].Predecessors.Clear();
                    nodeInfo[leftNode].Predecessors.Add((x, y, dir));
                    pq.Add((leftCost, x, y, leftDir));
                }
                else if (leftCost == nodeInfo[leftNode].Cost)
                {
                    nodeInfo[leftNode].Predecessors.Add((x, y, dir));
                }
            }

            // Turn right
            int rightDir = (dir + 1) % 4;
            int rightCost = cost + 1000;
            var rightNode = (x, y, rightDir);
            if (!nodeInfo.ContainsKey(rightNode) || rightCost <= nodeInfo[rightNode].Cost)
            {
                if (!nodeInfo.ContainsKey(rightNode))
                {
                    nodeInfo[rightNode] = new NodeInfo(rightCost);
                    nodeInfo[rightNode].Predecessors.Add((x, y, dir));
                    pq.Add((rightCost, x, y, rightDir));
                }
                else if (rightCost < nodeInfo[rightNode].Cost)
                {
                    nodeInfo[rightNode].Cost = rightCost;
                    nodeInfo[rightNode].Predecessors.Clear();
                    nodeInfo[rightNode].Predecessors.Add((x, y, dir));
                    pq.Add((rightCost, x, y, rightDir));
                }
                else if (rightCost == nodeInfo[rightNode].Cost)
                {
                    nodeInfo[rightNode].Predecessors.Add((x, y, dir));
                }
            }
        }

        // Collect all end nodes with minimal total cost
        var endNodes = new List<(int x, int y, int dir)>();
        foreach (var kvp in nodeInfo)
        {
            var node = kvp.Key;
            var info = kvp.Value;
            if (node.x == endX && node.y == endY && info.Cost == minTotalCost)
            {
                endNodes.Add(node);
            }
        }

        // Backtrack to find all tiles on minimal paths
        var tilesOnMinimalPaths = new HashSet<(int x, int y)>();
        var stack = new Stack<(int x, int y, int dir)>();
        var visitedNodes = new HashSet<(int x, int y, int dir)>();

        foreach (var endNode in endNodes)
        {
            stack.Push(endNode);
        }

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            if (visitedNodes.Contains(current))
                continue;
            visitedNodes.Add(current);
            tilesOnMinimalPaths.Add((current.x, current.y));
            var info = nodeInfo[current];
            foreach (var pred in info.Predecessors)
            {
                stack.Push(pred);
            }
        }

        int totalTiles = tilesOnMinimalPaths.Count;
        System.Console.WriteLine("Part 2: " + totalTiles);
    }

    class NodeInfo
    {
        public int Cost { get; set; }
        public List<(int x, int y, int dir)> Predecessors { get; set; }
        public NodeInfo(int cost)
        {
            Cost = cost;
            Predecessors = new List<(int x, int y, int dir)>();
        }
    }
}