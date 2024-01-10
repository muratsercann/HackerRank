using System.Reflection.Metadata;

internal class Program
{
    private static void Main(string[] args)
    {
        //Console.WriteLine(jumpingOnClouds(new List<int> { 0, 0, 0, 0, 1, 0}));
        Console.WriteLine(equalizeArray(new List<int> { 3,3,2,1,3,4,4 }));
    }

    /// <summary>
    /// Complete the 'equalizeArray' function below
    /// The function is expected to return an INTEGER.
    /// The function accepts INTEGER_ARRAY arr as parameter.
    /// </summary>
    /// <param name="arr"></param>
    /// <returns></returns>
    public static int equalizeArray(List<int> arr)
    {
        var min = int.MaxValue;
        foreach (var item in arr) {
            var count = arr.Count(x => x != item);
            if (count < min)
                min = count;
        }

        return min;
    }

    public static int jumpingOnClouds(List<int> c)
    {
        var result = 0;

        for (int i = 0; i < c.Count;)
        {
            if (i + 2 < c.Count && c[i + 2] == 0)
            {
                result++;
                i += 2;
            }

            else if (i + 1 < c.Count && c[i + 1] == 0)
            {
                result++;
                i++;
            }

            else
            {
                break;
            }
        }

        return result;
    }
}