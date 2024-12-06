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
            int idx = map[i].IndexOf('^');
            if (idx != -1)
            {
                y = i;
                x = idx;
                dir = 0;
                break;
            }
            idx = map[i].IndexOf('>');
            if (idx != -1)
            {
                y = i;
                x = idx;
                dir = 1;
                break;
            }
            idx = map[i].IndexOf('v');
            if (idx != -1)
            {
                y = i;
                x = idx;
                dir = 2;
                break;
            }
            idx = map[i].IndexOf('<');
            if (idx != -1)
            {
                y = i;
                x = idx;
                dir = 3;
                break;
            }
        }

        var visited = new HashSet<(int, int)>();
        visited.Add((x, y));

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

        Console.WriteLine($"Part 1: (distinct positions visited): {visited.Count}");
    }
}