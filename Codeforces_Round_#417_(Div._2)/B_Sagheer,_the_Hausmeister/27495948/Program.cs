using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

class Program
{
    static void Main()
    {
        new Magatro().Solve();
    }
}

class Magatro
{
    private int N, M;
    private string[] S;
    private int[] L, R;
    private int top = 0;

    private int Cnt(int n)
    {
        int res = 0;
        int b = 0;
        for (int i = N - 1; i >= top; i--)
        {
            int c = n % 2;
            if(R[i] == 0)
            {
                continue;
            }
            if (i == top)
            {
                if (b == 0)
                {
                    res += R[i];
                }
                else
                {
                    res += L[i];
                }
            }
            else
            {
                if (b == 0)
                {
                    if (c == 0)
                    {
                        res += 2 * R[i];
                    }
                    else
                    {
                        res += M + 1;
                    }
                }
                else
                {
                    if (c == 0)
                    {
                        res += M + 1;
                    }
                    else
                    {
                        res += 2 * L[i];
                    }
                }
            }
            b = c;
            n /= 2;
        }

        return res + N - 1 - top;
    }

    public void Solve()
    {
        var line = Console.ReadLine().Split(' ');
        N = int.Parse(line[0]);
        M = int.Parse(line[1]);
        S = new string[N];
        for (int i = 0; i < N; i++)
        {
            S[i] = Console.ReadLine();
        }

        int loop = 1 << N;
        int ans = int.MaxValue;
        L = new int[N];
        R = new int[N];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M + 2; j++)
            {
                if (S[i][j] == '1')
                {
                    R[i] = j;
                }
                if (S[i][M - j + 1] == '1')
                {
                    L[i] = j;
                }
            }
        }
        for (int i = 0; i < N; i++)
        {
            top = i;
            if (R[i] != 0)
            {
                break;
            }
        }

        for (int i = 0; i < loop; i++)
        {
            ans = Math.Min(ans, Cnt(i));
        }
        Console.WriteLine(ans);
    }
}
