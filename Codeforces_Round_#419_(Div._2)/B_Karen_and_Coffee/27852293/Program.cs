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
    private int K, N, Q;
    private int[] L, R, A, B;
    const int Max = 200000;
    private void Scan()
    {
        var line = Console.ReadLine().Split(' ');
        N = int.Parse(line[0]);
        K = int.Parse(line[1]);
        Q = int.Parse(line[2]);
        L = new int[N];
        R = new int[N];
        for (int i = 0; i < N; i++)
        {
            line = Console.ReadLine().Split(' ');
            L[i] = int.Parse(line[0]);
            R[i] = int.Parse(line[1]);
        }
        A = new int[Q];
        B = new int[Q];
        for (int i = 0; i < Q; i++)
        {
            line = Console.ReadLine().Split(' ');
            A[i] = int.Parse(line[0]);
            B[i] = int.Parse(line[1]);
        }
    }

    public void Solve()
    {
        Scan();
        int[] imos = new int[Max+2];
        for(int i = 0; i < N; i++)
        {
            imos[L[i]]++;
            imos[R[i] + 1]--;
        }
        for(int i  = 0; i <= Max; i++)
        {
            imos[i + 1] += imos[i];
        }
        int c = 0;
        int[] cnt = new int[Max + 1];
        for(int i = 0; i <= Max; i++)
        {
            if (imos[i] >= K)
            {
                c++;
            }
            cnt[i] = c;
        }
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < Q; i++)
        {
            int ans = cnt[B[i]] - cnt[A[i] - 1];
            sb.AppendLine(ans.ToString());
        }
        Console.Write(sb.ToString());
    }
}
