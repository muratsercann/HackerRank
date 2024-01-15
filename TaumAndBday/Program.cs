internal class Program
{
    private static void Main(string[] args)
    {
        //Console.WriteLine(taumBday(10, 10, 1, 1, 1));
        //Console.WriteLine(taumBday(5, 9, 2, 3, 4));
        //Console.WriteLine(taumBday(3,6,9,1,1));
        //Console.WriteLine(taumBday(7, 7, 4, 2, 1));
        //Console.WriteLine(taumBday(3, 3, 1, 9, 2));

        Console.WriteLine(taumBday(42899452, 58539299, 832193, 584380 ,655132
));
    }

    public static long taumBday(int b, int w, int bc, int wc, int z)
    {
        long cost = 0;

        if ((long)bc > (long)wc + (long)z)
        {
            cost += ((long)wc + (long)z) * (long)b;
        }

        else
        {
            
            cost += (long)bc * (long)b;
        }

        if ((long)wc > (long)bc + (long)z)
        {
            cost += ((long)bc + (long)z) * (long)w;
        }

        else
        {
            cost += (long)wc * (long)w;
        }

        return cost;
    }
}