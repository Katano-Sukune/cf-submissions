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
    private int N, K;
    private int[] A;
    private int[] B;
    private void Scan()
    {
        var line = Console.ReadLine().Split(' ');
        N = int.Parse(line[0]);
        K = int.Parse(line[1]);
        A = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
        B = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
    }

    public void Solve()
    {
        Scan();
        Array.Sort(B, (a, b) => -a.CompareTo(b));
        int index = 0;
        for(int i = 0; i < N; i++)
        {
            if(A[i] == 0)
            {
                A[i] = B[index];
                index++;
            }
            if (i > 0)
            {
                if (A[i - 1] > A[i])
                {
                    Console.WriteLine("Yes");
                    return;
                }
            }
        }
        Console.WriteLine("No");
    }
}
