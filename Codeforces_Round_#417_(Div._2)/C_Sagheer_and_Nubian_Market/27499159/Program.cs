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

struct P
{
    public int Cost, Index;
    public P(int cost, int index)
    {
        Cost = cost;
        Index = index;
    }
}

class Magatro
{
    private int N, S;
    private int[] A;

    private bool Check(int n)
    {
        return S >= Cnt(n);
    }

    private long Cnt(int n)
    {
        long[] array = new long[N];
        for (int i = 0; i < N; i++)
        {
            array[i] = A[i] + (long)(i + 1) * n;
        }
        Array.Sort(array);
        long sum = 0;
        for (int i = 0; i < n; i++)
        {
            sum += array[i];
        }
        return sum;
    }

    public void Solve()
    {
        var line = Console.ReadLine().Split(' ');
        N = int.Parse(line[0]);
        S = int.Parse(line[1]);
        A = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

        int min = 0;
        int max = N + 1;
        while (max - min > 1)
        {
            int mid = (max + min) / 2;
            if (Check(mid))
            {
                min = mid;
            }
            else
            {
                max = mid;
            }
        }

        Console.WriteLine("{0} {1}",min,Cnt(min));
    }
}
