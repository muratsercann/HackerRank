using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.InteropServices;


/// <summary>
/// https://www.hackerrank.com/challenges/organizing-containers-of-balls/problem?isFullScreen=false
/// </summary>
internal class Program
{
    private static void Main(string[] args)
    {
        List<List<int>> list = new List<List<int>>();

        list.Add(new List<int> { 999492517, 799478080, 656101934, 997512798, 804933333, 915670542, 782208250, 954643067, 732534714, 867597142 });
        list.Add(new List<int> { 964490821, 670761302, 730538472, 833364952, 990490567, 915915305, 992506159, 649697949, 716896380, 997743711 });
        list.Add(new List<int> { 999095096, 983763798, 908066775, 948861698, 726947895, 999354593, 984934595, 783261975, 978013440, 987794743 });
        list.Add(new List<int> { 986691506, 991462574, 885883868, 804829138, 442820754, 492982056, 998548547, 840263895, 668383027, 693023265 });
        list.Add(new List<int> { 974866184, 644489875, 868604324, 893837151, 765026553, 998391109, 441164033, 998130238, 619690221, 830966857 });
        list.Add(new List<int> { 952847118, 766160113, 550327396, 536251554, 998904508, 858024773, 651508169, 916581880, 972639192, 996726528 });
        list.Add(new List<int> { 812906949, 995619450, 702100492, 657117080, 986142546, 950529577, 621715226, 992598625, 793310688, 999452440 });
        list.Add(new List<int> { 985222981, 971752436, 999738929, 945702886, 614285146, 699281452, 996873064, 683673881, 656293577, 678356750 });
        list.Add(new List<int> { 654451506, 790649871, 992979755, 995323819, 995052055, 690803368, 803771886, 519616025, 999387614, 421120727 });
        list.Add(new List<int> { 970029930, 897355574, 936839157, 897371301, 875367874, 997446616, 531658701, 696699010, 726007773, 989623455 });

        var r = organizingContainers(list);
        Console.WriteLine(r);
    }


    /// <summary>
    /// Recursive Funtion. 
    /// This function checks : Can each color be collected in separate containers
    /// For example : Color : 0 in Container 2, Color 1 in Container 0, Color : 2 in Container 1
    /// </summary>
    /// <param name="distinctColors">This represents the distinct color list</param>
    /// <param name="containerIndex"></param>
    /// <param name="containers">
    ///     For example      : container = {{1},{0,1}}
    ///     containers[1]    : [0,1];  
    ///     => ALL COLOR "0" AND ALL COLOR "1" CAN BE COLLECTED IN CONTAINER "1"
    /// </param>
    /// <returns></returns>
    public static bool isColorsInSeparateContainer(List<int> distinctColors, int containerIndex, List<List<int>> containers)
    {
        for (int i = 0; i < containers[containerIndex].Count; i++)
        {
            if (!distinctColors.Any(x => x == containers[containerIndex][i]))
            {
                distinctColors.Add(containers[containerIndex][i]);

                if (containerIndex + 1 < containers.Count)
                {
                    isColorsInSeparateContainer(distinctColors, containerIndex + 1, containers);

                    if (distinctColors.Count == containers.Count)
                    {   //This means Each color in separate containers.
                        return true;
                    }
                }
            }
        }

        if (distinctColors.Count > 0 && distinctColors.Count != containers.Count)
        {
            distinctColors.RemoveAt(distinctColors.Count - 1);
        }

        if (distinctColors.Count == containers.Count)
        {
            return true;
        }

        return false;

    }

    public static string organizingContainers(List<List<int>> containerList)
    {
        var result = string.Empty;
        /// boxes : which container can collect all of which colors
        List<List<int>> boxes = new List<List<int>>();
        Dictionary<int, long> ColorCountDict = new Dictionary<int, long>();//Key: Color, value : Count
        int containerCount = containerList.Count;
        int countOfAllColor = containerList[0].Count;


        for (int i = 0; i < countOfAllColor; i++)
        {
            ColorCountDict.Add(i, SumColorCount(containerList, i));
        }

        for (int c = 0; c < containerList.Count; c++)
        {
            long countOfBalls = Sum(containerList[c]);

            boxes.Add(new List<int>() { });

            //Which colors can I collect in this containerList
            for (int i = 0; i < countOfAllColor; i++)
            {
                if (countOfBalls == ColorCountDict[i])
                {
                    boxes[boxes.Count - 1].Add(i);
                    //Console.WriteLine($"Ball : {i} can be collected in box {c}");
                }
            }
        }

        List<int> distinct = new List<int>();

        var a = isColorsInSeparateContainer(distinct, 0, boxes);

        result = a ? "Possible" : "Impossible";

        return result;
    }

    public static long Sum(List<int> list)
    {
        long total = 0;

        foreach (var item in list)
        {
            total += item;
        }

        return total;
    }

    public static long SumColorCount(List<List<int>> containers, int color)
    {
        long total = 0;
        foreach (var item in containers)
        {
            total += item[color];
        }

        return total;
    }

    public static void WriteList(List<int> list)
    {
        Console.Write("Distinctlist : ");
        foreach (var item in list)
        {
            Console.Write($"{item}, ");
        }
        Console.WriteLine();
    }
}