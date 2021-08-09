using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    private int N;
    private long[] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.LongArray().Where(l => l != 0).ToArray();
        N = A.Length;
        HashSet<(int s, int t)> edge = new HashSet<(int s, int t)>();
        for (int i = 0; i < 60; i++)
        {
            long b = 1L << i;
            var ls = new List<int>();
            for (int j = 0; j < N; j++)
            {
                if ((A[j] & b) > 0) ls.Add(j);
            }

            if (ls.Count >= 3)
            {
                Console.WriteLine("3");
                return;
            }

            if (ls.Count == 2)
            {
                edge.Add((ls[0], ls[1]));
            }
        }

        long[,] dist = new long[N, N];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (i == j) continue;
                dist[i, j] = int.MaxValue;
            }
        }

        long ans = int.MaxValue;
        foreach ((int s, int t) pair in edge)
        {
            ans = Math.Min(ans, dist[pair.s, pair.t] + 1);
            dist[pair.s, pair.t] = 1;
            dist[pair.t, pair.s] = 1;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    dist[i, j] = Math.Min(dist[i, j],
                        Math.Min(dist[i, pair.s] + 1 + dist[pair.t, j], dist[i, pair.t] + 1 + dist[pair.s, j]));
                }
            }
        }

        if (ans == int.MaxValue) Console.WriteLine("-1");
        else Console.WriteLine(ans);
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