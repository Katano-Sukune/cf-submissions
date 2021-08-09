using CompLib.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        int k = sc.NextInt();

        

        if (n == 2)
        {
            Console.WriteLine();
        }


        var e = new HashSet<int>[n];
        for (int i = 0; i < n; i++)
        {
            e[i] = new HashSet<int>();
        }

        for (int i = 0; i < n - 1; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            e[u].Add(v);
            e[v].Add(u);
        }

        if (k == 1)
        {
            Console.WriteLine(n - 1);
            return;
        }
        if(n == 2)
        {
            Console.WriteLine("0");
            return;
        }

        // vにつながってる葉
        var l = new Queue<int>[n];
        for (int i = 0; i < n; i++)
        {
            l[i] = new Queue<int>();
            foreach (var to in e[i])
            {
                if (e[to].Count == 1)
                {
                    l[i].Enqueue(to);
                }
            }
        }

        // つながってる葉がK個以上の頂点
        var q = new Queue<int>();
        for (int i = 0; i < n; i++)
        {
            if (l[i].Count >= k) q.Enqueue(i);
        }

        int ans = 0;
        while (q.Count > 0)
        {
            var d = q.Dequeue();
            int t = l[d].Count / k;
            ans += t;
            for (int i = 0; i < t * k; i++)
            {
                var leaf = l[d].Dequeue();
                e[d].Remove(leaf);
            }

            if (e[d].Count == 1)
            {
                int p = e[d].ToArray()[0];
                l[p].Enqueue(d);
                if (l[p].Count == k) q.Enqueue(p);
            }
        }
        Console.WriteLine(ans);
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
