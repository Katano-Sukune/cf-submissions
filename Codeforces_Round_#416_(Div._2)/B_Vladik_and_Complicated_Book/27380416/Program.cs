using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Linq.Expressions;

static class Program
{
    static void Main()
    {
        new Magatro().Solve();
    }
}

class Magatro
{
    private int N, M;
    private int[] P;
    private int[] L, R, X;
    private void Scan()
    {
        var line = Console.ReadLine().Split(' ');
        N = int.Parse(line[0]);
        M = int.Parse(line[1]);
        P = new int[N];
        line = Console.ReadLine().Split(' ');
        for (int i = 0; i < N; i++)
        {
            P[i] = int.Parse(line[i]);
        }
        L = new int[M];
        R = new int[M];
        X = new int[M];
        for (int i = 0; i < M; i++)
        {
            line = Console.ReadLine().Split(' ');
            L[i] = int.Parse(line[0]) - 1;
            R[i] = int.Parse(line[1]) - 1;
            X[i] = int.Parse(line[2]) - 1;
        }
    }

    private bool Sol(int l,int r,int x)
    {
        int cnt = 0;
        int target = 0;
        for(int i = l; i < x; i++)
        {
            //Console.Write(i);
            if (P[x] < P[i])
            {
                cnt++;
            }
            if (P[x] == P[i])
            {
                target++;
            }
        }
        for(int i = r; i > x; i--)
        {
            //Console.Write(i);
            if (P[x] > P[i])
            {
                cnt--;
            }
            if (P[x] == P[i])
            {
                target++;
            }
        }
        return Math.Abs(cnt) <= target;
    }

    public void Solve()
    {
        Scan();
        for (int i = 0; i < M; i++)
        {
            Console.WriteLine(Sol(L[i], R[i], X[i]) ? "Yes" : "No");
        }
    }
}