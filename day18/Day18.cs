// ...existing code...
using System;
using System.IO;
using System.Collections.Generic;

public class Day18
{
    public static void Run()
    {
        int gridSize = 71;
        bool[,] grid = new bool[gridSize, gridSize];

        var lines = File.ReadAllLines("day18/input.txt");
        int numBytes = 1024;

        for (int i = 0; i < numBytes && i < lines.Length; i++)
        {
            var parts = lines[i].Split(',');
            int x = int.Parse(parts[0]);
            int y = int.Parse(parts[1]);
            grid[x, y] = true; // Mark as corrupted
        }

        int steps = FindShortestPath(grid);
        Console.WriteLine($"Minimum number of steps needed to reach the exit: {steps}");
    }

    static int FindShortestPath(bool[,] grid)
    {
        int gridSize = grid.GetLength(0);
        var directions = new (int x, int y)[] { (1, 0), (-1, 0), (0, 1), (0, -1) };
        var queue = new Queue<(int x, int y, int steps)>();
        var visited = new bool[gridSize, gridSize];

        queue.Enqueue((0, 0, 0));
        visited[0, 0] = true;

        while (queue.Count > 0)
        {
            var (x, y, steps) = queue.Dequeue();

            if (x == gridSize - 1 && y == gridSize - 1)
                return steps;

            foreach (var (dx, dy) in directions)
            {
                int newX = x + dx;
                int newY = y + dy;

                if (newX >= 0 && newX < gridSize && newY >= 0 && newY < gridSize)
                {
                    if (!grid[newX, newY] && !visited[newX, newY])
                    {
                        visited[newX, newY] = true;
                        queue.Enqueue((newX, newY, steps + 1));
                    }
                }
            }
        }

        return -1; // Path not found
    }
}
