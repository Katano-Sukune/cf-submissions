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

struct S
{
    public long I;
    public long Last;
    public S(long i, long last)
    {
        this.I = i;
        this.Last = last;
    }
}

class Program
{
    private int N;
    private int[] X;
    private void Scan()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        X = new int[N];
        for (int i = 0; i < N; i++)
        {
            X[i] = sc.NextInt();
            sc.NextInt();
        }
    }

    public void Solve()
    {
        Scan();
        int plus = 0, minus = 0;
        foreach (int i in X)
        {
            if (i > 0)
            {
                plus++;
            }
            else
            {
                minus++;
            }
        }
        if (plus <= 1 || minus <= 1)
        {
            Console.WriteLine("Yes");
        }
        else
        {
            Console.WriteLine("No");
        }
    }

    static void Main(string[] args)
    {
        new Program().Solve();
    }
}