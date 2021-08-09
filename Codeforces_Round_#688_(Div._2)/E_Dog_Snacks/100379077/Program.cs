using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Graph;

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

    private int N;
    private int[] U, V;
    private List<int>[] E;

    void Q(Scanner sc)
    {
        N = sc.NextInt();
        U = new int[N - 1];
        V = new int[N - 1];
        for (int i = 0; i < N - 1; i++)
        {
            U[i] = sc.NextInt() - 1;
            V[i] = sc.NextInt() - 1;
        }

        E = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<int>();
        }

        for (int i = 0; i < N - 1; i++)
        {
            E[U[i]].Add(V[i]);
            E[V[i]].Add(U[i]);
        }

        Console.WriteLine(Go(0, -1).k);
        // 一番
    }

    (int k, int min) Go(int cur, int par)
    {
        // curの子 to
        // 部分木toの一番浅い頂点 + 1
        var ls = new List<(int k, int min)>();
        foreach (var to in E[cur])
        {
            if (to == par) continue;
            ls.Add(Go(to, cur));
            //
            // if (cur == 0 && N == 4)
            // {
            //     Console.WriteLine($"aaa {ls[^1].k} {ls[^1].min}");
            // }
        }


        ls.Sort((l, r) => l.min.CompareTo(r.min));
        int k = 1;
        if (cur == 0)
        {
            for (int i = 0; i < ls.Count; i++)
            {
                k = Math.Max(k, ls[i].k);
                if (i < ls.Count - 1) k = Math.Max(k, ls[i].min + 1);
                else k = Math.Max(k, ls[i].min);
            }
        }
        else
        {
            for (int i = 0; i < ls.Count; i++)
            {
                k = Math.Max(k, ls[i].k);
                if (i >= 1) k = Math.Max(k, ls[i].min + 1);
            }
        }


        int min = 1;
        if (ls.Count >= 1) min = ls[0].min + 1;
        return (k, min);
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