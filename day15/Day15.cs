public class Day15
{
    public static void Run()
    {
        Console.Clear();
        Part1();
        Part2();
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

    public static void Part1(bool printMap = false)
    {
        if (printMap)
        {
            Console.Clear();
        }

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
            if (printMap)
            {
                // print map
                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"Move: {move}");
                PrintMap(map, rows, columns);
                Thread.Sleep(100);
            }
        }
        Console.WriteLine($"Part 1: {CalcGPSPositionSum(map)}");
    }

    static void PrintMap(Map map, int rows, int cols, bool wide = false)
    {
        for (int y = 0; y < rows; y++)
        {
            var line = "";
            for (int x = 0; x < cols; x++)
            {
                if (map.robot == (y, x))
                {
                    line += '@';
                }
                else if (map.boxes.Contains((y, x)))
                {
                    line += wide ? '[' : 'O';
                }
                else if (wide && map.boxes.Contains((y, x - 1)))
                {
                    line += "]";
                }
                else if (map.walls.Contains((y, x)))
                {
                    line += '#';
                }
                else
                {
                    line += '.';
                }
            }
            Console.WriteLine(line);
        }
    }

    public static void Part2()
    {
        // in part 2 all walls are double as wide (with the same height)
        // all boxes are double as wide (with the same height) - when a box is moved up/down half-overlapping boxes are also moved
        var file = File.ReadAllText("day15/input.txt");
        var parts = file.Split("\n\n");
        var mapStr = parts[0].Split("\n");
        var rows = mapStr.Length;
        var columns = mapStr[0].Length;
        var map = new Map();
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                if (mapStr[y][x] == '#')
                {
                    map.walls.Add((y, x * 2));
                    map.walls.Add((y, x * 2 + 1));
                }
                else if (mapStr[y][x] == '@')
                {
                    map.robot = (y, x * 2);
                }
                else if (mapStr[y][x] == 'O')
                {
                    map.boxes.Add((y, x * 2));
                }
            }
        }

        // var moveCount = 0;
        var moves = parts[1].Replace("\n", "");
        foreach (var move in moves)
        {
            // Console.SetCursorPosition(0, 1);
            // Console.WriteLine($"Move {moveCount}: {move} {map.robot}");
            // PrintMap(map, rows, columns * 2, true);
            // Console.ReadLine();

            switch (move)
            {
                case '>':
                    if (!map.walls.Contains((map.robot.x, map.robot.y + 1)))
                    {
                        int nextY = map.robot.y + 1;
                        while (map.boxes.Contains((map.robot.x, nextY)))
                        {
                            // boxes are now 2 wide
                            nextY += 2;
                        }
                        if (!map.walls.Contains((map.robot.x, nextY)))
                        {
                            for (int y = nextY - 2; y > map.robot.y; y = y - 2)
                            {
                                map.boxes.Remove((map.robot.x, y));
                                map.boxes.Add((map.robot.x, y + 1));
                            }
                            map.robot = (map.robot.x, map.robot.y + 1);
                        }
                    }
                    break;
                case '<':
                    if (!map.walls.Contains((map.robot.x, map.robot.y - 1)))
                    {
                        int nextY = map.robot.y - 2;
                        while (map.boxes.Contains((map.robot.x, nextY)))
                        {
                            // boxes are now 2 wide
                            nextY -= 2;
                        }
                        if (!map.walls.Contains((map.robot.x, nextY + 1)))
                        {
                            for (int y = nextY + 2; y < map.robot.y; y = y + 2)
                            {
                                map.boxes.Remove((map.robot.x, y));
                                map.boxes.Add((map.robot.x, y - 1));
                            }
                            map.robot = (map.robot.x, map.robot.y - 1);
                        }
                    }
                    break;
                case '^':
                    if (!map.walls.Contains((map.robot.x - 1, map.robot.y)))
                    {
                        var boxesToMove = new List<(int x, int y)>();
                        var nextLine = new List<(int x, int y)>();
                        if (map.boxes.Contains((map.robot.x - 1, map.robot.y)))
                        {
                            nextLine.Add((map.robot.x - 1, map.robot.y));
                        }
                        if (map.boxes.Contains((map.robot.x - 1, map.robot.y - 1)))
                        {
                            nextLine.Add((map.robot.x - 1, map.robot.y - 1));
                        }
                        boxesToMove.AddRange(nextLine);
                        if (nextLine.Count == 0)
                        {
                            map.robot = (map.robot.x - 1, map.robot.y);
                            break;
                        }

                        while (nextLine.Count > 0)
                        {
                            var currentLine = new List<(int x, int y)>(nextLine);
                            nextLine.Clear();
                            foreach (var box in currentLine)
                            {
                                if (map.walls.Contains((box.x - 1, box.y)) || map.walls.Contains((box.x - 1, box.y + 1)))
                                {
                                    boxesToMove.Clear();
                                    nextLine.Clear();
                                    break;
                                }

                                if (map.boxes.Contains((box.x - 1, box.y + 1)))
                                {
                                    nextLine.Add((box.x - 1, box.y + 1));
                                }
                                if (map.boxes.Contains((box.x - 1, box.y)))
                                {
                                    nextLine.Add((box.x - 1, box.y));
                                }
                                if (map.boxes.Contains((box.x - 1, box.y - 1)))
                                {
                                    nextLine.Add((box.x - 1, box.y - 1));
                                }
                            }
                            boxesToMove.AddRange(nextLine);
                        }

                        if (boxesToMove.Count > 0)
                        {
                            boxesToMove.Sort((a, b) => a.x.CompareTo(b.x));
                            for (int i = 0; i < boxesToMove.Count; i++)
                            {
                                map.boxes.Remove(boxesToMove[i]);
                                map.boxes.Add((boxesToMove[i].x - 1, boxesToMove[i].y));
                            }
                            map.robot = (map.robot.x - 1, map.robot.y);
                        }
                    }
                    break;
                case 'v':
                    if (!map.walls.Contains((map.robot.x + 1, map.robot.y)))
                    {
                        var boxesToMove = new List<(int x, int y)>();
                        var nextLine = new List<(int x, int y)>();
                        if (map.boxes.Contains((map.robot.x + 1, map.robot.y)))
                        {
                            nextLine.Add((map.robot.x + 1, map.robot.y));
                        }
                        if (map.boxes.Contains((map.robot.x + 1, map.robot.y - 1)))
                        {
                            nextLine.Add((map.robot.x + 1, map.robot.y - 1));
                        }
                        boxesToMove.AddRange(nextLine);
                        if (nextLine.Count == 0)
                        {
                            map.robot = (map.robot.x + 1, map.robot.y);
                            break;
                        }

                        while (nextLine.Count > 0)
                        {
                            var currentLine = new List<(int x, int y)>(nextLine);
                            nextLine.Clear();
                            foreach (var box in currentLine)
                            {
                                if (map.walls.Contains((box.x + 1, box.y)) || map.walls.Contains((box.x + 1, box.y + 1)))
                                {
                                    boxesToMove.Clear();
                                    nextLine.Clear();
                                    break;
                                }

                                if (map.boxes.Contains((box.x + 1, box.y + 1)))
                                {
                                    nextLine.Add((box.x + 1, box.y + 1));
                                }
                                if (map.boxes.Contains((box.x + 1, box.y)))
                                {
                                    nextLine.Add((box.x + 1, box.y));
                                }
                                if (map.boxes.Contains((box.x + 1, box.y - 1)))
                                {
                                    nextLine.Add((box.x + 1, box.y - 1));
                                }
                            }
                            boxesToMove.AddRange(nextLine);
                        }

                        if (boxesToMove.Count > 0)
                        {
                            boxesToMove.Sort((a, b) => b.x.CompareTo(a.x));
                            for (int i = 0; i < boxesToMove.Count; i++)
                            {
                                map.boxes.Remove(boxesToMove[i]);
                                map.boxes.Add((boxesToMove[i].x + 1, boxesToMove[i].y));
                            }
                            map.robot = (map.robot.x + 1, map.robot.y);
                        }
                    }
                    break;
            }
        }
        Console.WriteLine($"Part 2: {CalcGPSPositionSum(map)}");
    }
}