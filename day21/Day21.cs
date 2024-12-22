public class Day21
{
    /*
        The klepad looks like this:
        +---+---+---+
        | 7 | 8 | 9 |
        +---+---+---+
        | 4 | 5 | 6 |
        +---+---+---+
        | 1 | 2 | 3 |
        +---+---+---+
            | 0 | A |
            +---+---+
    */
    class KeypadRobot
    {
        public static string[] Moves(char fromPos, char toPos)
        {
            switch (fromPos)
            {
                case '1':
                    switch (toPos)
                    {
                        case '2':
                            return [">"];
                        case '3':
                            return [">>"];
                        case '4':
                            return ["^"];
                        case '5':
                            return ["^>", ">^"];
                        case '6':
                            return ["^>>", ">>^", ">^>"];
                        case '7':
                            return ["^^"];
                        case '8':
                            return ["^^>", ">^^", ">^>"];
                        case '9':
                            return ["^^>>", ">>^^", ">^>^", "^>>^", "^>^>"];
                        case '0':
                            return [">v"];
                        case 'A':
                            return [">>v", ">v>"];
                    }
                    break;
                case '2':
                    switch (toPos)
                    {
                        case '1':
                            return ["<"];
                        case '3':
                            return [">"];
                        case '4':
                            return ["^<", "<^"];
                        case '5':
                            return ["^"];
                        case '6':
                            return ["^>", ">^"];
                        case '7':
                            return ["^^<", "<^^", "^<^"];
                        case '8':
                            return ["^^"];
                        case '9':
                            return [">^^", "^^>", "^>^"];
                        case '0':
                            return ["v"];
                        case 'A':
                            return [">v", "v>"];
                    }
                    break;
                case '3':
                    switch (toPos)
                    {
                        case '1':
                            return ["<<"];
                        case '2':
                            return ["<"];
                        case '4':
                            return ["^<<", "<<^", "<^<"];
                        case '5':
                            return ["^<", "<^"];
                        case '6':
                            return ["^"];
                        case '7':
                            return ["^^<<", "<<^^", "<^^<", "<^<^", "^<<^"];
                        case '8':
                            return ["^^<", "<^^", "^<^"];
                        case '9':
                            return ["^^"];
                        case '0':
                            return ["<v", "v<"];
                        case 'A':
                            return ["v"];
                    }
                    break;
                case '4':
                    switch (toPos)
                    {
                        case '1':
                            return ["v"];
                        case '2':
                            return ["v>", ">v"];
                        case '3':
                            return ["v>>", ">>v", ">v>"];
                        case '5':
                            return [">"];
                        case '6':
                            return [">>"];
                        case '7':
                            return ["^"];
                        case '8':
                            return ["^>", ">^"];
                        case '9':
                            return ["^>>", ">>^", ">^>"];
                        case '0':
                            return [">vv", "v>v"];
                        case 'A':
                            return [">>vv", ">v>v", "v>>v", "v>v>"];
                    }
                    break;
                case '5':
                    switch (toPos)
                    {
                        case '1':
                            return ["<v", "v<"];
                        case '2':
                            return ["v"];
                        case '3':
                            return ["v>", ">v"];
                        case '4':
                            return ["<"];
                        case '6':
                            return [">"];
                        case '7':
                            return ["^<", "<^"];
                        case '8':
                            return ["^"];
                        case '9':
                            return ["^>", ">^"];
                        case '0':
                            return ["vv"];
                        case 'A':
                            return [">vv", "vv>", "v>v"];
                    }
                    break;
                case '6':
                    switch (toPos)
                    {
                        case '1':
                            return ["<<v", "<v<", "v<<"];
                        case '2':
                            return ["<v", "v<"];
                        case '3':
                            return ["v"];
                        case '4':
                            return ["<<"];
                        case '5':
                            return ["<"];
                        case '7':
                            return ["^<<", "<<^", "<^<"];
                        case '8':
                            return ["^<", "<^"];
                        case '9':
                            return ["^"];
                        case '0':
                            return ["<vv", "v<v", "vv<"];
                        case 'A':
                            return ["vv"];
                    }
                    break;
                case '7':
                    switch (toPos)
                    {
                        case '1':
                            return ["vv"];
                        case '2':
                            return ["vv>", ">vv", "v>v"];
                        case '3':
                            return ["vv>>", ">>vv", ">v>v", "v>>v", "v>v>"];
                        case '4':
                            return ["v"];
                        case '5':
                            return ["v>", ">v"];
                        case '6':
                            return ["v>>", ">>v", ">v>"];
                        case '8':
                            return [">"];
                        case '9':
                            return [">>"];
                        case '0':
                            return [">vvv, v>vv", "vv>v"];
                        case 'A':
                            return [">>vvv", ">v>vv", "v>>vv", "v>vv>", "vv>>v", "vv>v>"];
                    }
                    break;
                case '8':
                    switch (toPos)
                    {
                        case '1':
                            return ["<vv", "v<v", "vv<"];
                        case '2':
                            return ["vv"];
                        case '3':
                            return ["vv>", ">vv", "v>v"];
                        case '4':
                            return ["<v", "v<"];
                        case '5':
                            return ["v"];
                        case '6':
                            return ["v>", ">v"];
                        case '7':
                            return ["<"];
                        case '9':
                            return [">"];
                        case '0':
                            return ["vvv"];
                        case 'A':
                            return [">vvv", "v>vv", "vv>v", "vvv>"];
                    }
                    break;
                case '9':
                    switch (toPos)
                    {
                        case '1':
                            return ["<<vv", "<v<v", "v<<v", "vv<<"];
                        case '2':
                            return ["<vv", "v<v", "vv<"];
                        case '3':
                            return ["vv"];
                        case '4':
                            return ["<<v", "<v<", "v<<"];
                        case '5':
                            return ["<v", "v<"];
                        case '6':
                            return ["v"];
                        case '7':
                            return ["<<"];
                        case '8':
                            return ["<"];
                        case '0':
                            return ["<vvv", "v<vv", "vv<v", "vvv<"];
                        case 'A':
                            return ["vvv"];
                    }
                    break;
                case '0':
                    switch (toPos)
                    {
                        case '1':
                            return ["^<"];
                        case '2':
                            return ["^"];
                        case '3':
                            return ["^>", ">^"];
                        case '4':
                            return ["^^<", "^<^"];
                        case '5':
                            return ["^^"];
                        case '6':
                            return ["^^>", ">^^", "^>^"];
                        case '7':
                            return ["^^^<", "^^<^", "^<^^",];
                        case '8':
                            return ["^^^"];
                        case '9':
                            return ["^^^>", ">^^^", "^>^^", "^^>^"];
                        case 'A':
                            return [">"];
                    }
                    break;
                case 'A':
                    switch (toPos)
                    {
                        case '1':
                            return ["^<<", "<^<"];
                        case '2':
                            return ["^<", "<^"];
                        case '3':
                            return ["^"];
                        case '4':
                            return ["^^<<", "<^^<", "<^<^", "^<<^"];
                        case '5':
                            return ["^^<", "<^^", "^<^"];
                        case '6':
                            return ["^^"];
                        case '7':
                            return ["^^^<<", "<^^^<", "<^<^^", "^<<^^", "<^^<^", "^<^^^"];
                        case '8':
                            return ["^^^<", "<^^^", "^<^^", "^^<^"];
                        case '9':
                            return ["^^^"];
                        case '0':
                            return ["<"];
                    }
                    break;

            }
            return [""];
        }
    }

    /*
        +---+---+
        | ^ | A |
    +---+---+---+
    | < | v | > |
    +---+---+---+
    */
    private class DirectionalRobot
    {
        public static string[] Moves(char fromPos, char toPos)
        {
            switch (fromPos)
            {
                case 'A':
                    switch (toPos)
                    {
                        case '^':
                            return ["<"];
                        case '<':
                            return ["v<<", "<v<"];
                        case 'v':
                            return ["<v", "v<"];
                        case '>':
                            return ["v"];
                    }
                    break;
                case '^':
                    switch (toPos)
                    {
                        case 'A':
                            return [">"];
                        case '<':
                            return ["v<"];
                        case 'v':
                            return ["v"];
                        case '>':
                            return ["v>", ">v"];
                    }
                    break;
                case '<':
                    switch (toPos)
                    {
                        case 'A':
                            return [">>^", ">^>"];
                        case '^':
                            return [">^"];
                        case 'v':
                            return [">"];
                        case '>':
                            return [">>"];
                    }
                    break;
                case 'v':
                    switch (toPos)
                    {
                        case 'A':
                            return [">^", "^>"];
                        case '^':
                            return ["^"];
                        case '<':
                            return ["<"];
                        case '>':
                            return [">"];
                    }
                    break;
                case '>':
                    switch (toPos)
                    {
                        case 'A':
                            return ["^"];
                        case '^':
                            return ["<^", "^<"];
                        case '<':
                            return ["<<"];
                        case 'v':
                            return ["<"];
                    }
                    break;
            }
            return [""];
        }
    }


    public static void Run()
    {
        var codes = File.ReadAllLines("day21/input.txt");

        Part1(codes);

        Part2(codes);
    }

    private static void Part1(string[] codes)
    {
        var sum = 0;
        foreach (var code in codes)
        {
            HashSet<string> keypadMoves = GetKeypadMoves(code);

            HashSet<string> directionalMoves1 = new HashSet<string>();
            foreach (var move in keypadMoves)
            {
                directionalMoves1.UnionWith(GetDirectionalMoves(move));
            }
            HashSet<string> directionalMoves2 = new HashSet<string>();
            foreach (var move in directionalMoves1)
            {
                directionalMoves2.UnionWith(GetDirectionalMoves(move));
            }

            var shortestMove = directionalMoves2.OrderBy(x => x.Length).First();
            Console.WriteLine($"{code}, ${shortestMove} ({shortestMove.Length})");

            int sequenceLength = shortestMove.Length;
            int numericPartOfCode = int.Parse(code.Substring(0, code.Length - 1));

            sum += sequenceLength * numericPartOfCode;
        }
        Console.WriteLine($"Part 1: {sum}");
    }

    private static void Part2(string[] codes)
    {
        // TODO
    }

    private static HashSet<string> GetKeypadMoves(string input)
    {
        var keypadMoves = new HashSet<string>() { "" };
        var currentPos = 'A';
        foreach (var move in input)
        {
            var nextKeypadMoves = new HashSet<string>();
            foreach (var moves in keypadMoves)
            {
                var newMoves = KeypadRobot.Moves(currentPos, move);
                foreach (var newMove in newMoves)
                {
                    nextKeypadMoves.Add(moves + newMove + 'A');
                }
            }
            currentPos = move;
            keypadMoves = nextKeypadMoves;
        }

        return keypadMoves;
    }

    private static HashSet<string> GetDirectionalMoves(string input)
    {
        var directionalMoves = new HashSet<string>() { "" };
        var currentPos = 'A';
        foreach (var move in input)
        {
            var nextDirectionalMoves = new HashSet<string>();
            foreach (var moves in directionalMoves)
            {
                var newMoves = DirectionalRobot.Moves(currentPos, move);
                foreach (var newMove in newMoves)
                {
                    nextDirectionalMoves.Add(moves + newMove + 'A');
                }
            }
            currentPos = move;
            directionalMoves = nextDirectionalMoves;
        }

        return directionalMoves;
    }
}