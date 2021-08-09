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

    public void Solve()
    {
        int N, K;
        int[] A;
        var line = Console.ReadLine().Split(' ');
        N = int.Parse(line[0]);
        K = int.Parse(line[1]);
        A = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
        int sum = A.Sum();
        for (int i = 0; i < 200000; i++)
        {
            decimal s = (decimal)sum / (decimal)(N + i);
            if (Math.Round(s, MidpointRounding.AwayFromZero) >= K)
            {
                Console.WriteLine(i);
                return;
            }

            sum += K;
        }
    }
}