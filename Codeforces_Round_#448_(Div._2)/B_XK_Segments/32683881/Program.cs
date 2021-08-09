using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Scanner
{
    private readonly char Separator = ' ';
    private int Index = 0;
    private string[] Line = new string[0];
    public string Next()
    {
        if (Index >= Line.Length)
        {
            Line = Console.ReadLine().Split(Separator);
            Index = 0;
        }
        var ret = Line[Index];
        Index++;
        return ret;
    }

    public int NextInt()
    {
        return int.Parse(Next());
    }

    public long NextLong()
    {
        return long.Parse(Next());
    }

    public string[] StringArray()
    {
        Line = Console.ReadLine().Split(Separator);
        Index = Line.Length;
        return Line;
    }

    public int[] IntArray()
    {
        var l = StringArray();
        var res = new int[l.Length];
        for (int i = 0; i < l.Length; i++)
        {
            res[i] = int.Parse(l[i]);
        }
        return res;
    }

    public long[] LongArray()
    {
        var l = StringArray();
        var res = new long[l.Length];
        for (int i = 0; i < l.Length; i++)
        {
            res[i] = long.Parse(l[i]);
        }
        return res;
    }
}
class Program
{
    private int N, X, K;
    private int[] A;
    private void Scan()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        X = sc.NextInt();
        K = sc.NextInt();
        A = sc.IntArray();
    }

    public void Solve()
    {
        Scan();
        Array.Sort(A);
        long ans = 0;
        for (int i = 0; i < N; i++)
        {
            ans += Q(A[i]);
        }
        Console.WriteLine(ans);
    }

    private int Q(int n)
    {
        long right;
        long left;
        if (n % X == 0)
        {
            if (K == 0)
            {
                return 0;
            }
            right = n + (long)(K - 1) * X;
            left = right + X - 1;
        }
        else
        {
            var nn = (n / X) * X;
            if (K == 0)
            {
                right = n;
                left = nn + X - 1;
            }
            else
            {
                right = nn + (long)X * K;
                left = right + X - 1;
            }
        }

        int min = -1;
        int max = N;
        while (max - min > 1)
        {
            int mid = (min + max) / 2;
            if (A[mid] >= right)
            {
                max = mid;
            }
            else
            {
                min = mid;
            }
        }
        var rightIndex = max;
        min = -1;
        max = N;
        while (max - min > 1)
        {
            int mid = (min + max) / 2;
            if (A[mid] > left)
            {
                max = mid;
            }
            else
            {
                min = mid;
            }
        }
        var leftIndex = min;
        return leftIndex - rightIndex + 1;
    }

    static void Main(string[] args)
    {
        new Program().Solve();
    }
}