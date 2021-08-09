using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using CompLib.Mathematics;
using CompLib.Collections;

public class Program
{
    int K;
    int[] B;
    int N, M;

    public void Solve()
    {
        var sc = new Scanner();
        K = sc.NextInt();
        B = sc.IntArray();
        N = sc.NextInt();
        M = sc.NextInt();

        if (M == 1)
        {
            Console.WriteLine("1");
            return;
        }

        long e;
        if (K == 1)
        {
            e = ModInt2.Pow(B[0], N - 1).num;
        }
        else
        {
            var a = new ModInt2[K];
            a[0] = 1;

            var tmp = new ModInt2[K];
            tmp[1] = 1;
            long y = N - 1;

            while (y > 0)
            {
                if (y % 2 == 1)
                {
                    var next = new ModInt2[2 * K - 1];
                    for (int i = 0; i < K; i++)
                    {
                        for (int j = 0; j < K; j++)
                        {
                            next[i + j] += (a[i] * tmp[j]);
                        }
                    }

                    for (int i = 2 * K - 2; i >= K; i--)
                    {
                        for (int j = 0; j < K; j++)
                        {
                            next[i - j - 1] += next[i] * B[j];
                        }
                    }

                    for (int i = 0; i < K; i++)
                    {
                        a[i] = next[i];
                    }
                }

                {
                    var next = new ModInt2[2 * K - 1];
                    for (int i = 0; i < K; i++)
                    {
                        for (int j = 0; j < K; j++)
                        {
                            next[i + j] += (tmp[i] * tmp[j]);
                        }
                    }

                    for (int i = 2 * K - 2; i >= K; i--)
                    {
                        for (int j = 0; j < K; j++)
                        {
                            next[i - j - 1] += next[i] * B[j];
                        }
                    }

                    for (int i = 0; i < K; i++)
                    {
                        tmp[i] = next[i];
                    }
                }
                y /= 2;
            }


            // Console.WriteLine(string.Join(" ", a));

            // x^e = M (mod p)
            e = a[K - 1].num;
        }
        // Console.WriteLine(e);
        // g^n = M (mod p)
        long n = ModLog(3, M, ModInt.Mod);
        // Console.WriteLine(n);

        // Console.WriteLine(ModInt.Pow(3, n));
        // e * f = n (mod p-1)

        // t.g = gcd (e, p-1)
        // t.x*e = t.g (mod p-1)
        var tuple = MathACL.InvGCD(e, ModInt.Mod - 1);

        if (n % tuple.g != 0)
        {
            Console.WriteLine("-1");
            return;
        }

        long f = tuple.x * (n / tuple.g);

        Console.WriteLine(ModInt.Pow(3, f));
    }

    long ModLog(long a, long b, long p)
    {
        long g = 1;
        for (long i = p; i > 0; i /= 2)
        {
            g *= a;
            g %= p;
        }

        g = MathEx.GCD(g, p);

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

        var bs = new HashMap<long, long>();
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

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}


namespace CompLib.Mathematics
{
    using System.Diagnostics;
    public static partial class MathACL
    {
        /// <summary>
        /// xのn乗をmで割った余り
        /// </summary>
        /// <param name="x"></param>
        /// <param name="n"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static long PowMod(long x, long n, int m)
        {
            Debug.Assert(0 <= n && 1 <= m);
            if (m == 1) return 0;
            x = SafeMod(x, m);
            long r = 1;
            while (n != 0)
            {
                if ((n & 1) > 0)
                {
                    r *= x;
                    r %= m;
                }
                x *= x;
                x %= m;
                n >>= 1;
            }
            return r;
        }

        /// <summary>
        /// xの逆数をmで割った余り
        /// </summary>
        /// <param name="x"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static long InvMod(long x, long m)
        {
            Debug.Assert(1 <= m);
            var z = InvGCD(x, m);
            Debug.Assert(z.g == 1);
            return z.x;
        }

        /// <summary>
        /// x = r[i] (mod m[i]), ∀i ∈ {0,1,...,n-1} なxを求め、(mod lcm(m))で返します
        /// </summary>
        /// <param name="r"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static (long rem, long mod) CRT(long[] r, long[] m)
        {
            Debug.Assert(r.Length == m.Length);
            int n = r.Length;
            long r0 = 0;
            long m0 = 1;
            for (int i = 0; i < n; i++)
            {
                Debug.Assert(1 <= m[i]);
                long r1 = SafeMod(r[i], m[i]);
                long m1 = m[i];

                if (m0 < m1)
                {
                    long t = r0;
                    r0 = r1;
                    r1 = t;

                    t = m0;
                    m0 = m1;
                    m1 = t;
                }

                if (m0 % m1 == 0)
                {
                    if (r0 % m1 != r1) return (0, 0);
                    continue;
                }


                (long g, long im) = InvGCD(m0, m1);

                long u1 = m1 / g;

                if ((r1 - r0) % g != 0) return (0, 0);

                long x = (r1 - r0) / g % u1 * im % u1;

                r0 += x * m0;
                m0 *= u1;
                if (r0 < 0) r0 += m0;
            }

            return (r0, m0);
        }

        /// <summary>
        /// Σ floor((a*i + b)/m)を返します
        /// </summary>
        /// <param name="n"></param>
        /// <param name="m"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static long FloorSum(long n, long m, long a, long b)
        {
            long ans = 0;
            if (a >= m)
            {
                ans += (n - 1) * n * (a / m) / 2;
                a %= m;
            }
            if (b >= m)
            {
                ans += n * (b / m);
                b %= m;
            }

            long yMax = (a * n + b) / m;
            long xMax = (yMax * m - b);
            if (yMax == 0) return ans;
            ans += (n - (xMax + a - 1) / a) * yMax;
            ans += FloorSum(yMax, a, m, (a - xMax % a) % a);
            return ans;
        }

        private static long SafeMod(long n, long m)
        {
            n %= m;
            if (n < 0) n += m;
            return n;
        }

        // g = gcd (a, b)
        // xa = g (mod b)
        public static (long g, long x) InvGCD(long a, long b)
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
    }
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

// https://bitbucket.org/camypaper/complib
namespace CompLib.Mathematics
{
    using System;
    using System.Collections.Generic;
    #region GCD LCM
    /// <summary>
    /// 様々な数学的関数の静的メソッドを提供します．
    /// </summary>
    public static partial class MathEx
    {
        /// <summary>
        /// 2 つの整数の最大公約数を求めます．
        /// </summary>
        /// <param name="n">最初の値</param>
        /// <param name="m">2 番目の値</param>
        /// <returns>2 つの整数の最大公約数</returns>
        /// <remarks>ユークリッドの互除法に基づき最悪計算量 O(log N) で実行されます．</remarks>
        public static int GCD(int n, int m) { return (int)GCD((long)n, m); }


        /// <summary>
        /// 2 つの整数の最大公約数を求めます．
        /// </summary>
        /// <param name="n">最初の値</param>
        /// <param name="m">2 番目の値</param>
        /// <returns>2 つの整数の最大公約数</returns>
        /// <remarks>ユークリッドの互除法に基づき最悪計算量 O(log N) で実行されます．</remarks>
        public static long GCD(long n, long m)
        {
            n = Math.Abs(n);
            m = Math.Abs(m);
            while (n != 0)
            {
                m %= n;
                if (m == 0) return n;
                n %= m;
            }
            return m;
        }


        /// <summary>
        /// 2 つの整数の最小公倍数を求めます．
        /// </summary>
        /// <param name="n">最初の値</param>
        /// <param name="m">2 番目の値</param>
        /// <returns>2 つの整数の最小公倍数</returns>
        /// <remarks>最悪計算量 O(log N) で実行されます．</remarks>
        public static long LCM(long n, long m) { return (n / GCD(n, m)) * m; }
    }
    #endregion
    #region PrimeSieve
    public static partial class MathEx
    {
        /// <summary>
        /// ある値までに素数表を構築します．
        /// </summary>
        /// <param name="max">最大の値</param>
        /// <param name="primes">素数のみを入れた数列が返される</param>
        /// <returns>0 から max までの素数表</returns>
        /// <remarks>エラトステネスの篩に基づき，最悪計算量 O(N loglog N) で実行されます．</remarks>
        public static bool[] Sieve(int max, List<int> primes = null)
        {
            var isPrime = new bool[max + 1];
            for (int i = 2; i < isPrime.Length; i++) isPrime[i] = true;
            for (int i = 2; i * i <= max; i++)
                if (!isPrime[i]) continue;
                else for (int j = i * i; j <= max; j += i) isPrime[j] = false;
            if (primes != null) for (int i = 0; i <= max; i++) if (isPrime[i]) primes.Add(i);

            return isPrime;
        }
    }
    #endregion
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
        // public const long Mod = (int)1e9 + 7;
        public const long Mod = 998244353;

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
}



// https://bitbucket.org/camypaper/complib
namespace CompLib.Mathematics
{
    #region ModInt
    /// <summary>
    /// [0,<see cref="Mod"/>) までの値を取るような数
    /// </summary>
    public struct ModInt2
    {
        /// <summary>
        /// 剰余を取る値．
        /// </summary>
        // public const long Mod = (int)1e9 + 7;
        public const long Mod = 998244353 - 1;

        /// <summary>
        /// 実際の数値．
        /// </summary>
        public long num;
        /// <summary>
        /// 値が <paramref name="n"/> であるようなインスタンスを構築します．
        /// </summary>
        /// <param name="n">インスタンスが持つ値</param>
        /// <remarks>パフォーマンスの問題上，コンストラクタ内では剰余を取りません．そのため，<paramref name="n"/> ∈ [0,<see cref="Mod"/>) を満たすような <paramref name="n"/> を渡してください．このコンストラクタは O(1) で実行されます．</remarks>
        public ModInt2(long n) { num = n; }
        /// <summary>
        /// このインスタンスの数値を文字列に変換します．
        /// </summary>
        /// <returns>[0,<see cref="Mod"/>) の範囲内の整数を 10 進表記したもの．</returns>
        public override string ToString() { return num.ToString(); }
        public static ModInt2 operator +(ModInt2 l, ModInt2 r) { l.num += r.num; if (l.num >= Mod) l.num -= Mod; return l; }
        public static ModInt2 operator -(ModInt2 l, ModInt2 r) { l.num -= r.num; if (l.num < 0) l.num += Mod; return l; }
        public static ModInt2 operator *(ModInt2 l, ModInt2 r) { return new ModInt2(l.num * r.num % Mod); }
        public static implicit operator ModInt2(long n) { n %= Mod; if (n < 0) n += Mod; return new ModInt2(n); }

        /// <summary>
        /// 与えられた 2 つの数値からべき剰余を計算します．
        /// </summary>
        /// <param name="v">べき乗の底</param>
        /// <param name="k">べき指数</param>
        /// <returns>繰り返し二乗法により O(N log N) で実行されます．</returns>
        public static ModInt2 Pow(ModInt2 v, long k) { return Pow(v.num, k); }

        /// <summary>
        /// 与えられた 2 つの数値からべき剰余を計算します．
        /// </summary>
        /// <param name="v">べき乗の底</param>
        /// <param name="k">べき指数</param>
        /// <returns>繰り返し二乗法により O(N log N) で実行されます．</returns>
        public static ModInt2 Pow(long v, long k)
        {
            long ret = 1;
            for (k %= Mod - 1; k > 0; k >>= 1, v = v * v % Mod)
                if ((k & 1) == 1) ret = ret * v % Mod;
            return new ModInt2(ret);
        }
        /// <summary>
        /// 与えられた数の逆元を計算します．
        /// </summary>
        /// <param name="v">逆元を取る対象となる数</param>
        /// <returns>逆元となるような値</returns>
        /// <remarks>法が素数であることを仮定して，フェルマーの小定理に従って逆元を O(log N) で計算します．</remarks>
        public static ModInt2 Inverse(ModInt2 v) { return Pow(v, Mod - 2); }
    }
    #endregion
    #region Binomial Coefficient
    public class BinomialCoefficient
    {
        public ModInt2[] fact, ifact;
        public BinomialCoefficient(int n)
        {
            fact = new ModInt2[n + 1];
            ifact = new ModInt2[n + 1];
            fact[0] = 1;
            for (int i = 1; i <= n; i++)
                fact[i] = fact[i - 1] * i;
            ifact[n] = ModInt2.Inverse(fact[n]);
            for (int i = n - 1; i >= 0; i--)
                ifact[i] = ifact[i + 1] * (i + 1);
            ifact[0] = ifact[1];
        }
        public ModInt2 this[int n, int r]
        {
            get
            {
                if (n < 0 || n >= fact.Length || r < 0 || r > n) return 0;
                return fact[n] * ifact[n - r] * ifact[r];
            }
        }
        public ModInt2 RepeatedCombination(int n, int k)
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
