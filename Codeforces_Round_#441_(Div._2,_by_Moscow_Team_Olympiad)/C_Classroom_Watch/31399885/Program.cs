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
    private void Scan()
    {
        var sc = new Scanner();
        N = sc.NextInt();
    }

    private int Sum(int n)
    {
        if (n <= 9)
        {
            return n;
        }
        else
        {
            return n % 10 + Sum(n / 10);
        }
    }

    public void Solve()
    {
        Scan();
        var sb = new StringBuilder();
        int cnt = 0;
        for (int i = 100; i >= 0; i--)
        {
            int a = N - i;
            if(Sum(a)==i)
            {
                cnt++;
                sb.AppendLine(a.ToString());
            }
        }
        Console.WriteLine(cnt);
        if(cnt!=0)
        {
            Console.Write(sb.ToString());
        }
    }

    static public void Main()
    {
        new Magatro().Solve();
    }
}
