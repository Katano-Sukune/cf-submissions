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
    private void Scan()
    {
        N = int.Parse(Console.ReadLine());
        A = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
    }

    public void Solve()
    {
        Scan();
        Array.Sort(A);
        int cnt = 1;
        if(A[2] == A[1])
        {
            cnt++;
            if (A[1] == A[0])
            {
                cnt++;
            }
        }
        
        long ans = 1;
        int k = 0;
        foreach(int j in A)
        {
            if(j == A[2])
            {
                k++;
            }
        }
        for (int i = 0; i < cnt; i++)
        {
            ans *= k;
            ans /= (i + 1);
            k--;

        }
        Console.WriteLine(ans);
    }
}
