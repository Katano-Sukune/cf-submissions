using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.Collections.Generic;
using CompLib.Graph;
using CompLib.Mathematics;

public class Program
{
    int N;
    AdjacencyList E;
    ModInt[,] DP, DP2;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        E = new AdjacencyList(N);
        for (int i = 0; i < N - 1; i++)
        {
            E.AddUndirectedEdge(sc.NextInt() - 1, sc.NextInt() - 1);
        }
        E.Build();

        DP = new ModInt[N + 2, N + 2];
        var half = ModInt.Inverse(2);
        DP[0, 0] = 1;
        for (int i = 0; i <= N; i++)
        {
            for (int j = 0; j <= N; j++)
            {
                DP[i + 1, j] += DP[i, j] * half;
                DP[i, j + 1] += DP[i, j] * half;
            }
        }

        DP2 = new ModInt[N + 1, N + 1];
        for (int i = 0; i <= N; i++)
        {
            for (int j = 0; i + j <= N; j++)
            {
                if (i == 0) DP2[i, j] = 1;
                else
                {
                    for (int k = 0; k < j; k++)
                    {
                        DP2[i, j] += DP[i - 1, k] * half;
                    }
                }
            }
        }
        // Console.WriteLine(DP[1, 0]);
        // Console.WriteLine(DP2[1, 2]);
        // Console.WriteLine(3 * ModInt.Inverse(4));
        // 頂点 a,b a < b
        // aより先にbが選ばれる
        // 
        // 最初に選ばれるやつ s
        // b側の頂点 1
        // 
        // a側 0
        // 
        // 間
        // 
        // sを根としたa,bのLCA
        // 
        // 
        ModInt sum = 0;
        for (int i = 0; i < N; i++)
        {
            sum += F(i);
        }

        Console.WriteLine(sum * ModInt.Inverse(N));
    }

    int[] Depth;
    int[] Par;
    ModInt F(int s)
    {
        Depth = new int[N];
        Par = new int[N];
        Array.Fill(Depth, -1);
        var q = new Queue<int>();
        q.Enqueue(s);
        Depth[s] = 0;

        while (q.Count > 0)
        {
            int cur = q.Dequeue();
            foreach (int to in E[cur])
            {
                if (Depth[to] != -1) continue;
                Par[to] = cur;
                Depth[to] = Depth[cur] + 1;
                q.Enqueue(to);
            }
        }

        ModInt ret = 0;
        for (int a = 0; a < N; a++)
        {
            for (int b = 0; b < a; b++)
            {
                // bよりaが先に選ばれる確率
                int lca = LCA(a, b);
                int distA = Depth[a] - Depth[lca];
                int distB = Depth[b] - Depth[lca];

                ret += DP2[distA, distB];

            }
        }

        return ret;
    }

    int LCA(int v, int w)
    {

        while (Depth[v] > Depth[w]) v = Par[v];
        while (Depth[w] > Depth[v]) w = Par[w];
        while (v != w)
        {
            v = Par[v];
            w = Par[w];
        }
        return v;
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

namespace CompLib.Mathematics
{
    #region ModInt
    /// <summary>
    /// [0,<see cref="Mod"/>) までの値を取るような数
    /// </summary>
    public struct ModInt
    {
        /// <summary>
        /// 剰余を取る値．
        /// </summary>
        public const long Mod = (int)1e9 + 7;
        // public const long Mod = 998244353;

        /// <summary>
        /// 実際の数値．
        /// </summary>
        public long num;
        /// <summary>
        /// 値が <paramref name="n"/> であるようなインスタンスを構築します．
        /// </summary>
        /// <param name="n">インスタンスが持つ値</param>
        /// <remarks>パフォーマンスの問題上，コンストラクタ内では剰余を取りません．そのため，<paramref name="n"/> ∈ [0,<see cref="Mod"/>) を満たすような <paramref name="n"/> を渡してください．このコンストラクタは O(1) で実行されます．</remarks>
        public ModInt(long n) { num = n; }
        /// <summary>
        /// このインスタンスの数値を文字列に変換します．
        /// </summary>
        /// <returns>[0,<see cref="Mod"/>) の範囲内の整数を 10 進表記したもの．</returns>
        public override string ToString() { return num.ToString(); }
        public static ModInt operator +(ModInt l, ModInt r) { l.num += r.num; if (l.num >= Mod) l.num -= Mod; return l; }
        public static ModInt operator -(ModInt l, ModInt r) { l.num -= r.num; if (l.num < 0) l.num += Mod; return l; }
        public static ModInt operator *(ModInt l, ModInt r) { return new ModInt(l.num * r.num % Mod); }
        public static implicit operator ModInt(long n) { n %= Mod; if (n < 0) n += Mod; return new ModInt(n); }

        /// <summary>
        /// 与えられた 2 つの数値からべき剰余を計算します．
        /// </summary>
        /// <param name="v">べき乗の底</param>
        /// <param name="k">べき指数</param>
        /// <returns>繰り返し二乗法により O(N log N) で実行されます．</returns>
        public static ModInt Pow(ModInt v, long k) { return Pow(v.num, k); }

        /// <summary>
        /// 与えられた 2 つの数値からべき剰余を計算します．
        /// </summary>
        /// <param name="v">べき乗の底</param>
        /// <param name="k">べき指数</param>
        /// <returns>繰り返し二乗法により O(N log N) で実行されます．</returns>
        public static ModInt Pow(long v, long k)
        {
            long ret = 1;
            for (k %= Mod - 1; k > 0; k >>= 1, v = v * v % Mod)
                if ((k & 1) == 1) ret = ret * v % Mod;
            return new ModInt(ret);
        }
        /// <summary>
        /// 与えられた数の逆元を計算します．
        /// </summary>
        /// <param name="v">逆元を取る対象となる数</param>
        /// <returns>逆元となるような値</returns>
        /// <remarks>法が素数であることを仮定して，フェルマーの小定理に従って逆元を O(log N) で計算します．</remarks>
        public static ModInt Inverse(ModInt v) { return Pow(v, Mod - 2); }
    }
    #endregion
    #region Binomial Coefficient
    public class BinomialCoefficient
    {
        public ModInt[] fact, ifact;
        public BinomialCoefficient(int n)
        {
            fact = new ModInt[n + 1];
            ifact = new ModInt[n + 1];
            fact[0] = 1;
            for (int i = 1; i <= n; i++)
                fact[i] = fact[i - 1] * i;
            ifact[n] = ModInt.Inverse(fact[n]);
            for (int i = n - 1; i >= 0; i--)
                ifact[i] = ifact[i + 1] * (i + 1);
            ifact[0] = ifact[1];
        }
        public ModInt this[int n, int r]
        {
            get
            {
                if (n < 0 || n >= fact.Length || r < 0 || r > n) return 0;
                return fact[n] * ifact[n - r] * ifact[r];
            }
        }
        public ModInt RepeatedCombination(int n, int k)
        {
            if (k == 0) return 1;
            return this[n + k - 1, k];
        }
    }
    #endregion
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
