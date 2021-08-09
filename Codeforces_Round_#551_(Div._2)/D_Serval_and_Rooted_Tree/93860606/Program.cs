using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Graph;

public class Program
{
    private int N;
    private int[] F;
    private AdjacencyList E;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        F = sc.IntArray();
        E = new AdjacencyList(N);
        for (int i = 1; i < N; i++)
        {
            E.AddDirectedEdge(sc.NextInt() - 1, i);
        }

        E.Build();

        var t = Go(0);
        Console.WriteLine(t.cnt - t.rank + 1);
    }

    // 葉の個数, 何位のが帰るか?
    (int cnt, int rank) Go(int cur)
    {
        if (E[cur].Length == 0)
        {
            return (1, 1);
        }

        if (F[cur] == 1)
        {
            int cnt = 0;
            int rank = int.MaxValue;
            foreach (var to in E[cur])
            {
                var tuple = Go(to);
                cnt += tuple.cnt;
                rank = Math.Min(rank, tuple.rank);
            }

            return (cnt, rank);
        }
        else
        {
            int cnt = 0;
            int rank = 0;
            foreach (var to in E[cur])
            {
                var tuple = Go(to);
                cnt += tuple.cnt;
                rank += tuple.rank;
            }

            return (cnt, rank);
        }
    }

    // public static void Main(string[] args) => new Program().Solve();
    public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

namespace CompLib.Graph
{
    using System;
    using System.Collections.Generic;

    class AdjacencyList
    {
        private readonly int _n;
        private readonly List<(int f, int t)> _edges;

        private int[] _start;
        private int[] _eList;

        public AdjacencyList(int n)
        {
            _n = n;
            _edges = new List<(int f, int t)>();
        }

        public void AddDirectedEdge(int from, int to)
        {
            _edges.Add((from, to));
        }

        public void AddUndirectedEdge(int f, int t)
        {
            AddDirectedEdge(f, t);
            AddDirectedEdge(t, f);
        }

        public void Build()
        {
            _start = new int[_n + 1];
            foreach (var e in _edges)
            {
                _start[e.f + 1]++;
            }

            for (int i = 1; i <= _n; i++)
            {
                _start[i] += _start[i - 1];
            }

            int[] counter = new int[_n + 1];
            _eList = new int[_edges.Count];

            foreach (var e in _edges)
            {
                _eList[_start[e.f] + counter[e.f]++] = e.t;
            }
        }

        public ReadOnlySpan<int> this[int f]
        {
            get { return _eList.AsSpan(_start[f], _start[f + 1] - _start[f]); }
        }
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