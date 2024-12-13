using System;
using System.IO;
using System.Collections.Generic;

public class Day13
{
    public static void Run()
    {
        var machines = new List<Machine>();
        var lines = File.ReadAllLines("day13/input.txt");
        for (int i = 0; i < lines.Length; i += 4)
        {
            var machine = new Machine();

            var buttonA = lines[i].Split(new[] { "Button A: ", "X+", ", Y+", "" }, StringSplitOptions.RemoveEmptyEntries);
            machine.Ax = int.Parse(buttonA[0]);
            machine.Ay = int.Parse(buttonA[1]);

            var buttonB = lines[i + 1].Split(new[] { "Button B: ", "X+", ", Y+", "" }, StringSplitOptions.RemoveEmptyEntries);
            machine.Bx = int.Parse(buttonB[0]);
            machine.By = int.Parse(buttonB[1]);

            var prize = lines[i + 2].Split(new[] { "Prize: ", "X=", ", Y=", "" }, StringSplitOptions.RemoveEmptyEntries);
            machine.Px = int.Parse(prize[0]);
            machine.Py = int.Parse(prize[1]);

            machines.Add(machine);
        }

        int totalPrizes = 0;
        int totalTokens = 0;

        foreach (var machine in machines)
        {
            int minTokens = int.MaxValue;
            bool canWin = false;
            for (int nA = 0; nA <= 100; nA++)
            {
                for (int nB = 0; nB <= 100; nB++)
                {
                    int x = nA * machine.Ax + nB * machine.Bx;
                    int y = nA * machine.Ay + nB * machine.By;

                    if (x == machine.Px && y == machine.Py)
                    {
                        int tokens = nA * 3 + nB * 1;
                        if (tokens < minTokens)
                        {
                            minTokens = tokens;
                            canWin = true;
                        }
                    }
                }
            }
            if (canWin)
            {
                totalPrizes++;
                totalTokens += minTokens;
            }
        }

        Console.WriteLine($"Prizes won: {totalPrizes}");
        Console.WriteLine($"Minimum tokens required: {totalTokens}");
    }
}

public class Machine
{
    public int Ax, Ay, Bx, By, Px, Py;
}