using System;
using System.Collections.Generic;
using CompLib.Util;

public class Program
{
    private const int MaxA = 200000;
    private int N;
    private int[] A;
    private List<int>[] E;
    private int[] Div;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();
        E = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<int>();
        }

        for (int i = 0; i < N - 1; i++)
        {
            int x = sc.NextInt() - 1;
            int y = sc.NextInt() - 1;
            E[x].Add(y);
            E[y].Add(x);
        }

        Div = new int[MaxA + 1];
        for (int i = 2; i * i <= MaxA; i++)
        {
            if (Div[i] == 0)
            {
                for (int j = i; j <= MaxA; j += i)
                {
                    Div[j] = i;
                }
            }
        }

        for (int i = 2; i <= MaxA; i++)
        {
            if (Div[i] == 0) Div[i] = i;
        }

        int ans = 0;
        for (int i = 0; i < N; i++)
        {
            while (A[i] != 1)
            {
                int p = Div[A[i]];
                int ff;
                F(i, -1, out ff, p);
                int d = F(ff, -1, out _, p);
                ans = Math.Max(ans, d);
                Go(i, -1, p);
            }
        }

        Console.WriteLine(ans);
    }

    // 現在地、親、一番遠いnode, prime, return 一番遠いnode
    int F(int cur, int par, out int node, int prime)
    {
        int max = 0;
        node = cur;
        foreach (int to in E[cur])
        {
            if (to == par || A[to] % prime != 0) continue;
            int tmp2;
            int tmp = F(to, cur, out tmp2, prime);
            if (max < tmp)
            {
                max = tmp;
                node = tmp2;
            }
        }

        return max + 1;
    }

    void Go(int cur, int par, int prime)
    {
        while (A[cur] % prime == 0)
        {
            A[cur] /= prime;
        }

        foreach (int to in E[cur])
        {
            if (to == par || A[to] % prime != 0) continue;
            Go(to, cur, prime);
        }
    }

    public static void Main(string[] args) => new Program().Solve();
}

namespace CompLib.Util
{
    using System;
    using System.Linq;

    class Scanner
    {
        private string[] _line;
        private int _index;
        private const char Separator = ' ';

        public Scanner()
        {
            _line = new string[0];
            _index = 0;
        }

        public string Next()
        {
            if (_index >= _line.Length)
            {
                string s = Console.ReadLine();
                while (s.Length == 0)
                {
                    s = Console.ReadLine();
                }

                _line = s.Split(Separator);
                _index = 0;
            }

            return _line[_index++];
        }

        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public decimal NextDecimal() => decimal.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            string s = Console.ReadLine();
            _line = s.Length == 0 ? new string[0] : s.Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}