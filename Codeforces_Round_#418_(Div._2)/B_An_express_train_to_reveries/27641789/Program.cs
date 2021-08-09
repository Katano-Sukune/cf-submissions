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
    private int N;
    private int[] A;
    private int[] B;
    private void Scan()
    {
        N = int.Parse(Console.ReadLine());
        A = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
        B = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
    }

    public void Solve()
    {
        Scan();
        var ans = new int[N];
        List<int> index = new List<int>();
        bool[] b = new bool[N + 1];
        for (int i = 0; i < N; i++)
        {
            if (A[i] != B[i])
            {
                index.Add(i);
            }
            else
            {
                ans[i] = A[i];
                b[A[i]] = true;
            }
        }

        if (index.Count == 1)
        {
            int i = 0;
            for (i = 1; i <= N; i++)
            {
                if (!b[i])
                {
                    break;
                }
            }
            ans[index[0]] = i;
        }
        else
        {
            if (!b[A[index[0]]] && !b[B[index[1]]])
            {
                ans[index[0]] = A[index[0]];
                ans[index[1]] = B[index[1]];
            }
            else
            {
                ans[index[0]] = B[index[0]];
                ans[index[1]] = A[index[1]];
            }
        }

        Console.WriteLine(string.Join(" ", ans));
    }
}
