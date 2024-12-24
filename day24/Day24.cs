public class Day24
{

    public static void Run()
    {
        var lines = File.ReadAllText("day24/input.txt");
        var wireValues = new Dictionary<string, bool>();
        var gates = new List<(string left, string gate, string right, string output)>();

        var parts = lines.Split("\n\n");

        foreach (var line in parts[0].Split("\n"))
        {
            if (string.IsNullOrEmpty(line)) continue;
            // Format: wireName: 0/1
            var nameValue = line.Split(": ");
            if (nameValue.Length < 2) break;
            wireValues[nameValue[0].Trim()] = nameValue[1].Trim() == "1";
        }
        foreach (var line in parts[1].Split("\n"))
        {
            if (string.IsNullOrEmpty(line)) continue;
            // Format: X GATE Y -> Z
            var seg = line.Split(new[] { "->" }, StringSplitOptions.None);
            var leftPart = seg[0].Trim().Split(' ');
            gates.Add((leftPart[0], leftPart[1], leftPart[2], seg[1].Trim()));
        }

        bool updated = true;
        while (updated)
        {
            updated = false;
            foreach (var (l, g, r, o) in gates)
            {
                if (wireValues.ContainsKey(o)) continue;
                if (!wireValues.ContainsKey(l) || !wireValues.ContainsKey(r)) continue;
                if (g == "AND")
                {
                    wireValues[o] = wireValues[l] && wireValues[r];
                }
                else if (g == "OR")
                {
                    wireValues[o] = wireValues[l] || wireValues[r];
                }
                else if (g == "XOR")
                {
                    wireValues[o] = wireValues[l] ^ wireValues[r];
                }
                updated = true;
            }
        }

        var zWires = wireValues
            .Where(kv => kv.Key.StartsWith('z'))
            .Select(kv => (Key: kv.Key, Val: kv.Value))
            .OrderBy(k => -int.Parse(k.Key[1..]))
            .Select(k => k.Val ? "1" : "0")
            .ToList();

        var outputBinary = string.Join("", zWires);
        var result = Convert.ToInt64(outputBinary, 2);
        Console.WriteLine(outputBinary);
        Console.WriteLine(result);
    }
}