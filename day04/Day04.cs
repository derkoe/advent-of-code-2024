

public class Day04
{
    public static void Run()
    {
        // Read file "day04/input.txt" into 2d array
        var input = File.ReadAllLines("day04/input.txt")
            .Select(l => l.ToCharArray())
            .ToArray();

        // find all occurences of word "XMAS" in all directions in the 2d array
        var xmasCount = 0;

        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                if (input[i][j] == 'X')
                {
                    // right
                    if (i + 3 < input.Length && input[i + 1][j] == 'M' && input[i + 2][j] == 'A' && input[i + 3][j] == 'S')
                    {
                        xmasCount++;
                    }
                    // left
                    if (i - 3 >= 0 && input[i - 1][j] == 'M' && input[i - 2][j] == 'A' && input[i - 3][j] == 'S')
                    {
                        xmasCount++;
                    }
                    // down
                    if (j + 3 < input[i].Length && input[i][j + 1] == 'M' && input[i][j + 2] == 'A' && input[i][j + 3] == 'S')
                    {
                        xmasCount++;
                    }
                    // up
                    if (j - 3 >= 0 && input[i][j - 1] == 'M' && input[i][j - 2] == 'A' && input[i][j - 3] == 'S')
                    {
                        xmasCount++;
                    }

                    // diagonal down right
                    if (i + 3 < input.Length && j + 3 < input[i].Length && input[i + 1][j + 1] == 'M' && input[i + 2][j + 2] == 'A' && input[i + 3][j + 3] == 'S')
                    {
                        xmasCount++;
                    }
                    // diagonal down left
                    if (i + 3 < input.Length && j - 3 >= 0 && input[i + 1][j - 1] == 'M' && input[i + 2][j - 2] == 'A' && input[i + 3][j - 3] == 'S')
                    {
                        xmasCount++;
                    }
                    // diagonal up right
                    if (i - 3 >= 0 && j + 3 < input[i].Length && input[i - 1][j + 1] == 'M' && input[i - 2][j + 2] == 'A' && input[i - 3][j + 3] == 'S')
                    {
                        xmasCount++;
                    }
                    // diagonal up left
                    if (i - 3 >= 0 && j - 3 >= 0 && input[i - 1][j - 1] == 'M' && input[i - 2][j - 2] == 'A' && input[i - 3][j - 3] == 'S')
                    {
                        xmasCount++;
                    }
                }
            }
        }
        Console.WriteLine($"Part 1: {xmasCount}");

        // find all MAS written in diagonal crosses in the 2d array
        var diagonalXMasCount = 0;
        for (int line = 0; line < input.Length; line++)
        {
            for (int col = 0; col < input[line].Length; col++)
            {
                // A is in the middle
                if (input[line][col] == 'A')
                {
                    if (isUpLeft(input, line, col, 'M') && isDownRight(input, line, col, 'S') && isDownLeft(input, line, col, 'M') && isUpRight(input, line, col, 'S'))
                    {
                        diagonalXMasCount++;
                    }
                    if (isUpLeft(input, line, col, 'M') && isDownRight(input, line, col, 'S') && isDownLeft(input, line, col, 'S') && isUpRight(input, line, col, 'M'))
                    {
                        diagonalXMasCount++;
                    }
                    if (isUpLeft(input, line, col, 'S') && isDownRight(input, line, col, 'M') && isDownLeft(input, line, col, 'M') && isUpRight(input, line, col, 'S'))
                    {
                        diagonalXMasCount++;
                    }
                    if (isUpLeft(input, line, col, 'S') && isDownRight(input, line, col, 'M') && isDownLeft(input, line, col, 'S') && isUpRight(input, line, col, 'M'))
                    {
                        diagonalXMasCount++;
                    }
                }
            }
        }

        Console.WriteLine($"Part 2: {diagonalXMasCount}");
    }

    private static bool isUpLeft(char[][] input, int line, int col, char character)
    {
        return line - 1 >= 0 && col - 1 >= 0 && input[line - 1][col - 1] == character;
    }

    private static bool isUpRight(char[][] input, int line, int col, char character)
    {
        return line - 1 >= 0 && col + 1 < input[line].Length && input[line - 1][col + 1] == character;
    }

    private static bool isDownLeft(char[][] input, int line, int col, char character)
    {
        return line + 1 < input.Length && col - 1 >= 0 && input[line + 1][col - 1] == character;
    }

    private static bool isDownRight(char[][] input, int line, int col, char character)
    {
        return line + 1 < input.Length && col + 1 < input[line].Length && input[line + 1][col + 1] == character;
    }
}