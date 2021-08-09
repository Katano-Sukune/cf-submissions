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
    private int Q;
    private int[] N;
    private void Scan()
    {
        var sc = new Scanner();
        Q = sc.NextInt();
        N = new int[Q];
        for (int i = 0; i < Q; i++)
        {
            N[i] = sc.NextInt();
        }
    }

    private int Query(int n)
    {

        switch (n % 4)
        {
            case 0:
                return n / 4;
            case 2:
                if (n >= 6)
                    return n / 4;
                else return -1;
            case 1:
                if (n >= 9)
                    return n / 4 - 1;
                else return -1;
            case 3:
                if (n >= 15)
                    return n / 4 - 1;
                else return -1;
            default:
                return -1;
        }
    }

    public void Solve()
    {
        Scan();
        var sw = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false };
        Console.SetOut(sw);
        foreach (int i in N)
        {
            Console.WriteLine(Query(i));
        }
        Console.Out.Flush();
    }

    static public void Main()
    {
        new Magatro().Solve();
    }
}
