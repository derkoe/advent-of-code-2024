public class Day09
{
    const int FREE_SPACE = -1;

    static long CalcChecksum(List<int> disk)
    {
        var checksum = 0L;
        for (int i = 0; i < disk.Count(); i++)
        {
            if (disk[i] == FREE_SPACE)
            {
                continue;
            }
            checksum += i * disk[i];
        }
        return checksum;
    }

    static void PrintDisk(List<int> disk)
    {
        foreach (var block in disk)
        {
            Console.Write(block == FREE_SPACE ? "." : block);
        }
        Console.WriteLine();
    }

    public static void Run()
    {
        var input = File.ReadAllText("day09/input.txt");
        var disk = new List<int>();
        for (int i = 0; i < input.Count() - 1; i = i + 2)
        {
            int block = int.Parse(input[i].ToString());
            int free = int.Parse(input[i + 1].ToString());
            disk.AddRange(Enumerable.Repeat(i / 2, block));
            disk.AddRange(Enumerable.Repeat(FREE_SPACE, free));
        }
        disk.AddRange(Enumerable.Repeat((input.Count() - 1) / 2, int.Parse(input.Last().ToString())));

        Part1(new List<int>(disk));
        Part2(new List<int>(disk));
    }

    static void Part1(List<int> disk)
    {
        // Part 1 - defragement disk by continuously moving the last block to the first free space
        while (true)
        {
            while (disk.Last() == FREE_SPACE)
            {
                disk.RemoveAt(disk.Count - 1);
            }
            int lastBlock = disk.Last();
            int freeSpaceIndex = disk.IndexOf(FREE_SPACE);
            if (freeSpaceIndex == -1)
            {
                break;
            }
            disk[freeSpaceIndex] = lastBlock;
            disk.RemoveAt(disk.Count - 1);
        }
        Console.WriteLine("Part 1: " + CalcChecksum(disk));
    }

    static void Part2(List<int> disk)
    {
        // Part 2 - defragment disk but keep file blocks (with same number) together 
        int lastBlock = disk.Last();
        for (int block = lastBlock; block >= 0; block--)
        {
            int startOfBlock = disk.IndexOf(block);
            int endOfBlock = disk.LastIndexOf(block);
            int blockSize = endOfBlock - startOfBlock + 1;

            int startOfFreeSpace = disk.IndexOf(FREE_SPACE);
            while (startOfFreeSpace < startOfBlock && startOfFreeSpace != -1)
            {
                // find first free space to fit block with blockSize
                int endOfFreeSpace = startOfFreeSpace;
                while (endOfFreeSpace < disk.Count && disk[endOfFreeSpace] == FREE_SPACE)
                {
                    endOfFreeSpace++;
                }

                if (blockSize <= endOfFreeSpace - startOfFreeSpace)
                {
                    // move block to free freeSpace
                    disk.InsertRange(startOfFreeSpace, disk.GetRange(startOfBlock, blockSize));
                    disk.RemoveRange(startOfFreeSpace + blockSize, blockSize);
                    disk.RemoveRange(startOfBlock, blockSize);
                    disk.InsertRange(startOfBlock, Enumerable.Repeat(FREE_SPACE, blockSize));
                    break;
                }

                if (endOfFreeSpace == disk.Count)
                {
                    break;
                }

                startOfFreeSpace = disk.IndexOf(FREE_SPACE, endOfFreeSpace + 1);
            }

        }
        Console.WriteLine("Part 2: " + CalcChecksum(disk));

    }

}