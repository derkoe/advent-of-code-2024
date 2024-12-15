public class Day15
{
    public static void Run()
    {
        Part1();
    }

    struct Map
    {
        public (int x, int y) robot;
        public HashSet<(int x, int y)> boxes = new HashSet<(int, int)>();
        public HashSet<(int x, int y)> walls = new HashSet<(int, int)>();

        public Map()
        {
        }
    }

    // The lanternfish use their own custom Goods Positioning System (GPS for short) to track the locations of the boxes. The GPS coordinate of a box is equal to 100 times its distance from the top edge of the map plus its distance from the left edge of the map. (This process does not stop at wall tiles; measure all the way to the edges of the map.)
    static int CalcGPSPositionSum(Map map)
    {
        int sum = 0;
        foreach (var box in map.boxes)
        {
            sum += 100 * box.x + box.y;
        }
        return sum;
    }

    public static void Part1()
    {
        // parse map from input "day15/input.txt"
        var file = File.ReadAllText("day15/input.txt");
        var parts = file.Split("\n\n");
        var mapStr = parts[0].Split("\n");
        var rows = mapStr.Length;
        var columns = mapStr[0].Length;
        var map = new Map();
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (mapStr[i][j] == '#')
                {
                    map.walls.Add((i, j));
                }
                else if (mapStr[i][j] == '@')
                {
                    map.robot = (i, j);
                }
                else if (mapStr[i][j] == 'O')
                {
                    map.boxes.Add((i, j));
                }
            }
        }

        Console.Clear();
        var moves = parts[1].Replace("\n", "");
        foreach (var move in moves)
        {
            switch (move)
            {
                case '>':
                    if (!map.walls.Contains((map.robot.x, map.robot.y + 1)))
                    {
                        int nextY = map.robot.y + 1;
                        while (map.boxes.Contains((map.robot.x, nextY)))
                        {
                            nextY++;
                        }
                        if (!map.walls.Contains((map.robot.x, nextY)) && !map.boxes.Contains((map.robot.x, nextY)))
                        {
                            for (int y = nextY - 1; y > map.robot.y; y--)
                            {
                                map.boxes.Remove((map.robot.x, y));
                                map.boxes.Add((map.robot.x, y + 1));
                            }
                            map.robot = (map.robot.x, map.robot.y + 1);
                        }
                    }
                    break;
                case '^':
                    if (!map.walls.Contains((map.robot.x - 1, map.robot.y)))
                    {
                        int nextX = map.robot.x - 1;
                        while (map.boxes.Contains((nextX, map.robot.y)))
                        {
                            nextX--;
                        }
                        if (!map.walls.Contains((nextX, map.robot.y)) && !map.boxes.Contains((nextX, map.robot.y)))
                        {
                            for (int x = nextX + 1; x < map.robot.x; x++)
                            {
                                map.boxes.Remove((x, map.robot.y));
                                map.boxes.Add((x - 1, map.robot.y));
                            }
                            map.robot = (map.robot.x - 1, map.robot.y);
                        }
                    }
                    break;
                case '<':
                    if (!map.walls.Contains((map.robot.x, map.robot.y - 1)))
                    {
                        int nextY = map.robot.y - 1;
                        while (map.boxes.Contains((map.robot.x, nextY)))
                        {
                            nextY--;
                        }
                        if (!map.walls.Contains((map.robot.x, nextY)) && !map.boxes.Contains((map.robot.x, nextY)))
                        {
                            for (int y = nextY + 1; y < map.robot.y; y++)
                            {
                                map.boxes.Remove((map.robot.x, y));
                                map.boxes.Add((map.robot.x, y - 1));
                            }
                            map.robot = (map.robot.x, map.robot.y - 1);
                        }
                    }
                    break;
                case 'v':
                    if (!map.walls.Contains((map.robot.x + 1, map.robot.y)))
                    {
                        int nextX = map.robot.x + 1;
                        while (map.boxes.Contains((nextX, map.robot.y)))
                        {
                            nextX++;
                        }
                        if (!map.walls.Contains((nextX, map.robot.y)) && !map.boxes.Contains((nextX, map.robot.y)))
                        {
                            for (int x = nextX - 1; x > map.robot.x; x--)
                            {
                                map.boxes.Remove((x, map.robot.y));
                                map.boxes.Add((x + 1, map.robot.y));
                            }
                            map.robot = (map.robot.x + 1, map.robot.y);
                        }
                    }
                    break;
            }
            // print map
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Move: {move}");
            for (int i = 0; i < mapStr.Length; i++)
            {
                for (int j = 0; j < mapStr[i].Length; j++)
                {
                    if (map.robot == (i, j))
                    {
                        Console.Write("@");
                    }
                    else if (map.boxes.Contains((i, j)))
                    {
                        Console.Write('O');
                    }
                    else if (map.walls.Contains((i, j)))
                    {
                        Console.Write('#');
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }
        Console.WriteLine($"Part 1: {CalcGPSPositionSum(map)}");
    }
}