using NonDivisibleSubset;
using System.Runtime.InteropServices;

internal class Program
{
    //https://www.hackerrank.com/challenges/non-divisible-subset/problem?isFullScreen=true
    //submitted on 09.01.2023 Tue
    //difficulty : medium

    private static void Main(string[] args)
    {
        var list = Helper.Data;
        var k = 98;
        Console.WriteLine("\nDictionary : ");
        var dic = GetModDictionary(k, list);
        var sortedDict = dic.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

        /*foreach (var item in sortedDict)
        {
            Console.WriteLine($"key : {item.Key}, value : {item.Value}");
        }*/

        Console.WriteLine("maxSubArrLength :" + getNonDivisibles(k, sortedDict));

    }

    public static Dictionary<int, int> GetModDictionary(int k, List<int> list)
    {
        Dictionary<int, int> dic = new Dictionary<int, int>();

        foreach (var item in list)
        {
            int mod = item % k;
            if (dic.ContainsKey(mod))
            {
                dic[mod]++;
            }
            else
            {
                dic.Add(mod, 1);
            }

        }

        return dic;
    }

    public static int getNonDivisibles(int k, Dictionary<int, int> dic)
    {
        var arr = new Dictionary<int, int>();
        var noneEligibleList = new List<int>();

        int maxSubArrLength = 0;
        int subArrayLength = 0;
        maxSubArrLength = subArrayLength;

        foreach (var item_1 in dic)
        {
            if (item_1.Key == 0)
                continue;
            subArrayLength = 0;
            arr = new Dictionary<int, int>();
            noneEligibleList = new List<int>();

            noneEligibleList.Add(k - item_1.Key);

            if ((item_1.Key * 2) % k == 0)
            {
                subArrayLength++;
                arr.Add(item_1.Key, 1);
            }

            else
            {
                subArrayLength += item_1.Value;
                arr.Add(item_1.Key, item_1.Value);
            }

            foreach (var item_2 in dic)
            {
                if (item_2.Key == item_1.Key || item_2.Key == 0)
                    continue;

                if (!noneEligibleList.Any(x => x == item_2.Key))
                {
                    var val = (item_2.Key * 2) % k == 0 ? 1 : item_2.Value;
                    arr.Add(item_2.Key, val);
                    subArrayLength += val;
                    noneEligibleList.Add(k - item_2.Key);
                }
            }

            Console.WriteLine();
            foreach (var x in arr)
            {
                Console.Write($" ({x.Key} - {x.Value}) ");
            }
            Console.WriteLine($" : [{subArrayLength}]");



            if (subArrayLength > maxSubArrLength)
            {
                maxSubArrLength = subArrayLength;
            }
        }

        if (dic.Any(x => x.Key == 0))
            maxSubArrLength++;

        return maxSubArrLength;
    }


}