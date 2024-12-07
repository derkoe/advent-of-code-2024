class Program
{
    public static void Main(string[] args)
    {
        switch (int.Parse(args[0]))
        {
            case 1: Day01.Run(); break;
            case 2: Day02.Run(); break;
            case 3: Day03.Run(); break;
            case 4: Day04.Run(); break;
            case 5: Day05.Run(); break;
            case 6: Day06.Run(); break;
            case 7: Day07.Run(); break;
        }
    }
}