

using System.Numerics;
using System.Reflection.Metadata;

internal class Program
{
    private static void Main(string[] args)
    {
        var obstacles = new List<List<int>>
        {
            new List<int> { 3,5 },//3
            new List<int> { 7,7 },//2
            new List<int> { 6,6 },//2
            new List<int> { 4,6 },//3
            new List<int> { 4,8 },//1
            new List<int> { 4,2 },//2
            new List<int> { 6,2 },//2
            new List<int> { 1, 1},//1
            new List<int> { 3, 3},//1
        };

        Console.WriteLine("old : " + queensAttack(8, 0, 3, 2, obstacles));

        Console.WriteLine("new : " + QAttack.Run(8, 0, 3, 2, obstacles));
    }

    public static int queensAttack(int n, int k, int r_q, int c_q, List<List<int>> obstacles)
    {
        var result = 0;

        Borders borders = new Borders();

        var right = obstacles.Where(x => x[0] == r_q && x[1] > c_q)
                               .OrderBy(x => x[1])
                               .FirstOrDefault();

        var left = obstacles.Where(x => x[0] == r_q && x[1] < c_q)
                               .OrderByDescending(x => x[1])
                               .FirstOrDefault();

        var top = obstacles.Where(x => x[1] == c_q && x[0] > r_q)
                               .OrderBy(x => x[0])
                               .FirstOrDefault();

        var bottom = obstacles.Where(x => x[1] == c_q && x[0] < r_q)
                               .OrderByDescending(x => x[0])
                               .FirstOrDefault();

        var rightTop = obstacles.Where(x => x[0] - x[1] == r_q - c_q && x[0] > r_q)
                               .OrderBy(x => x[0])
                               .FirstOrDefault();

        var leftBottom = obstacles.Where(x => x[0] - x[1] == r_q - c_q && x[0] < r_q)
                              .OrderByDescending(x => x[0])
                              .FirstOrDefault();

        var rightBottom = obstacles.Where(x => x[0] + x[1] == r_q + c_q && x[0] < r_q)
                              .OrderByDescending(x => x[0])
                              .FirstOrDefault();

        var leftTop = obstacles.Where(x => x[0] + x[1] == r_q + c_q && x[0] > r_q)
                              .OrderBy(x => x[0])
                              .FirstOrDefault();


        int originalCount = n * 2 - 2;

        result += originalCount;

        //left-bottom to right-top diagonal calculation
        int dif = r_q - c_q;

        var notEligible = 0;
        int row1, col1, row2, col2;

        if (dif >= 0)
        {
            col1 = 1;
            row1 = dif + 1;

            row2 = n;
            col2 = n - dif;
        }
        else
        {
            row1 = 1;
            col1 = 1 - dif;

            col2 = n;
            row2 = n + dif;
        }

        borders.leftBottom = new int[] { row1, col1 };
        borders.rightTop = new int[] { row2, col2 };

        result += (row2 - row1);

        //left-top to right-bottom diagonal calculation
        var sum = r_q + c_q;

        if (sum > n)
        {
            row1 = n;
            col1 = sum - n;

            col2 = row1;
            row2 = col1;


        }
        else
        {
            col1 = 1;
            row1 = sum - col1;

            row2 = col1;
            col2 = row1;
        }

        borders.leftTop = new int[] { row1, col1 };
        borders.rightBottom = new int[] { row2, col2 };

        result += (col2 - col1);


        borders.left = new int[] { r_q, 1 };
        borders.right = new int[] { r_q, n };
        borders.top = new int[] { n, c_q };
        borders.bottom = new int[] { 1, c_q };


        if (right != null)
            notEligible += (n - right[1] + 1);

        if (left != null)
            notEligible += (left[1]);

        if (top != null)
            notEligible += (n - top[0] + 1);

        if (bottom != null)
            notEligible += (bottom[0]);

        if (rightTop != null)
            notEligible += (borders.rightTop[0] - rightTop[0] + 1);

        if (rightBottom != null)
            notEligible += (rightBottom[0] - borders.rightBottom[0] + 1);

        if (leftTop != null)
            notEligible += (borders.leftTop[0] - leftTop[0] + 1);

        if (leftBottom != null)
            notEligible += (leftBottom[0] - borders.leftBottom[0] + 1);

        return result - notEligible;
    }

    public class Borders
    {
        public int[] right { get; set; } = new int[2];
        public int[] left { get; set; } = new int[2];
        public int[] top { get; set; } = new int[2];
        public int[] bottom { get; set; } = new int[2];
        public int[] rightTop { get; set; } = new int[2];
        public int[] rightBottom { get; set; } = new int[2];
        public int[] leftTop { get; set; } = new int[2];
        public int[] leftBottom { get; set; } = new int[2];
    }

    /// <summary>
    /// Better Solution For this problem
    /// </summary>
    public static class QAttack
    {
        public static int Run(int n, int k, int row, int col, List<List<int>> obstacles)
        {
            var result = 0;

            int l, r, t, b, lt, lb, rt, rb;

            l = col - 1;
            r = n - col;
            t = n - row;
            b = row - 1;

            lb = Math.Min(row - 1, col - 1);
            rt = Math.Min(n - row, n - col);
            lt = Math.Min(n - row, col - 1);
            rb = Math.Min(row - 1, n - col);

            foreach (var obs in obstacles)
            {
                if (row > obs[0] && col > obs[1] && obs[0] - obs[1] == row - col)
                    rt = Math.Min(rt, obs[0] - row - 1);
                if (row < obs[0] && col < obs[1] && obs[0] - obs[1] == row - col)
                    lb = Math.Min(lb, row - obs[0] - 1);
                if (row > obs[0] && col < obs[1] && obs[0] + obs[1] == row + col)
                    lt = Math.Min(lt, obs[0] - row - 1);
                if (row < obs[0] && col > obs[1] && obs[0] + obs[1] == row + col)
                    rb = Math.Min(rb, row - obs[0] - 1);

                if (obs[0] == row && obs[1] > col)
                    r = Math.Min(r, obs[1] - col - 1);
                if (obs[0] == row && obs[1] < col)
                    l = Math.Min(l, col - obs[1] - 1);
                if (obs[1] == col && obs[0] > row)
                    t = Math.Min(t, obs[0] - row - 1);
                if (obs[1] == col && obs[0] < row)
                    b = Math.Min(b, row - obs[0] - 1);

            }

            result = l + r + t + b + lb + rt + lt + rb;

            return result;
        }


        public static bool checkStatus(int r1, int c1, int r2, int c2)
        {
            var isValid = false;

            return isValid;
        }

    }
}