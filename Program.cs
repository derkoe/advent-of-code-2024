class Program
{
    public static void Main(string[] args)
    {
        switch (int.Parse(args[0]))
        {
            case 1: Day01.Run(); break;
            case 2: Day02.Run(); break;
        }
    }
}