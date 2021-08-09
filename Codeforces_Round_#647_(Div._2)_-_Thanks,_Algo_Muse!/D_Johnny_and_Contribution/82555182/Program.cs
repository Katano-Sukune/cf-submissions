using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N, M;
    List<int>[] Edge;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        Edge = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            Edge[i] = new List<int>();
        }
        for (int i = 0; i < M; i++)
        {
            int a = sc.NextInt() - 1;
            int b = sc.NextInt() - 1;
            Edge[a].Add(b);
            Edge[b].Add(a);
        }

        int[] t = sc.IntArray();

        var ans = new int[N];
        for (int i = 0; i < N; i++)
        {
            ans[i] = i;
        }

        Array.Sort(ans, (l, r) => t[l].CompareTo(t[r]));

        int[] f = new int[N];
        for (int i = 0; i < N; i++)
        {
            f[i] = 1;
        }

        foreach (int i in ans)
        {
            if (t[i] != f[i])
            {
                Console.WriteLine("-1");
                return;
            }

            foreach (var to in Edge[i])
            {
                if (f[to] == f[i])
                {
                    f[to]++;
                }
            }
        }

        Console.WriteLine(string.Join(" ", ans.Select(i => i + 1)));
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
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(Separator);
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
            _line = Console.ReadLine().Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}
