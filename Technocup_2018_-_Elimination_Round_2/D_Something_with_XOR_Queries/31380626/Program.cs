using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.IO;
using System.Diagnostics;

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

    private long NextLong()
    {
        return long.Parse(Next());
    }
}

class Magatro
{
    private int N;
    private Scanner Sc;
    private int[] P, B;
    private void Scan()
    {
        Sc = new Scanner();
        N = Sc.NextInt();
    }

    private int Q(int p, int b)
    {
        Console.WriteLine("? {0} {1}", p, b);
        return int.Parse(Console.ReadLine());
    }

    private int QQ(int p, int b)
    {
        return P[p] ^ B[b] ^ P[0];
    }

    private bool F(int n, ref int[] result)
    {
        var p = new int[N];
        var b = new int[N];
        p[0] = n;
        b[0] = n ^ QQ(0, 0);
        for (int i = 1; i < N; i++)
        {
            p[i] = b[0] ^ QQ(i, 0);
            b[i] = p[0] ^ QQ(0, i);
        }
        var hs = new HashSet<int>();

        for (int i = 0; i < N; i++)
        {
            if (!hs.Add(p[i]))
            {
                return false;
            }
            if (0 > p[i] || p[i] >= N)
            {
                return false;
            }
            if (0 > b[i] || b[i] >= N)
            {
                return false;
            }
            if (p[b[i]] != i)
            {
                return false;
            }
        }
        result = p.ToArray();
        //Console.WriteLine(string.Join(" ", p));
        return true;
    }

    public void Solve()
    {
        Scan();
        P = new int[N];
        B = new int[N];
        for (int i = 0; i < N; i++)
        {
            P[i] = Q(i, 0);
            if (i > 0)
            {
                B[i] = Q(0, i);
            }
            else
            {
                B[i] = P[i];
            }
        }
        int cnt = 0;
        int[] ans = new int[N];
        for (int i = 0; i < N; i++)
        {
            if (F(i, ref ans))
            {
                cnt++;
            }
        }
        Console.WriteLine("!\n{0}\n{1}", cnt, string.Join(" ", ans));
    }

    static public void Main()
    {
        new Magatro().Solve();
    }
}
