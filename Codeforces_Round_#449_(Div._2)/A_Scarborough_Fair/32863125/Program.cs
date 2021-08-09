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
    private int N, M;
    private string S;
    private int[] L, R;
    private char[] C1, C2;
    private void Scan()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        S = sc.Next();
        L = new int[M];
        R = new int[M];
        C1 = new char[M];
        C2 = new char[M];
        for (int i = 0; i < M; i++)
        {
            L[i] = sc.NextInt() - 1;
            R[i] = sc.NextInt() - 1;
            C1[i] = sc.Next()[0];
            C2[i] = sc.Next()[0];
        }
    }

    public void Solve()
    {
        Scan();
        var ans = S.ToArray();
        for (int i = 0; i < M; i++)
        {
            for (int j = L[i]; j <= R[i]; j++)
            {
                if (ans[j] == C1[i])
                {
                    ans[j] = C2[i];
                }
            }
        }
        Console.WriteLine(new string(ans));
    }

    static void Main(string[] args)
    {
        new Program().Solve();
    }
}