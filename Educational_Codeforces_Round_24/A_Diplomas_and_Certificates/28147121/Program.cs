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
    private long N, K;

    private void Scan()
    {
        var line = Console.ReadLine().Split(' ');
        N = long.Parse(line[0]);
        K = long.Parse(line[1]);
    }


    public void Solve()
    {
        Scan();
        long a = N / 2;
        long b = a / (K + 1);
        long c = b * K;
        long d = N - b - c;
        Console.WriteLine("{0} {1} {2}", b, c, d);
    }
}
