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
    private long N, S;
    private void Scan()
    {
        var line = Console.ReadLine().Split(' ');
        N = long.Parse(line[0]);
        S = long.Parse(line[1]);
    }

    public long C(long n)
    {
        int sum = 0;
        long p = n;
        while (p > 0)
        {
            sum += (int)(p % 10);
            p /= 10;
        }
        return n - sum;
    }

    public void Solve()
    {
        Scan();
        long ok = long.MaxValue;
        long ng = 0;
        while (ok - ng > 1)
        {
            long mid = (ok + ng) / 2;
            if (C(mid) >= S)
            {
                ok = mid;
            }
            else
            {
                ng = mid;
            }
        }
        Console.WriteLine(Math.Max(0, N - ok + 1));
    }
}
