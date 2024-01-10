internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine(repeatedString("a", 1000000000000));
    }

    public static long repeatedString(string s, long n)
    {
        long result = 0;
        int c = 0;
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == 'a' )
                c++;
        }

        result = (long)Math.Floor((double)n/s.Length )* c;

        var mod = n % s.Length;
        for (int i = 0; i < mod; i++)
        {
            if (s[i] == 'a')
                result++;
        }

        return result;
    }
}