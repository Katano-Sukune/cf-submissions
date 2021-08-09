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
    private int N;
    private int[] A;
    private void Scan()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();
    }

    public void Solve()
    {
        Scan();
        int ans = int.MaxValue;
        for (int a = 0; a < N; a++)
        {
            for (int b = a; b <= N; b++)
            {
                ans = Math.Min(ans, Calc(a, b));
            }

        }
        Console.WriteLine(ans);
    }

    private int Calc(int a, int b)
    {
        int aa = 0;
        int bb = 0;
        for (int i = a; i < b; i++)
        {
            aa += A[i];
        }
        for (int i = 0; i < a; i++)
        {
            bb += A[i];
        }
        for (int i = b; i < N; i++)
        {
            bb += A[i];
        }
        return Math.Abs(aa - bb);
    }

    static void Main(string[] args)
    {
        new Program().Solve();
    }
}