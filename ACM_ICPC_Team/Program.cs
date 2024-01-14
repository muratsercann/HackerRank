using System.Diagnostics.Metrics;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {

        var result = acmTeam(new List<string> { "10101", "11110", "00010" });
        Console.WriteLine(result[0]);
        Console.WriteLine(result[1]);
    }

    public static List<int> acmTeam(List<string> topic)
    {
        var result = new List<int>();

        int max = Int32.MinValue;
        int teamCount = 0;

        for (int i = 0; i < topic.Count; i++)
        {
            for (int j = i+1; j < topic.Count; j++)
            {
                var r = BigBinaryOr(topic[i], topic[j]);
                var count = r.Where(x => x == '1').Count();

                if (count == max)
                    teamCount++;

                if (count > max)
                {
                    max = count;
                    teamCount = 1;
                }
            }

        }

        result.Add(max);
        result.Add(teamCount);
        return result;
    }

    public static string BigBinaryOr(string s1, string s2)
    {
        int maxcharacterLength = 32;

        s1 = s1.PadLeft(s2.Length, '0');
        s2 = s2.PadLeft(s1.Length, '0');

        int length = s1.Length;
        int n = (int)(s1.Length / maxcharacterLength);
        int m = s1.Length % maxcharacterLength;

        StringBuilder s = new StringBuilder();
        string t1 = "";
        string t2 = "";
        int x1, x2, x3;

        for (int i = 1; i <= n; i++)
        {

            t1 = s1.Substring(length - (maxcharacterLength * i), maxcharacterLength);
            t2 = s2.Substring(length - (maxcharacterLength * i), maxcharacterLength);
            
            s.Insert(0, BinaryOr(t1, t2));
        }

        if (length % maxcharacterLength > 0)
        {
            t1 = s1.Substring(0, length % maxcharacterLength);
            t2 = s2.Substring(0, length % maxcharacterLength);
            s.Insert(0, BinaryOr(t1, t2));
        }

        return s.ToString();
    }

    private static string BinaryOr(string t1, string t2)
    {
        var result = string.Empty;
        int x1 = Convert.ToInt32(t1, 2);
        int x2 = Convert.ToInt32(t2, 2);
        int x3 = x1 | x2;        
        result = Convert.ToString(x3, 2);
        return result;
    }
}