using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using CompLib.Graph;
using System.Collections;
using System.Collections.Generic;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
        Console.Out.Flush();
    }

    int N, M;
    AdjacencyList E;
    AdjacencyList Rev;
    int[] Dist;

    int[] DP;

    int[] DP2;

    void Q(Scanner sc)
    {
        N = sc.NextInt();
        M = sc.NextInt();
        E = new AdjacencyList(N);
        Rev = new AdjacencyList(N);
        for (int i = 0; i < M; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            E.AddDirectedEdge(u, v);
            Rev.AddDirectedEdge(v, u);
        }

        E.Build();
        Rev.Build();

        // 各頂点からの距離
        Dist = new int[N];
        Array.Fill(Dist, int.MaxValue);
        var q = new Queue<int>();
        Dist[0] = 0;
        q.Enqueue(0);
        while (q.Count > 0)
        {
            var cur = q.Dequeue();
            foreach (var to in E[cur])
            {
                if (Dist[to] != int.MaxValue) continue;
                Dist[to] = Dist[cur] + 1;
                q.Enqueue(to);
            }
        }

        DP = new int[N];
        DP2 = new int[N];
        Array.Fill(DP, int.MaxValue);
        Array.Fill(DP2, int.MaxValue);

        /*
         * u->vの有効辺
         * 
         * 小さい方への移動1回以下
         * 各頂点から行ける頂点　1からの距離最小
         */
        int[] ans = new int[N];
        for (int i = 0; i < N; i++)
        {
            ans[i] = Go2(i);
        }

        Console.WriteLine(string.Join(" ", ans));
    }

    int Go(int v)
    {
        if (DP[v] != int.MaxValue) return DP[v];
        int ans = Dist[v];
        foreach (var to in E[v])
        {
            if (Dist[to] <= Dist[v]) continue;

            ans = Math.Min(ans, Go(to));
        }
        DP[v] = ans;
        return ans;
    }

    int Go2(int v)
    {
        if (DP2[v] != int.MaxValue) return DP2[v];

        int ans = Dist[v];
        foreach (var to in E[v])
        {
            if (Dist[to] > Dist[v])
            {
                ans = Math.Min(ans, Go2(to));
            }
            else
            {
                ans = Math.Min(ans, Go(to));
            }
        }
        DP2[v] = ans;
        return ans;
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
