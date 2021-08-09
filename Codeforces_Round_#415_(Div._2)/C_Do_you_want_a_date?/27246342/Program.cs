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

struct P
{
    public int Num, Value;
    public P(int num, int value)
    {
        Num = num;
        Value = value;
    }
}

class Magatro
{

    public void Solve()
    {
        int N = int.Parse(Console.ReadLine());
        var X = Console.ReadLine().Split(' ').Select(long.Parse).ToArray();
        int Mod = 1000000007;
        Array.Sort(X);
        long ans = 0;
        long a = 1;
        for (int i = 0; i < N; i++)
        {
            ans += (X[i] * (a+Mod-1)) % Mod;
            ans %= Mod;
            a *= 2;
            a %= Mod;
        }
        a = 1;
        for (int i = N - 1; i >= 0; i--)
        {
            ans -= (X[i] * (a+Mod-1)) % Mod;
            if (ans < 0)
            {
                ans += Mod;
            }
            a *= 2;
            a %= Mod;
        }
        Console.WriteLine(ans);
    }
}


