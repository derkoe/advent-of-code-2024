public class Day25
{
    public static void Run()
    {
        var blocks = File.ReadAllText("day25/input.txt").Split("\n\n");

        var locks = new List<int[]>();
        var keys = new List<int[]>();

        foreach (var block in blocks)
        {
            var lines = block.Split('\n');
            if (lines[0] == "#####")
            {
                var lck = new int[5] { 0, 0, 0, 0, 0 };
                for (int i = 1; i < lines.Length; i++)
                {
                    var line = lines[i];
                    for (int j = 0; j < line.Length; j++)
                    {
                        if (line[j] == '#')
                        {
                            lck[j] += 1;
                        }
                    }
                }
                locks.Add(lck);
            }
            else
            {
                var key = new int[5] { 0, 0, 0, 0, 0 };
                for (int i = 0; i < lines.Length - 1; i++)
                {
                    var line = lines[i];
                    for (int j = 0; j < line.Length; j++)
                    {
                        if (line[j] == '#')
                        {
                            key[j] += 1;
                        }
                    }
                }
                keys.Add(key);
            }
        }

        // check if the key fits the lock
        var fitCount = 0;
        foreach (var lck in locks)
        {
            foreach (var key in keys)
            {
                var fits = true;
                for (int i = 0; i < 5; i++)
                {
                    if (lck[i] + key[i] > 5)
                    {
                        fits = false;
                        break;
                    }
                }
                if (fits)
                {
                    fitCount++;
                }
            }
        }

        // Console.WriteLine("Locks:");
        // foreach (var lck in locks)
        // {
        //     Console.WriteLine(string.Join(",", lck));
        // }
        // Console.WriteLine("Keys:");
        // foreach (var key in keys)
        // {
        //     Console.WriteLine(string.Join(",", key));
        // }

        Console.WriteLine("Part 1: " + fitCount);

    }
}