using System;
using System.IO;
using System.Collections.Generic;

public class Day06
{
    public static void Run()
    {
        var map = File.ReadAllLines("day06/input.txt");
        int rows = map.Length;
        int cols = map[0].Length;

        // Directions: Up, Right, Down, Left
        int[] dx = { 0, 1, 0, -1 };
        int[] dy = { -1, 0, 1, 0 };
        char[] dirChars = { '^', '>', 'v', '<' };

        int x = 0, y = 0, dir = 0;

        // Find the guard's starting position and direction
        for (int i = 0; i < rows; i++)
        {
            for (int d = 0; d < 4; d++)
            {
                int idx = map[i].IndexOf(dirChars[d]);
                if (idx != -1)
                {
                    y = i;
                    x = idx;
                    dir = d;
                    break;
                }
            }
        }

        var visited = new HashSet<(int, int)>();
        visited.Add((x, y));

        // Part 1
        SimulateGuard(map, x, y, dir, visited);
        Console.WriteLine($"Part 1: (distinct positions visited): {visited.Count}");

        // Part 2
        int loopPositions = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                char cell = map[i][j];
                if (cell == '.' && (i != y || j != x))
                {
                    // Place an obstruction at (j, i)
                    char[] newMapRow = map[i].ToCharArray();
                    newMapRow[j] = '#';
                    string[] newMap = (string[])map.Clone();
                    newMap[i] = new string(newMapRow);

                    if (DoesGuardGetStuck(newMap, x, y, dir, dx, dy))
                    {
                        loopPositions++;
                    }
                }
            }
        }

        Console.WriteLine($"Part 2: (positions for new obstruction): {loopPositions}");
    }

    static void SimulateGuard(string[] map, int x, int y, int dir, HashSet<(int, int)> visited)
    {
        int rows = map.Length;
        int cols = map[0].Length;
        int[] dx = { 0, 1, 0, -1 };
        int[] dy = { -1, 0, 1, 0 };

        while (true)
        {
            int nx = x + dx[dir];
            int ny = y + dy[dir];

            // Check if next position is out of bounds (guard leaves the map)
            if (ny < 0 || ny >= rows || nx < 0 || nx >= cols)
                break;

            char nextCell = map[ny][nx];

            if (nextCell == '#')
            {
                // Turn right 90 degrees
                dir = (dir + 1) % 4;
            }
            else
            {
                // Move forward
                x = nx;
                y = ny;
                visited.Add((x, y));
            }
        }
    }

    static bool DoesGuardGetStuck(string[] map, int startX, int startY, int startDir, int[] dx, int[] dy)
    {
        int rows = map.Length;
        int cols = map[0].Length;
        var states = new HashSet<(int, int, int)>();
        int x = startX;
        int y = startY;
        int dir = startDir;

        while (true)
        {
            var state = (x, y, dir);
            if (states.Contains(state))
            {
                // Loop detected
                return true;
            }
            states.Add(state);

            int nx = x + dx[dir];
            int ny = y + dy[dir];

            // Check if next position is out of bounds
            if (ny < 0 || ny >= rows || nx < 0 || nx >= cols)
                return false;

            char nextCell = map[ny][nx];

            if (nextCell == '#')
            {
                // Turn right 90 degrees
                dir = (dir + 1) % 4;
            }
            else
            {
                // Move forward
                x = nx;
                y = ny;
            }
        }
    }
}