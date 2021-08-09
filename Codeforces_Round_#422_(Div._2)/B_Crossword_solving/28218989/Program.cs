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
    private int N, M;
    private string S,T;
    private int[] ans;
    private void Scan()
    {
        var line = Console.ReadLine().Split(' ');
        N = int.Parse(line[0]);
        M = int.Parse(line[1]);
        S = Console.ReadLine();
        T = Console.ReadLine();
    }

    private void Q(int i)
    {
        List<int> l = new List<int>();
        for(int j = 0; j < N; j++)
        {
            if (S[j] != T[i + j])
            {
                l.Add(j + 1);
            }
        }
        if(ans == null || ans.Length > l.Count)
        {
            ans = l.ToArray();
        }
    }

    public void Solve()
    {
        Scan();
        for(int i = 0; i <= M - N; i++)
        {
            Q(i);
        }
        Console.WriteLine("{0}\n{1}", ans.Length, string.Join(" ", ans));
    }
}
