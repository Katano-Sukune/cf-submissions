using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Graph;

public class Program
{
    private int N;
    private long[][] C;
    private AdjacencyList E;

    private long[][] Cost;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        C = new long[3][];
        for (int i = 0; i < 3; i++)
        {
            C[i] = sc.LongArray();
        }

        E = new AdjacencyList(N);
        for (int i = 0; i < N - 1; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            E.AddUndirectedEdge(u, v);
        }

        E.Build();

        for (int i = 0; i < N; i++)
        {
            if (E[i].Length > 2)
            {
                Console.WriteLine("-1");
                return;
            }
        }

        int leaf = -1;
        for (int i = 0; i < N; i++)
        {
            if (E[i].Length == 1) leaf = i;
        }

        int[] t = new int[N];
        t[0] = leaf;
        for (int i = 0; i < N - 1; i++)
        {
            if (i == 0) t[i + 1] = E[leaf][0];
            else
            {
                t[i + 1] = t[i - 1] == E[t[i]][0] ? E[t[i]][1] : E[t[i]][0];
            }
        }

        long cost = long.MaxValue;
        int[] col = new int[N];

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (i == j) continue;
                int k = 3 - (i + j);
                int[] ar = {i, j, k};
                long tmp = 0;
                for (int l = 0; l < N; l++)
                {
                    int v = t[l];
                    tmp += C[ar[l % 3]][v];
                }

                // Console.WriteLine($"{i} {j} {k} {tmp}");
                if (tmp < cost)
                {
                    cost = tmp;
                    for (int l = 0; l < N; l++)
                    {
                        int v = t[l];
                        col[v] = ar[l % 3] + 1;
                    }
                }
            }
        }

        Console.WriteLine(cost);
        Console.WriteLine(string.Join(" ", col));
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