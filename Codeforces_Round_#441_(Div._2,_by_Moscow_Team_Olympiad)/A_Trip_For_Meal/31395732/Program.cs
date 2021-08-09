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
    private int N, A, B, C;
    private int[] Dist;
    private void Scan()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.NextInt();
        B = sc.NextInt();
        C = sc.NextInt();
    }


    public void Solve()
    {
        Scan();
        var dp = new int[N, 3];//0 rabbit 1 owl 2 eeyore

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                dp[i, j] = 100000000;
            }
        }
        dp[0, 0] = 0;
        for (int i = 0; i < N - 1; i++)
        {

            for (int j = 0; j < 3; j++)
            {
                int t = dp[i, j];
                switch (j)
                {
                    case 0:
                        dp[i + 1, 1] = Math.Min(dp[i + 1, 1], t + A);
                        dp[i + 1, 2] = Math.Min(dp[i + 1, 2], t + B);
                        break;
                    case 1:
                        dp[i + 1, 0] = Math.Min(dp[i + 1, 0], t + A);
                        dp[i + 1, 2] = Math.Min(dp[i + 1, 2], t + C);
                        break;
                    case 2:
                        dp[i + 1, 1] = Math.Min(dp[i + 1, 1], t + C);
                        dp[i + 1, 0] = Math.Min(dp[i + 1, 0], t + B);
                        break;
                }
            }
        }
        Console.WriteLine(Math.Min(dp[N - 1, 0], Math.Min(dp[N - 1, 1], dp[N - 1, 2])));
    }

    static public void Main()
    {
        new Magatro().Solve();
    }
}
