using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Graph;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int m = sc.NextInt();
        int k = sc.NextInt();
        int s = sc.NextInt();
        List<int>[] ls = new List<int>[k];
        for (int i = 0; i < k; i++)
        {
            ls[i] = new List<int>();
        }

        for (int i = 0; i < n; i++)
        {
            ls[sc.NextInt() - 1].Add(i);
        }

        AdjacencyList e = new AdjacencyList(n);
        for (int i = 0; i < m; i++)
        {
            e.AddUndirectedEdge(sc.NextInt() - 1, sc.NextInt() - 1);
        }

        e.Build();


        // 町iにjを持ってくるコスト
        int[] cost = new int[n * k];
        Array.Fill(cost, int.MaxValue);
        
        var q = new Queue<int>();
        for (int j = 0; j < k; j++)
        {
            foreach (int i in ls[j])
            {
                q.Enqueue(i);
                cost[i * k + j] = 0;
            }

            while (q.Count > 0)
            {
                int d = q.Dequeue();
                foreach (int to in e[d])
                {
                    if (cost[to * k + j] != int.MaxValue) continue;
                    cost[to * k + j] = cost[d * k + j] + 1;
                    q.Enqueue(to);
                }
            }
        }

        var ans = new int[n];
        for (int i = 0; i < n; i++)
        {
            int[] tmp = new int[k];
            for (int j = 0; j < k; j++)
            {
                tmp[j] = cost[i * k + j];
            }

            Array.Sort(tmp);
            for (int j = 0; j < s; j++)
            {
                ans[i] += tmp[j];
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