using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, M;

    private const int K = 9;
    private const int L = 1 << K;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();


        int[] cnt = new int[L];
        for (int i = 0; i < N; i++)
        {
            int f = sc.NextInt();
            int b = 0;
            for (int j = 0; j < f; j++)
            {
                b |= 1 << (sc.NextInt() - 1);
            }

            cnt[b]++;
        }

        var ls = new List<(int idx, int c)>[L];
        for (int i = 0; i < L; i++)
        {
            ls[i] = new List<(int idx, int c)>();
        }

        for (int i = 0; i < M; i++)
        {
            int c = sc.NextInt();
            int r = sc.NextInt();
            int a = 0;
            for (int j = 0; j < r; j++)
            {
                a |= 1 << (sc.NextInt() - 1);
            }

            ls[a].Add((i, c));
        }

        for (int i = 0; i < L; i++)
        {
            ls[i].Sort((l, r) => l.c.CompareTo(r.c));
        }

        // iのピザを選ぶとdp[i]人満足
        int[] dp = new int[L];
        for (int i = 0; i < L; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                if ((i & j) == j) dp[i] += cnt[j];
            }
        }

        int max = int.MinValue;
        int cost = int.MaxValue;

        int j1 = -1;
        int j2 = -1;

        for (int i = 0; i < L; i++)
        {
            if (ls[i].Count == 0) continue;
            for (int j = 0; j < i; j++)
            {
                if (ls[j].Count == 0) continue;
                int t = dp[i | j];
                int c = ls[i][0].c + ls[j][0].c;

                // Console.WriteLine($"{i} {j} {t} {c}");

                if (max < t || (max == t && c < cost))
                {
                    max = t;
                    cost = c;
                    j1 = ls[i][0].idx;
                    j2 = ls[j][0].idx;
                }
            }

            if (ls[i].Count == 1) continue;
            int t2 = dp[i];
            int c2 = ls[i][0].c + ls[i][1].c;

            if (max < t2 || (max == t2 && c2 < cost))
            {
                max = t2;
                cost = c2;
                j1 = ls[i][0].idx;
                j2 = ls[i][1].idx;
            }
        }

        // Console.WriteLine($"{max} {cost}");
        Console.WriteLine($"{j1 + 1} {j2 + 1}");
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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
                string s;
                do
                {
                    s = Console.ReadLine();
                } while (s.Length == 0);

                _line = s.Split(Separator);
                _index = 0;
            }

            return _line[_index++];
        }

        public string ReadLine()
        {
            _index = _line.Length;
            return Console.ReadLine();
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