using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Mathematics;

public class Program
{
    private int K;
    private int[] B;
    private int N, M;

    public void Solve()
    {
        var sc = new Scanner();
        K = sc.NextInt();
        B = sc.IntArray();
        N = sc.NextInt();
        M = sc.NextInt();

        long num;
        if (K == 1)
        {
            // b_0 ^ (n-1)
            num = ModInt.Pow(B[0], N - 1).num;
        }
        else
        {
            var a = new ModInt[K];
            a[0] = 1;
            var b = new ModInt[K];
            b[1] = 1;
            int t = N - 1;
            while (t > 0)
            {
                if (t % 2 == 1)
                {
                    var tmp = new ModInt[2 * K - 1];
                    for (int i = 0; i < K; i++)
                    {
                        for (int j = 0; j < K; j++)
                        {
                            tmp[i + j] += a[i] * b[j];
                        }
                    }

                    for (int i = 2 * K - 2; i >= K; i--)
                    {
                        for (int j = 1; j <= K; j++)
                        {
                            tmp[i - j] += tmp[i] * B[j - 1];
                        }
                    }

                    for (int i = 0; i < K; i++)
                    {
                        a[i] = tmp[i];
                    }
                }

                {
                    var tmp = new ModInt[2 * K - 1];
                    for (int i = 0; i < K; i++)
                    {
                        for (int j = 0; j < K; j++)
                        {
                            tmp[i + j] += b[i] * b[j];
                        }
                    }

                    for (int i = 2 * K - 2; i >= K; i--)
                    {
                        for (int j = 1; j <= K; j++)
                        {
                            tmp[i - j] += tmp[i] * B[j - 1];
                        }
                    }

                    for (int i = 0; i < K; i++)
                    {
                        b[i] = tmp[i];
                    }
                }
                t /= 2;
            }


            // Console.WriteLine(a[K - 1]);

            num = a[K - 1].num;
        }

        const long mod = 998244353;
        // x^num = m
        // 3^y = m
        long y = NumberTheory.ModLog(3, M, mod);

        // 3^(pow*num) = m
        long pow = NumberTheory.ModDiv(y, num, mod - 1);
        if (pow == -1)
        {
            Console.WriteLine("-1");
            return;
        }

        // 3^pow
        long ans = 1;
        long tmp2 = 3;
        while (pow > 0)
        {
            if (pow % 2 == 1)
            {
                ans *= tmp2;
                ans %= mod;
            }

            tmp2 *= tmp2;
            tmp2 %= mod;
            pow /= 2;
        }

        Console.WriteLine(ans);
        // Console.WriteLine((long)998244351 * 998244351% mod);
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

namespace CompLib.Mathematics
{
    using System.Collections.Generic;
    using System;

    public static class NumberTheory
    {
        /// <summary>
        /// a,bの最大公約数を求める
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static long GCD(long a, long b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);
            while (a != 0)
            {
                b %= a;
                if (b == 0) return a;
                a %= b;
            }

            return b;
        }

        private static long SafeMod(long n, long m)
        {
            n %= m;
            if (n < 0) n += m;
            return n;
        }

        // g = gcd (a, b)
        // xa = g (mod b)
        private static (long g, long x) InvGCD(long a, long b)
        {
            a = SafeMod(a, b);
            if (a == 0) return (b, 0);
            long s = b;
            long t = a;
            long m0 = 0;
            long m1 = 1;

            while (t > 0)
            {
                long u = s / t;
                s -= t * u;
                m0 -= m1 * u;

                long tmp = s;
                s = t;
                t = tmp;

                tmp = m0;
                m0 = m1;
                m1 = tmp;
            }

            if (m0 < 0) m0 += b / s;
            return (s, m0);
        }

        /// <summary>
        /// a^x = b (mod p) を満たすxを求める 見つからなかった場合-1を返す
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="mod"></param>
        /// <returns></returns>
        public static long ModLog(long a, long b, long p)
        {
            long g = 1;
            for (long i = p; i > 0; i /= 2)
            {
                g *= a;
                g %= p;
            }

            g = GCD(g, p);

            long t = 1;
            long c = 0;
            for (; t % g > 0; c++)
            {
                if (t == b) return c;
                t *= a;
                t %= p;
            }

            if (b % g > 0) return -1;

            long n = p / g;
            long h = 0;
            long gs = 1;
            for (; h * h < n; h++)
            {
                gs *= a;
                gs %= n;
            }

            var bs = new Dictionary<long, long>();
            for (long s = 0, e = b; s < h; bs[e] = ++s)
            {
                e *= a;
                e %= n;
            }

            for (long s = 0, e = t; s < n;)
            {
                e *= gs;
                e %= n;
                s += h;
                if (bs.ContainsKey(e)) return c + s - bs[e];
            }

            return -1;
        }

        /// <summary>
        /// a/b (mod mod)を求める
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static long ModDiv(long a, long b, long mod)
        {
            var tuple = InvGCD(b, mod);
            if (a % tuple.g != 0) return -1;
            return (tuple.x * (a / tuple.g)) % mod;
        }
    }
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
        // public const long Mod = (int) 1e9 + 7;
        public const long Mod = 998244352;

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