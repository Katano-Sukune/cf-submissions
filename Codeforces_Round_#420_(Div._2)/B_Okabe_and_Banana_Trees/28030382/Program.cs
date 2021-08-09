using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.IO;

class Program
{
    static void Main()
    {
        new Magatro().Solve();
    }
}


class Magatro
{
    private int M, B;
    private void Scan()
    {
        var line = Console.ReadLine().Split(' ');
        M = int.Parse(line[0]);
        B = int.Parse(line[1]);
    }

    private long C(long y, long x)
    {
        long a = (y * y + y) / 2 * (x + 1);
        long b = (x * x + x) / 2 * (y + 1);
        return a + b;
    }


    public void Solve()
    {
        Scan();
        long ans = -1;
        for (long y = 0; y <= B; y++)
        {
            long x = (B - y) * M;
            ans = Math.Max(ans, C(y, x));
        }
        Console.WriteLine(ans);
    }
}
