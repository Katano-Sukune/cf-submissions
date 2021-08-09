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
    static void Main(string[] args)
    {
        new Magatro().Solve();
    }
}



public class Magatro
{
    int N, T, K, D;
    private void Scan()
    {
        var line = Console.ReadLine().Split(' ');
        N = int.Parse(line[0]);
        T = int.Parse(line[1]);
        K = int.Parse(line[2]);
        D = int.Parse(line[3]);
    }

    public void Solve()
    {
        Scan();
        for (int i = 1; i < 10000; i++)
        {
            int time = T * i;
            int bake = K * i;

            if (bake >= N) break;

            if (time > D)
            {
                Console.WriteLine("YES");
                return;
            }
        }
        Console.WriteLine("NO");

    }
}