using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

static public class Program
{
    static void Main()
    {
        new Magatro().Solve();
    }
}

class Magatro
{
    private int N;
    private int[] A, B;
    private void Scan()
    {
        N = int.Parse(Console.ReadLine());
        A = new int[N];
        B = new int[N];
        for (int i = 0; i < N; i++)
        {
            var line = Console.ReadLine().Split(' ');
            A[i] = int.Parse(line[0]);
            B[i] = int.Parse(line[1]);
        }
    }

    public void Solve()
    {
        Scan();
        bool flag = true;
        for (int i = 0; i < N; i++)
        {
            if (A[i] != B[i])
            {
                Console.WriteLine("rated");
                return;
            }
            if (i > 0)
            {
                if (A[i] > A[i - 1])
                {
                    flag = false;
                }
            }
        }
        Console.WriteLine(flag ? "maybe" : "unrated");
    }
}
