using System.Reflection.Metadata;

public class Day12
{
    private static readonly int[] DR = { -1, 1, 0, 0 };
    private static readonly int[] DC = { 0, 0, -1, 1 };

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
        var regionType = new Dictionary<int, char>();
        var area = new Dictionary<int, int>();
        var perimeter = new Dictionary<int, int>();
        var sides = new Dictionary<int, int>();


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
                    sides[regionId] = 0;
                    DFS(map, visited, r, c, type, regionId, area, perimeter, sides);
                }
            }
        }

        var price = 0;
        var priceWithSides = 0;

        foreach (var id in area.Keys)
        {
            Console.WriteLine($"Region {regionType[id]}: Area = {area[id]}, Perimeter = {perimeter[id]}, Sides = {sides[id]}");
            price += area[id] * perimeter[id];
            priceWithSides += area[id] * sides[id];
        }

        Console.WriteLine($"Part 1: {price}");
        Console.WriteLine($"Part 2: {priceWithSides}");
    }

    static void DFS(char[,] map, int[,] visited, int r, int c, char type, int regionId,
        Dictionary<int, int> area, Dictionary<int, int> perimeter, Dictionary<int, int> sides)
    {
        var stack = new Stack<(int, int)>();
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
                int nr = row + DR[i];
                int nc = col + DC[i];

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

            // calculate corners of the region - corners can be outside corders or inside corners
            int corners = 0;
            // case 1 - top left corner of the region, entries to the top and left are not the same type
            if (row == 0 && col == 0 || (row == 0 && col > 0 && map[row, col - 1] != type) || (col == 0 && row > 0 && map[row - 1, col] != type) || (row > 0 && col > 0 && map[row, col - 1] != type && map[row - 1, col] != type))
            {
                corners++;
            }
            // case 2 - top right corner of the region, entries to the top and right are not the same type
            if (row == 0 && col == cols - 1 || (row == 0 && col < cols - 1 && map[row, col + 1] != type) || (col == cols - 1 && row > 0 && map[row - 1, col] != type) || (row > 0 && col < cols - 1 && map[row, col + 1] != type && map[row - 1, col] != type))
            {
                corners++;
            }
            // case 3 - bottom left corner of the region, entries to the bottom and left are not the same type
            if (row == rows - 1 && col == 0 || (row == rows - 1 && col > 0 && map[row, col - 1] != type) || (col == 0 && row < rows - 1 && map[row + 1, col] != type) || (row < rows - 1 && col > 0 && map[row, col - 1] != type && map[row + 1, col] != type))
            {
                corners++;
            }
            // case 4 - bottom right corner of the region, entries to the bottom and right are not the same type
            if (row == rows - 1 && col == cols - 1 || (row == rows - 1 && col < cols - 1 && map[row, col + 1] != type) || (col == cols - 1 && row < rows - 1 && map[row + 1, col] != type) || (row < rows - 1 && col < cols - 1 && map[row, col + 1] != type && map[row + 1, col] != type))
            {
                corners++;
            }
            // case 5 - inside corner top right - cell to the top and the right are the same type but the cell top right is not
            if (row > 0 && col < cols - 1 && map[row, col + 1] == type && map[row - 1, col] == type && map[row - 1, col + 1] != type)
            {
                corners++;
            }
            // case 6 - inside corner top left - cell to the top and the left are the same type but the cell top left is not
            if (row > 0 && col > 0 && map[row, col - 1] == type && map[row - 1, col] == type && map[row - 1, col - 1] != type)
            {
                corners++;
            }
            // case 7 - inside corner bottom right - cell to the bottom and the right are the same type but the cell bottom right is not
            if (row < rows - 1 && col < cols - 1 && map[row, col + 1] == type && map[row + 1, col] == type && map[row + 1, col + 1] != type)
            {
                corners++;
            }
            // case 8 - inside corner bottom left - cell to the bottom and the left are the same type but the cell bottom left is not
            if (row < rows - 1 && col > 0 && map[row, col - 1] == type && map[row + 1, col] == type && map[row + 1, col - 1] != type)
            {
                corners++;
            }

            sides[regionId] += corners;
        }
    }

}