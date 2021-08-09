using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using CompLib.Mathematics;

public class Program
{
    BinomialCoefficient C;
    public void Solve()
    {
        C = new BinomialCoefficient(100000);
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

    int N, K;

    int[] U, V;
    List<int>[] E;
    void Q(Scanner sc)
    {
        N = sc.NextInt();
        K = sc.NextInt();
        E = new List<int>[N];
        for (int i = 0; i < N; i++) E[i] = new List<int>();
        U = new int[N - 1];
        V = new int[N - 1];
        for (int i = 0; i < N - 1; i++)
        {
            U[i] = sc.NextInt() - 1;
            V[i] = sc.NextInt() - 1;
            E[U[i]].Add(V[i]);
            E[V[i]].Add(U[i]);
        }

        ModInt ans = 0;
        for (int s = 0; s < N; s++)
        {
            var ls = new List<int>[N];
            for (int i = 0; i < N; i++) ls[i] = new List<int>();
            foreach (int t in E[s])
            {
                var dist = Dist(t, s);
                var ar = new int[N];
                for (int i = 0; i < N; i++)
                {
                    if (dist[i] == int.MaxValue) continue;
                    if (dist[i] == -1) continue;
                    ar[dist[i]] += 1;
                }

                for (int i = 0; i < N; i++)
                {
                    if (ar[i] > 0) ls[i].Add(ar[i]);
                }
            }


            for (int l = 0; l < N; l++)
            {
                if (ls[l].Count < K) continue;

                var dp = new ModInt[ls[l].Count + 1, K + 1];
                dp[0, 0] = 1;
                for (int i = 0; i < ls[l].Count; i++)
                {
                    for (int j = 0; j <= K; j++)
                    {
                        dp[i + 1, j] += dp[i, j];
                        if (j + 1 <= K) dp[i + 1, j + 1] += dp[i, j] * ls[l][i];
                    }
                }
                ans += dp[ls[l].Count,K];
            }
        }


        if (K == 2)
        {
            for (int e = 0; e < N - 1; e++)
            {
                var distU = Dist(U[e], V[e]);
                var distV = Dist(V[e], U[e]);

                int[] cntU = new int[N];
                int[] cntV = new int[N];


                for (int i = 0; i < N; i++)
                {
                    if (distU[i] != int.MaxValue && distU[i] != -1) cntU[distU[i]] += 1;
                    if (distV[i] != int.MaxValue && distV[i] != -1) cntV[distV[i]] += 1;
                }



                for (int i = 0; i < N; i++)
                {
                    if (cntU[i] >= 1 && cntV[i] >= 1) ans += (ModInt)cntU[i] * cntV[i];
                }
            }
        }

        Console.WriteLine(ans);
    }

    int[] Dist(int u, int v)
    {
        int[] ans = new int[N];
        Array.Fill(ans, int.MaxValue);
        ans[u] = 0;
        ans[v] = -1;
        var q = new Queue<int>();
        q.Enqueue(u);
        while (q.Count > 0)
        {
            int d = q.Dequeue();
            foreach (int to in E[d])
            {
                if (ans[to] == int.MaxValue)
                {
                    ans[to] = ans[d] + 1;
                    q.Enqueue(to);
                }
            }
        }
        return ans;
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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