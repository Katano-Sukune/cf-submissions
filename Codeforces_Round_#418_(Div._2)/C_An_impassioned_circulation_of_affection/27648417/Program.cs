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
    private int N, Q;
    string S;
    Dictionary<char, List<int>> Map = new Dictionary<char, List<int>>();

    private void Scan()
    {
        N = int.Parse(Console.ReadLine());
        S = Console.ReadLine();
        Q = int.Parse(Console.ReadLine());
    }
    private void Qu()
    {
        int m;
        char c;
        var line = Console.ReadLine().Split(' ');
        m = int.Parse(line[0]);
        c = line[1][0];
        int ans = -1;
        int cnt = 0;
        int left = 0;
        for (int i = 0; i < N; i++)
        {
            if (S[i] != c)
            {
                cnt++;
            }
            if (cnt <= m)
            {
                ans = Math.Max(ans, i - left + 1);
            }
            if (cnt > m)
            {
                for (; cnt > m; left++)
                {
                    if (S[left] != c)
                    {
                        cnt--;
                    }
                }
            }
        }
        Console.WriteLine(ans);
    }

    public void Solve()
    {
        Scan();

        for (int i = 0; i < Q; i++)
        {
            Qu();
        }
    }
}
