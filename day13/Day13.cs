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
            machine.Ax = long.Parse(buttonA[0]);
            machine.Ay = long.Parse(buttonA[1]);

            var buttonB = lines[i + 1].Split(new[] { "Button B: ", "X+", ", Y+", "" }, StringSplitOptions.RemoveEmptyEntries);
            machine.Bx = long.Parse(buttonB[0]);
            machine.By = long.Parse(buttonB[1]);

            var prize = lines[i + 2].Split(new[] { "Prize: ", "X=", ", Y=", "" }, StringSplitOptions.RemoveEmptyEntries);
            machine.Px = long.Parse(prize[0]);
            machine.Py = long.Parse(prize[1]);

            machines.Add(machine);
        }

        // Part 1
        SolveMachines(machines);

        // Part 2
        foreach (var machine in machines)
        {
            machine.Px += 10000000000000L;
            machine.Py += 10000000000000L;
        }
        SolveMachines(machines);
    }

    static void SolveMachines(List<Machine> machines)
    {
        int totalPrizes = 0;
        long totalTokens = 0;

        foreach (var machine in machines)
        {
            var result = Solve(machine.Ax, machine.Bx, machine.Px, machine.Ay, machine.By, machine.Py);
            if (result != null)
            {
                totalPrizes++;
                totalTokens += result.Item3;
            }
        }

        Console.WriteLine($"Prizes won: {totalPrizes}");
        Console.WriteLine($"Minimum tokens required: {totalTokens}");
    }

    static Tuple<long, long, long>? Solve(long a1, long b1, long c1, long a2, long b2, long c2)
    {
        long det = a1 * b2 - a2 * b1;
        if (det == 0) return null;

        long xNum = c1 * b2 - c2 * b1;
        long yNum = a1 * c2 - a2 * c1;

        if (xNum % det != 0 || yNum % det != 0) return null;

        long nA = xNum / det;
        long nB = yNum / det;

        if (nA < 0 || nB < 0) return null;

        long tokens = nA * 3 + nB * 1;

        return Tuple.Create(nA, nB, tokens);
    }
}

public class Machine
{
    public long Ax, Ay, Bx, By, Px, Py;
}