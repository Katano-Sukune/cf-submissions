using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using CompLib.Graph;
using System.Collections.Generic;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
#if !DEBUG
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
#endif
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
        Console.Out.Flush();
    }

    int N;
    AdjacencyList E;

    int[] Depth;
    int[] Size;
    List<int>[] A;

    void Q(Scanner sc)
    {
        N = sc.NextInt();
        E = new AdjacencyList(N);
        for (int i = 0; i < N - 1; i++)
        {
            E.AddUndirectedEdge(sc.NextInt(), sc.NextInt());
        }

        E.Build();

        Depth = new int[N];
        Size = new int[N];
        A = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            A[i] = new List<int>();
        }

        Go(0, -1);

        long[] t = new long[N + 1];
        t[0] = (long)(N - 1) * N / 2;
        {
            long tmp = 1;
            foreach (int to in E[0])
            {
                t[1] += tmp * Size[to];
                tmp += Size[to];
            }
        }

        int l = 0;
        int r = 0;

        for (int i = 1; i < N; i++)
        {
            int li = LCA(l, i);
            int ri = LCA(r, i);

            if (li == i)
            {
                // iがパス(0,l)にある
            }
            else if (ri == i)
            {
                // (0,r)にある
            }
            else if (li == l && ri == 0)
            {
                // iはlの子孫
                l = i;
            }
            else if (ri == r && li == 0)
            {
                // rの子孫 
                r = i;
            }
            else
            {
                break;
            }

            if (l == 0)
            {
                int s = GetA(r, Depth[r] - 1);
                t[i + 1] = (long)Size[r] * (N - Size[s]);
            }
            else if (r == 0)
            {
                int s = GetA(l, Depth[l] - 1);
                t[i + 1] = (long)Size[l] * (N - Size[s]);
            }
            else
            {
                t[i + 1] = (long)Size[l] * (Size[r]);
            }
        }

        // Console.WriteLine(string.Join(" ", t));
        for (int i = 0; i < N; i++)
        {
            t[i] -= t[i + 1];
        }

        Console.WriteLine(string.Join(" ", t));
    }

    void Go(int cur, int par)
    {
        Size[cur] = 1;
        if (Depth[cur] > 0)
        {
            for (int i = 0; i < A[A[cur][i]].Count; i++)
            {
                A[cur].Add(A[A[cur][i]][i]);
            }
        }

        foreach (int to in E[cur])
        {
            if (to == par) continue;
            A[to].Add(cur);
            Depth[to] = Depth[cur] + 1;
            Go(to, cur);
            Size[cur] += Size[to];
        }
    }

    // vのd個上
    int GetA(int v, int d)
    {
        int b = 0;
        while (d > 0)
        {
            if (d % 2 == 1) v = A[v][b];
            b++;
            d /= 2;
        }
        return v;
    }

    int LCA(int v, int w)
    {
        if (Depth[v] > Depth[w])
        {
            (v, w) = (w, v);
        }

        if (Depth[w] > Depth[v])
        {
            w = GetA(w, Depth[w] - Depth[v]);
        }
        if (v == w) return v;
        for (int i = A[w].Count - 1; i >= 0; i--)
        {
            if (i >= A[w].Count) continue;
            if (A[v][i] == A[w][i]) continue;
            (v, w) = (A[v][i], A[w][i]);
        }

        return A[v][0];
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