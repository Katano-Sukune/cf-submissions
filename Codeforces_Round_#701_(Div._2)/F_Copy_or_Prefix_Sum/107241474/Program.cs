using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Collections;
using CompLib.Mathematics;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
#if !DEBUG
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
#endif
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
        int[] b = sc.IntArray();

        var dp = new ModInt[n + 1, 2];
        dp[0, 0] = 1;

        var map = new HashMap<long, ModInt>();
        map[0] = 1;
        long sum = 0;

        // var t2 = new ModInt[n + 1];
        for (int i = 0; i < n; i++)
        {
            sum += b[i];
            // t2[i + 1] = map[sum];
            if (b[i] == 0)
            {
                // 0
                dp[i + 1, 0] += dp[i, 0];

                // 1
                // a_i = b_i
                // 総和が0にならない
                dp[i + 1, 1] += dp[i, 1];
               //  dp[i + 1, 0] += map[sum];
                dp[i + 1, 0] += dp[i, 1];
                map[sum] += dp[i + 1, 1];
            }
            else
            {
                // 0
                dp[i + 1, 1] += dp[i, 0];

                // 1
                // a_i = b_i
                // 総和が0にならない
                dp[i + 1, 1] += dp[i, 1] - map[sum];
                // 0
                dp[i + 1, 0] += map[sum];
                // Σa_j = b_i
                dp[i + 1, 1] += dp[i, 1];
                map[sum] += dp[i + 1, 1];
            }


            //  sum += b[i];
        }

        Console.WriteLine(dp[n,0] + dp[n,1]);
        // var p2 = new ModInt[n + 1];
        // var m2 = new ModInt[n + 1];
        // for (int i = 0; i <= n; i++)
        // {
        //     p2[i] = dp[i, 1];
        //     m2[i] = dp[i, 0];
        // }
        // 1 1 2 3

        // Console.WriteLine(string.Join(" ", p2));
        // Console.WriteLine(string.Join(" ", m2));
        // Console.WriteLine(string.Join(" ", t2));

        // var dp2 = new HashMap<int, ModInt>[n + 1];
        // for (int i = 0; i <= n; i++)
        // {
        //     dp2[i] = new HashMap<int, ModInt>();
        // }
        //
        // var t = new ModInt[n + 1];
        // dp2[0][0] = 1;
        // for (int i = 0; i < n; i++)
        // {
        //     foreach ((int j, ModInt v) in dp2[i])
        //     {
        //         if (j == 0)
        //         {
        //             dp2[i + 1][b[i]] += v;
        //         }
        //         else
        //         {
        //             dp2[i + 1][j + b[i]] += v;
        //             if (j + b[i] == 0)
        //             {
        //                 t[i + 1] += v;
        //             }
        //
        //             dp2[i + 1][b[i]] += v;
        //         }
        //     }
        // }
        //
        //
        // ModInt ans = 0;
        // foreach ((int j, ModInt v) in dp2[n])
        // {
        //     ans += v;
        // }
        //
        // var p = new ModInt[n + 1];
        // var m = new ModInt[n + 1];
        // for (int i = 0; i <= n; i++)
        // {
        //     foreach (var pair in dp2[i])
        //     {
        //         if (pair.Key == 0) m[i] += pair.Value;
        //         else p[i] += pair.Value;
        //     }
        // }
        //
        // Console.WriteLine(string.Join(" ", p));
        // Console.WriteLine(string.Join(" ", m));
        // Console.WriteLine(string.Join(" ", t));
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

// https://bitbucket.org/camypaper/complib
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
        public const long Mod = (int) 1e9 + 7;
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
        public ModInt(long n)
        {
            num = n;
        }

        /// <summary>
        /// このインスタンスの数値を文字列に変換します．
        /// </summary>
        /// <returns>[0,<see cref="Mod"/>) の範囲内の整数を 10 進表記したもの．</returns>
        public override string ToString()
        {
            return num.ToString();
        }

        public static ModInt operator +(ModInt l, ModInt r)
        {
            l.num += r.num;
            if (l.num >= Mod) l.num -= Mod;
            return l;
        }

        public static ModInt operator -(ModInt l, ModInt r)
        {
            l.num -= r.num;
            if (l.num < 0) l.num += Mod;
            return l;
        }

        public static ModInt operator *(ModInt l, ModInt r)
        {
            return new ModInt(l.num * r.num % Mod);
        }

        public static implicit operator ModInt(long n)
        {
            n %= Mod;
            if (n < 0) n += Mod;
            return new ModInt(n);
        }

        /// <summary>
        /// 与えられた 2 つの数値からべき剰余を計算します．
        /// </summary>
        /// <param name="v">べき乗の底</param>
        /// <param name="k">べき指数</param>
        /// <returns>繰り返し二乗法により O(N log N) で実行されます．</returns>
        public static ModInt Pow(ModInt v, long k)
        {
            return Pow(v.num, k);
        }

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
                if ((k & 1) == 1)
                    ret = ret * v % Mod;
            return new ModInt(ret);
        }

        /// <summary>
        /// 与えられた数の逆元を計算します．
        /// </summary>
        /// <param name="v">逆元を取る対象となる数</param>
        /// <returns>逆元となるような値</returns>
        /// <remarks>法が素数であることを仮定して，フェルマーの小定理に従って逆元を O(log N) で計算します．</remarks>
        public static ModInt Inverse(ModInt v)
        {
            return Pow(v, Mod - 2);
        }
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