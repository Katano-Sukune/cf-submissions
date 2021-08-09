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
    private string Zero = "What are you doing at the end of the world? Are you busy? Will you save us?";
    private string AddA = "What are you doing while sending \"";
    private string AddB = "\"? Are you busy? Will you send \"";
    private string AddC = "\"?";
    private int Q;
    private int[] N;
    private long[] K;
    private long[] Length;
    private void Scan()
    {
        var sc = new Scanner();
        Q = sc.NextInt();
        N = new int[Q];
        K = new long[Q];
        for (int i = 0; i < Q; i++)
        {
            N[i] = sc.NextInt();
            K[i] = sc.NextLong();
        }
    }

    private void CalcLength()
    {
        Length = new long[100001];
        Length[0] = Zero.Length;
        for (int i = 1; i <= 100000; i++)
        {
            Length[i] = Math.Min((Length[i - 1] * 2) + AddA.Length + AddB.Length + AddC.Length, (long)1e18 + 1);
        }
    }

    private char Func(int i, long index)
    {
        if (Length[i] <= index)
        {
            return '.';
        }
        if (i == 0)
        {
            return Zero[(int)index];
        }
        long a, b, c, d, e;
        a = AddA.Length;
        b = a + Length[i - 1];
        c = b + AddB.Length;
        d = c + Length[i - 1];
        e = d + AddC.Length;
        if (index < a)
        {
            return AddA[(int)index];
        }
        if (a <= index && index < b)
        {
            return Func(i - 1, index - a);
        }
        if (b <= index && index < c)
        {
            return AddB[(int)(index - b)];
        }
        if (c <= index && index < d)
        {
            return Func(i - 1, index - c);
        }
        if (d <= index && index < e)
        {
            return AddC[(int)(index - d)];
        }
        return '.';
    }

    public void Solve()
    {
        Scan();
        CalcLength();
        var ans = "";
        for (int i = 0; i < Q; i++)
        {
            ans += Func(N[i], K[i] - 1);
        }
        Console.WriteLine(ans);
    }



    static void Main(string[] args)
    {
        new Program().Solve();
    }
}