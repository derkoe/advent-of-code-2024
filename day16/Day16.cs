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

        // Directions: 0-North, 1-East, 2-South, 3-West
        int[] dx = { -1, 0, 1, 0 };
        int[] dy = { 0, 1, 0, -1 };

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
                System.Console.WriteLine(cost);
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

        System.Console.WriteLine("No path found.");
    }
}