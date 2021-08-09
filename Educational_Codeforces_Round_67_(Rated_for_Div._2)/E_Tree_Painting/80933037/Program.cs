using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CompLib.Util;

public class Program
{
    int N;
    List<int>[] E;

    // 0を根としたときの部分木のスコア、サイズ
    long[] Score;
    int[] Size;

    long Ans;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        E = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<int>();
        }

        for (int i = 0; i < N - 1; i++)
        {
            int a = sc.NextInt() - 1;
            int b = sc.NextInt() - 1;
            E[a].Add(b);
            E[b].Add(a);
        }

        Score = new long[N];
        Size = new int[N];

        DFS1(0, -1);

        Ans = long.MinValue;

        DFS2(0, -1, 0);

        Console.WriteLine(Ans);
    }

    void DFS1(int cur, int par)
    {
        Score[cur] = 1;
        Size[cur] = 1;
        foreach (var to in E[cur])
        {
            if (to == par) continue;
            DFS1(to, cur);
            Score[cur] += Score[to] + Size[to];
            Size[cur] += Size[to];
        }
    }


    void DFS2(int cur, int par, long p)
    {
        foreach (var to in E[cur])
        {
            if (par == to) continue;
            DFS2(to, cur, p + N + Score[cur] - Score[to] - 2 * Size[to]);
        }
        Ans = Math.Max(Ans, Score[cur] + p);
    }
    public static void Main(string[] args)
    {
        // new Program().Solve();
        var t = new Thread(new Program().Solve, 1 << 27);
        t.Start();
    }
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
