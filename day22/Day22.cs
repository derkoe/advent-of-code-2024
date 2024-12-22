public class Day22
{
    public static void Run()
    {
        var numbers = File.ReadAllLines("day22/input.txt").Select(line => Int32.Parse(line)).ToList();

        Part1(numbers);

        Part2(numbers);
    }

    private static void Part1(List<int> numbers)
    {
        for (int i = 0; i < 2000; i++)
        {
            numbers = numbers.Select(CalcNextSecretNumber).ToList();
        }

        Console.WriteLine("Part 1: " + numbers.Sum(x => (long)x));
    }

    private static void Part2(List<int> numbers)
    {
        var seqToPriceMap = new Dictionary<string, int>();
        foreach (var secretNum in numbers)
        {
            var currentPrice = secretNum % 10;
            var diffSeq = new List<int>();
            var currentSecretNumber = secretNum;
            var currentSeqToPriceMap = new Dictionary<string, int>();
            for (int i = 1; i < 2000; i++)
            {
                var nextSecretNumber = CalcNextSecretNumber(currentSecretNumber);
                var nextPrice = nextSecretNumber % 10;
                diffSeq.Add(nextPrice - currentPrice);
                currentPrice = nextPrice;
                currentSecretNumber = nextSecretNumber;
                if (i > 3)
                {
                    var diffSeqStr = string.Join(",", diffSeq);
                    if (!currentSeqToPriceMap.ContainsKey(diffSeqStr))
                    {
                        currentSeqToPriceMap.Add(diffSeqStr, nextPrice);
                    }
                    diffSeq.RemoveAt(0);
                }
            }
            foreach (var seqToPrice in currentSeqToPriceMap)
            {
                if (seqToPriceMap.ContainsKey(seqToPrice.Key))
                {
                    seqToPriceMap[seqToPrice.Key] += seqToPrice.Value;
                }
                else
                {
                    seqToPriceMap.Add(seqToPrice.Key, seqToPrice.Value);
                }
            }
        }

        // find key with max price
        int maxPrice = int.MinValue;
        string maxPriceKey = "";
        foreach (var kvp in seqToPriceMap)
        {
            if (kvp.Value > maxPrice)
            {
                maxPrice = kvp.Value;
                maxPriceKey = kvp.Key;
            }
        }

        Console.WriteLine("Part 2: " + maxPrice + " " + maxPriceKey);
    }

    public static int CalcNextSecretNumber(int secretNumber)
    {
        long secretNumberLong = secretNumber;
        secretNumberLong = (secretNumberLong ^ (secretNumberLong * 64)) % 16777216;
        secretNumberLong = (secretNumberLong ^ (secretNumberLong / 32)) % 16777216;
        secretNumberLong = (secretNumberLong ^ (secretNumberLong * 2048)) % 16777216;
        return (int)secretNumberLong;
    }
}