using System.Text;

public class Day20
{
    public struct Map
    {
        public (int x, int y) start;
        public (int x, int y) end;
        public bool[,] walls;
    }

    public static void Run()
    {
        var mapLines = File.ReadAllLines("day20/input.txt");
        bool[,] walls = new bool[mapLines.Length, mapLines[0].Length];
        (int x, int y) start = (-1, -1);
        (int x, int y) end = (-1, -1);

        for (int i = 0; i < mapLines.Length; i++)
        {
            var line = mapLines[i];
            for (int j = 0; j < line.Length; j++)
            {
                if (line[j] == 'S')
                    start = (i, j);
                else if (line[j] == 'E')
                    end = (i, j);
                else if (line[j] == '#')
                    walls[i, j] = true;
            }
        }
        var map = new Map { start = start, end = end, walls = walls };

        int shortestPath = FindShortestPath(map);
        Console.WriteLine($"Shortest path: {shortestPath}");

        var cheatsWith100 = 0;
        var maps = RemoveWallTile(map);
        foreach (var m in maps)
        {
            int saved = shortestPath - FindShortestPath(m);
            if (saved >= 100)
            {
                cheatsWith100++;
            }
        }
        Console.WriteLine($"Part 1: {cheatsWith100}");
    }

    // Generate a list of all possbile maps with one wall tile removed from the original map
    // The wall tile has to be between two empty tiles
    static List<Map> RemoveWallTile(Map map)
    {
        var directions = new (int x, int y)[] { (1, 0), (-1, 0), (0, 1), (0, -1) };
        var maps = new List<Map>();
        var seenMaps = new HashSet<string>();

        for (int i = 0; i < map.walls.GetLength(0); i++)
        {
            for (int j = 0; j < map.walls.GetLength(1); j++)
            {
                if (map.walls[i, j])
                {
                    foreach (var (dx, dy) in directions)
                    {
                        int x1 = i + dx;
                        int y1 = j + dy;
                        int x2 = i - dx;
                        int y2 = j - dy;

                        if (x1 >= 0 && x1 < map.walls.GetLength(0) && y1 >= 0 && y1 < map.walls.GetLength(1) && x2 >= 0 && x2 < map.walls.GetLength(0) && y2 >= 0 && y2 < map.walls.GetLength(1) && !map.walls[x1, y1] && !map.walls[x2, y2])
                        {
                            var newWalls = (bool[,])map.walls.Clone();
                            newWalls[i, j] = false;
                            var newMap = new Map { start = map.start, end = map.end, walls = newWalls };
                            var mapKey = newMap.walls.Cast<bool>().Aggregate(new StringBuilder(), (sb, b) => sb.Append(b ? '1' : '0')).ToString();
                            if (!seenMaps.Contains(mapKey))
                            {
                                maps.Add(newMap);
                                seenMaps.Add(mapKey);
                            }
                        }
                    }
                }
            }
        }

        return maps;
    }

    static int FindShortestPath(Map map)
    {
        var directions = new (int x, int y)[] { (1, 0), (-1, 0), (0, 1), (0, -1) };
        var queue = new Queue<(int x, int y, int steps)>();
        var visited = new bool[map.walls.GetLength(0), map.walls.GetLength(1)];

        queue.Enqueue((map.start.x, map.start.y, 0));
        visited[map.start.x, map.start.y] = true;

        while (queue.Count > 0)
        {
            var (x, y, steps) = queue.Dequeue();

            if (x == map.end.x && y == map.end.y)
            {
                return steps;
            }

            foreach (var (dx, dy) in directions)
            {
                int newX = x + dx;
                int newY = y + dy;

                if (newX >= 0 && newX < map.walls.GetLength(0) && newY >= 0 && newY < map.walls.GetLength(1) && !map.walls[newX, newY] && !visited[newX, newY])
                {
                    queue.Enqueue((newX, newY, steps + 1));
                    visited[newX, newY] = true;
                }
            }
        }

        return -1;
    }

    public static void PrintMap(Map map)
    {
        for (int i = 0; i < map.walls.GetLength(0); i++)
        {
            for (int j = 0; j < map.walls.GetLength(1); j++)
            {
                if (map.start == (i, j))
                    Console.Write('S');
                else if (map.end == (i, j))
                    Console.Write('E');
                else if (map.walls[i, j])
                    Console.Write('#');
                else
                    Console.Write('.');
            }
            Console.WriteLine();
        }
    }
}