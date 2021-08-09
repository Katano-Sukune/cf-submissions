
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
    private readonly int Mod = 1000000007;
    private int N;
    private int[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();
        int gcd = 0;
        foreach (int i in A)
        {
            gcd = (int)GCD(gcd, i);
        }
        if (gcd == A[0])
        {
            int[] ans = new int[N * 2 - 1];
            for (int i = 0; i < N * 2 - 1; i++)
            {
                if(i%2==0)
                {
                    ans[i] = A[i / 2];
                }
                else
                {
                    ans[i] = A[0];
                }
            }
            Console.WriteLine(ans.Length);
            Console.WriteLine(string.Join(" ",ans));
        }
        else
        {
            Console.WriteLine(-1);
        }
    }

    public long GCD(long a, long b)
    {
        if (a < b)
        {
            var t = a;
            a = b;
            b = t;
        }
        while (b > 0)
        {
            long r = a % b;
            a = b;
            b = r;
        }
        return a;
    }

    static void Main(string[] args)
    {
        new Program().Solve();
    }
}