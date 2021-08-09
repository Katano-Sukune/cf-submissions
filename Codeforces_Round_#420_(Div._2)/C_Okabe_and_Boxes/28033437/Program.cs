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
    string[] Q;
    private void Scan()
    {
        N = int.Parse(Console.ReadLine());
        Q = new string[2 * N];
        for (int i = 0; i < 2 * N; i++)
        {
            Q[i] = Console.ReadLine();
        }
    }

    public void Solve()
    {
        Scan();
        Stack<int> S = new Stack<int>();
        int c = 1;
        int ans = 0;
        foreach (var q in Q)
        {
            if (q == "remove")
            {
                if (S.Count > 0 && S.Peek() != c)
                {
                    ans++;
                    S.Clear();
                }
                else
                {
                    if (S.Count > 0)
                    {
                        S.Pop();
                    }
                }
                c++;
            }
            else
            {
                int n = int.Parse(q.Split(' ')[1]);
                S.Push(n);
            }
        }
        Console.WriteLine(ans);
    }
}
