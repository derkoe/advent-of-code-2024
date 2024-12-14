public class Day14
{
    struct Robot
    {
        public int x, y, vx, vy;
    }

    public static void Run()
    {
        Part1();
        Part2();
    }

    static void Part1()
    {
        var robots = parseRobots();
        var positions = CalcRobots(robots);
        Console.WriteLine("Part 1: " + CalcSafetyFactor(positions));
    }

    static void Part2()
    {
        var robots = parseRobots();

        // find lines of 30 in a row
        for (int iteration = 0; iteration <= 10403; iteration++)
        {
            var positions = CalcRobots(robots, iteration);
            for (int y = 0; y < 103; y++)
            {
                if (positions.Count(p => p.y == y) >= 30)
                {
                    // see if the items are all next to each other
                    var items = positions.Where(p => p.y == y).OrderBy(p => p.x).ToList();
                    bool found = true;
                    for (int i = 1; i < items.Count; i++)
                    {
                        if (items[i].x - items[i - 1].x != 1)
                        {
                            found = false;
                            break;
                        }
                    }
                    if (found)
                    {
                        Console.WriteLine("Part 2: " + iteration);
                        printRobots(positions);
                    }
                }
            }
        }
    }

    static List<Robot> parseRobots()
    {
        var robots = new List<Robot>();
        foreach (var line in File.ReadLines("day14/input.txt"))
        {
            var parts = line.Split(' ');
            var pParts = parts[0].Substring(2).Split(',');
            var vParts = parts[1].Substring(2).Split(',');

            int px = int.Parse(pParts[0]);
            int py = int.Parse(pParts[1]);
            int vx = int.Parse(vParts[0]);
            int vy = int.Parse(vParts[1]);

            robots.Add(new Robot { x = px, y = py, vx = vx, vy = vy });
        }
        return robots;
    }

    static List<(int x, int y)> CalcRobots(List<Robot> robots, int iteration = 100)
    {
        var positions = new List<(int x, int y)>();
        foreach (var robot in robots)
        {
            int x = (int)((robot.x + robot.vx * iteration) % 101);
            int y = (int)((robot.y + robot.vy * iteration) % 103);
            if (x < 0) x += 101;
            if (y < 0) y += 103;

            positions.Add((x, y));
        }
        return positions;
    }

    static int CalcSafetyFactor(List<(int x, int y)> robots)
    {
        int q1 = 0, q2 = 0, q3 = 0, q4 = 0;
        foreach (var (x, y) in robots)
        {
            if (x == 50 || y == 51)
                continue;
            if (x > 50 && y < 51)
                q1++;
            else if (x < 50 && y < 51)
                q2++;
            else if (x < 50 && y > 51)
                q3++;
            else if (x > 50 && y > 51)
                q4++;
        }

        int safetyFactor = q1 * q2 * q3 * q4;
        return safetyFactor;
    }

    static void printRobots(List<(int x, int y)> robots)
    {
        for (int y = 0; y < 103; y++)
        {
            for (int x = 0; x < 101; x++)
            {
                if (robots.Any(r => r.x == x && r.y == y))
                    Console.Write("#");
                else
                    Console.Write(" ");
            }
            Console.WriteLine();
        }
    }
}