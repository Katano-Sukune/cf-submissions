using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Collections;
using CompLib.Graph;

public class Program
{
    private int N;
    private int[] V, L, R;
    private AdjacencyList E;
    private HashMap<int, int> Map;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        V = new int[N];
        L = new int[N];
        R = new int[N];
        for (int i = 0; i < N; i++)
        {
            V[i] = sc.NextInt();
            L[i] = sc.NextInt() - 1;
            R[i] = sc.NextInt() - 1;
        }

        /*
         * 二分木 T
         * t 現在地
         * x 探索してる値
         *
         * if t == null: return false
         * if t.value == x: return true
         * if x < t.value: 左の子に
         * else: 右の子に行く
         *
         * n頂点
         * 頂点i
         * 値 v_i
         * 左 l_i
         * 右 r_i
         *
         * vの各値探す
         *
         * 正しい値返さない個数
         */

        /*
         * 左の部分木
         * t.v 未満
         * 右 t.v以上
         */

        bool[] par = new bool[N];

        for (int i = 0; i < N; i++)
        {
            if (L[i] >= 0) par[L[i]] = true;
            if (R[i] >= 0) par[R[i]] = true;
        }

        int root = -1;
        for (int i = 0; i < N; i++)
        {
            if (!par[i]) root = i;
        }

        Map = new HashMap<int, int>();
        foreach (int i in V)
        {
            Map[i]++;
        }

        Console.WriteLine(N - Go(root, int.MinValue, int.MaxValue));
    }

    int Go(int cur, int min, int max)
    {
        int ans = 0;
        if (min <= V[cur] && V[cur] < max) ans += Map[V[cur]];
        if (L[cur] >= 0) ans += Go(L[cur], min, Math.Min(max, V[cur]));
        if (R[cur] >= 0) ans += Go(R[cur], Math.Max(min, V[cur] + 1), max);
        return ans;
    }

    // public static void Main(string[] args) => new Program().Solve();
    public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

namespace CompLib.Collections
{
    using System.Collections.Generic;

    public class HashMap<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public new TValue this[TKey key]
        {
            get
            {
                TValue o;
                return TryGetValue(key, out o) ? o : default(TValue);
            }
            set { base[key] = value; }
        }
    }
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