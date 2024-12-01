var lines = File.ReadAllLines("day1/input.txt");
List<int> arr1 = new List<int>();
List<int> arr2 = new List<int>();
foreach (var line in lines)
{
    var parts = line.Split("   ");
    var first = int.Parse(parts[0]);
    arr1.Add(first);
    var second = int.Parse(parts[1]);
    arr2.Add(second);
}

// Part 1
arr1.Sort();
arr2.Sort();
int sum = 0;
for (int i = 0; i < arr1.Count; i++)
{
    sum += Math.Abs(arr2[i] - arr1[i]);
}

Console.WriteLine("Part 1:" + sum);

// Part 2
int part2 = 0;
foreach (var num in arr1)
{
    int count = 0;
    foreach (var num2 in arr2)
    {
        if (num == num2)
        {
            count++;
        }
    }
    if (count > 0)
    {
        part2 += num * count;
    }
}

Console.WriteLine("Part 2:" + part2);