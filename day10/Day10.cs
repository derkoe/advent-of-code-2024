using System;
using System.Collections.Generic;
using System.IO;

public class Day10
{
    public static void Run()
    {
        // Read the map from "day10/input.txt"
        var lines = File.ReadAllLines("day10/input.txt");
        int rows = lines.Length;
        int cols = lines[0].Length;
        int[,] map = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            var line = lines[i];
            for (int j = 0; j < cols; j++)
            {
                map[i, j] = line[j] - '0';
            }
        }

        Part1(map, rows, cols);
        Part2(map, rows, cols);
    }

    private static void Part1(int[,] map, int rows, int cols)
    {
        int totalScore = 0;

        // Find all trailheads (positions with height 0)
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (map[i, j] == 0)
                {
                    int score = ComputeTrailheadScore(map, i, j);
                    totalScore += score;
                }
            }
        }

        Console.WriteLine("Part 1: " + totalScore);
    }
    private static void Part2(int[,] map, int rows, int cols)
    {
        int[,] paths = new int[rows, cols];

        // Starting from positions with height 9
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (map[i, j] == 9)
                {
                    paths[i, j] = 1;
                }
            }
        }

        // Process heights from 8 down to 0
        for (int height = 8; height >= 0; height--)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (map[i, j] == height)
                    {
                        int count = 0;
                        int[] dr = new int[] { -1, 1, 0, 0 };
                        int[] dc = new int[] { 0, 0, -1, 1 };

                        for (int d = 0; d < 4; d++)
                        {
                            int newRow = i + dr[d];
                            int newCol = j + dc[d];

                            if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols)
                            {
                                if (map[newRow, newCol] == height + 1)
                                {
                                    count += paths[newRow, newCol];
                                }
                            }
                        }
                        paths[i, j] = count;
                    }
                }
            }
        }

        int totalRating = 0;

        // Sum the ratings of all trailheads (height 0)
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (map[i, j] == 0)
                {
                    totalRating += paths[i, j];
                }
            }
        }

        Console.WriteLine("Part 2: " + totalRating);
    }

    private static int ComputeTrailheadScore(int[,] map, int startRow, int startCol)
    {
        int rows = map.GetLength(0);
        int cols = map.GetLength(1);
        // HashSet to keep track of positions of height 9 that can be reached
        HashSet<(int, int)> reachableNines = new HashSet<(int, int)>();

        // Queue for BFS, each item is (row, col, currentHeight)
        Queue<(int, int, int)> queue = new Queue<(int, int, int)>();
        queue.Enqueue((startRow, startCol, 0));

        // Visited positions with specific height to avoid loops
        HashSet<(int, int)> visited = new HashSet<(int, int)>();
        visited.Add((startRow, startCol));

        int[] dr = new int[] { -1, 1, 0, 0 }; // up, down
        int[] dc = new int[] { 0, 0, -1, 1 }; // left, right

        while (queue.Count > 0)
        {
            var (row, col, height) = queue.Dequeue();

            if (map[row, col] == 9)
            {
                reachableNines.Add((row, col));
                continue; // we don't need to explore further from height 9
            }

            for (int d = 0; d < 4; d++)
            {
                int newRow = row + dr[d];
                int newCol = col + dc[d];

                if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols)
                {
                    int newHeight = map[newRow, newCol];
                    if (newHeight == height + 1)
                    {
                        var state = (newRow, newCol);
                        if (!visited.Contains(state))
                        {
                            visited.Add(state);
                            queue.Enqueue((newRow, newCol, newHeight));
                        }
                    }
                }
            }
        }

        return reachableNines.Count;
    }
}