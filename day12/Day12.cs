public class Day12
{
    public static void Run()
    {
        string[] lines = File.ReadAllLines("day12/input.txt");
        int rows = lines.Length;
        int cols = lines[0].Length;
        char[,] map = new char[rows, cols];

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                map[r, c] = lines[r][c];
            }
        }

        int[,] visited = new int[rows, cols];
        int regionId = 0;
        Dictionary<int, char> regionType = new Dictionary<int, char>();
        Dictionary<int, int> area = new Dictionary<int, int>();
        Dictionary<int, int> perimeter = new Dictionary<int, int>();

        int[] dr = { -1, 1, 0, 0 };
        int[] dc = { 0, 0, -1, 1 };

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (visited[r, c] == 0)
                {
                    regionId++;
                    char type = map[r, c];
                    regionType[regionId] = type;
                    area[regionId] = 0;
                    perimeter[regionId] = 0;
                    DFS(map, visited, r, c, type, regionId, area, perimeter, dr, dc);
                }
            }
        }

        var price = 0;

        foreach (var id in area.Keys)
        {
            Console.WriteLine($"Region {regionType[id]}: Area = {area[id]}, Perimeter = {perimeter[id]}");
            price += area[id] * perimeter[id];
        }

        Console.WriteLine($"Part 1: {price}");
    }

    static void DFS(char[,] map, int[,] visited, int r, int c, char type, int regionId,
        Dictionary<int, int> area, Dictionary<int, int> perimeter, int[] dr, int[] dc)
    {
        Stack<(int, int)> stack = new Stack<(int, int)>();
        stack.Push((r, c));

        int rows = map.GetLength(0);
        int cols = map.GetLength(1);

        while (stack.Count > 0)
        {
            var (row, col) = stack.Pop();
            if (visited[row, col] != 0)
                continue;

            visited[row, col] = regionId;
            area[regionId]++;

            int cellPerimeter = 4;

            for (int i = 0; i < 4; i++)
            {
                int nr = row + dr[i];
                int nc = col + dc[i];

                if (nr >= 0 && nr < rows && nc >= 0 && nc < cols)
                {
                    if (map[nr, nc] == type)
                    {
                        cellPerimeter--;
                        if (visited[nr, nc] == 0)
                            stack.Push((nr, nc));
                    }
                }
            }
            perimeter[regionId] += cellPerimeter;
        }
    }
}