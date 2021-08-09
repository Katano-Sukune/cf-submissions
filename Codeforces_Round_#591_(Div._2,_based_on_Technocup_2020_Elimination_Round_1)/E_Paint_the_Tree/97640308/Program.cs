using System;
using System.Collections.Generic;
using System.IO;
using CompLib.Util;
using CompLib.Graph;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();

        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    private int N, K;
    private AdjacencyList E;

    private Array2D Memo;

    void Q(Scanner sc)
    {
        N = sc.NextInt();
        K = sc.NextInt();

        E = new AdjacencyList(N);

        for (int i = 0; i < N - 1; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            int w = sc.NextInt();
            E.AddUndirectedEdge(u, v, w);
        }

        E.Build();

        /*
         * 木
         *
         * 各頂点にk個色を割り当て
         * 各色2回まで
         *
         * 辺の端点に同じ色がある辺のWの総和最大
         * 
         */

        /*
         * 
         */

        Memo = new Array2D(N, 2);
        for (int i = 0; i < N; i++)
        {
            Memo[i, 0] = -1;
            Memo[i, 1] = -1;
        }

        Console.WriteLine(Go(0, -1, 0));
    }

    // 現在地、親、親との辺を使ったか?
    long Go(int cur, int par, int f)
    {
        if (Memo[cur, f] != -1) return Memo[cur, f];
        long result = 0;


        List<long> p = new List<long>(Math.Max(0, E[cur].Length - 1));
        foreach ((int to, int w) in E[cur])
        {
            if (to == par) continue;
            long tt = Go(to, cur, 1) + w;
            long ff = Go(to, cur, 0);
            if (tt > ff) p.Add(tt - ff);
            result += ff;
        }

        p.Sort((l, r) => r.CompareTo(l));

        for (int i = 0; i < p.Count && i < K - f; i++)
        {
            result += p[i];
        }

        // Console.WriteLine($"{cur} {f} {result}");
        Memo[cur, f] = result;
        return result;
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

class Array2D
{
    public readonly int R, C;
    private readonly long[] T;

    public Array2D(int r, int c)
    {
        R = r;
        C = c;
        T = new long[R * C];
    }

    public long this[int i, int j]
    {
        get { return T[i * C + j]; }
        set { T[i * C + j] = value; }
    }
}

namespace CompLib.Graph
{
    using System;
    using System.Collections.Generic;

    class AdjacencyList
    {
        private readonly int _n;
        private readonly List<(int f, int t, int w)> _edges;

        private int[] _start;
        private (int to, int w)[] _eList;

        public AdjacencyList(int n)
        {
            _n = n;
            _edges = new List<(int f, int t, int w)>();
        }

        public void AddDirectedEdge(int from, int to, int w)
        {
            _edges.Add((from, to, w));
        }

        public void AddUndirectedEdge(int f, int t, int w)
        {
            AddDirectedEdge(f, t, w);
            AddDirectedEdge(t, f, w);
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
            _eList = new (int to, int w)[_edges.Count];

            foreach (var e in _edges)
            {
                _eList[_start[e.f] + counter[e.f]++] = (e.t, e.w);
            }
        }

        public ReadOnlySpan<(int to, int w)> this[int f]
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