using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Graph;

public class Program
{
    private int N;
    private int[] X, Y;

    private int M;
    private int[] A, B, G;

    private List<(int to, int idx)>[] E;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        X = new int[N - 1];
        Y = new int[N - 1];
        for (int i = 0; i < N - 1; i++)
        {
            X[i] = sc.NextInt() - 1;
            Y[i] = sc.NextInt() - 1;
        }

        M = sc.NextInt();
        A = new int[M];
        B = new int[M];
        G = new int[M];
        for (int i = 0; i < M; i++)
        {
            A[i] = sc.NextInt() - 1;
            B[i] = sc.NextInt() - 1;
            G[i] = sc.NextInt();
        }

        E = new List<(int to, int idx)>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<(int to, int idx)>();
        }

        for (int i = 0; i < N - 1; i++)
        {
            E[X[i]].Add((Y[i], i));
            E[Y[i]].Add((X[i], i));
        }

        int[] ans = new int[N - 1];
        Array.Fill(ans, 1);

        for (int t = 0; t < M; t++)
        {
            int[] prev = new int[N];
            int[] pIdx = new int[N];
            Array.Fill(prev, -1);
            var q = new Queue<int>();
            q.Enqueue(A[t]);
            while (q.Count > 0)
            {
                var d = q.Dequeue();
                foreach ((int to, int idx) in E[d])
                {
                    if (prev[to] != -1) continue;
                    prev[to] = d;
                    pIdx[to] = idx;
                    q.Enqueue(to);
                }
            }

            int cur = B[t];
            while (cur != A[t])
            {
                ans[pIdx[cur]] = Math.Max(ans[pIdx[cur]], G[t]);
                cur = prev[cur];
            }
        }

        for (int t = 0; t < M; t++)
        {
            int[] prev = new int[N];
            int[] pIdx = new int[N];
            Array.Fill(prev, -1);
            var q = new Queue<int>();
            q.Enqueue(A[t]);
            while (q.Count > 0)
            {
                var d = q.Dequeue();
                foreach ((int to, int idx) in E[d])
                {
                    if (prev[to] != -1) continue;
                    prev[to] = d;
                    pIdx[to] = idx;
                    q.Enqueue(to);
                }
            }

            int min = int.MaxValue;
            int cur = B[t];
            while (cur != A[t])
            {
                min = Math.Min(min, ans[pIdx[cur]]);
                cur = prev[cur];
            }

            if (min != G[t])
            {
                Console.WriteLine("-1");
                return;
            }
        }

        Console.WriteLine(string.Join(" ", ans));
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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