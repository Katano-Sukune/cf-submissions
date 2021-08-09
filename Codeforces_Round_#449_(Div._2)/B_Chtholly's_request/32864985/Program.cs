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
    private int K, P;
    private void Scan()
    {
        var sc = new Scanner();
        K = sc.NextInt();
        P = sc.NextInt();
    }

    public void Solve()
    {
        Scan();
        long ans = 0;
        for(int i=1;i<=K;i++)
        {
            ans += Num(i);
            ans %= P;
        }
        Console.WriteLine(ans);
    }

    private long Num(int i)
    {
        long ret = i;
        while (i > 0)
        {
            ret *= 10;
            ret += i % 10;
            i /= 10;
            ret %= P;
        }
        return ret;
    }

    static void Main(string[] args)
    {
        new Program().Solve();
    }
}