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
    private int N;
    private int[][] A;
    private void Scan()
    {
        N = int.Parse(Console.ReadLine());
        A = new int[N][];
        for (int i = 0; i < N; i++)
        {
            A[i] = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
        }
    }

    bool C(int x, int y)
    {
        if (A[y][x] == 1)
        {
            return true;
        }
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (i == y || j == x)
                {
                    continue;
                }
                int a = A[i][x];
                int b = A[y][j];
                if (a + b == A[y][x])
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void Solve()
    {
        Scan();
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (!C(i, j))
                {
                    Console.WriteLine("No");
                    return;
                }
            }
        }
        Console.WriteLine("Yes");
    }
}
