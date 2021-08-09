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
    private int N, M;
    private int[] A, B;
    private void Scan()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        A = new int[N];
        B = new int[M];
        for (int i = 0; i < N; i++)
        {
            A[i] = sc.NextInt();
        }
        for (int i = 0; i < M; i++)
        {
            B[i] = sc.NextInt();
        }
    }

    public void Solve()
    {
        Scan();
        for(int i=1;i<=9;i++)
        {
            if(A.Contains(i)&&B.Contains(i))
            {
                Console.WriteLine(i);
                return;
            }
        }
        var aMin = A.Min();
        var bMin = B.Min();
        Console.WriteLine(Math.Min(aMin * 10 + bMin, bMin * 10 + aMin));
    }

    static public void Main()
    {
        new Magatro().Solve();
    }
}
