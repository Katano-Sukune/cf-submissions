using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using CompLib.Mathematics;
using Mint1 = CompLib.Mathematics.DynamicModInt<CompLib.Mathematics.Mod998244352>;
using Mint2 = CompLib.Mathematics.DynamicModInt<CompLib.Mathematics.Mod998244353>;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int k = sc.NextInt();
        int[] b = sc.IntArray();
        int n = sc.NextInt();
        int m = sc.NextInt();

        long e;
        if (k == 1)
        {
            e = Mint1.Pow(b[^1], n - 1).num;
        }
        else
        {
            var res = new Mint1[k];
            res[0] = 1;

            var a = new Mint1[k];
            a[1] = 1;

            long y = n - 1;
            while (y > 0)
            {
                if (y % 2 == 1)
                {
                    var tmp = new Mint1[2 * k - 1];
                    for (int i = 0; i < k; i++)
                    {
                        for (int j = 0; j < k; j++)
                        {
                            tmp[i + j] += res[i] * a[j];
                        }
                    }

                    for (int i = 2 * k - 2; i >= k; i--)
                    {
                        for (int j = 0; j < k; j++)
                        {
                            tmp[i - j - 1] += tmp[i] * b[j];
                        }
                    }

                    for (int i = 0; i < k; i++)
                    {
                        res[i] = tmp[i];
                    }
                }

                {
                    var tmp = new Mint1[2 * k - 1];
                    for (int i = 0; i < k; i++)
                    {
                        for (int j = 0; j < k; j++)
                        {
                            tmp[i + j] += a[i] * a[j];
                        }
                    }

                    for (int i = 2 * k - 2; i >= k; i--)
                    {
                        for (int j = 0; j < k; j++)
                        {
                            tmp[i - j - 1] += tmp[i] * b[j];
                        }
                    }

                    for (int i = 0; i < k; i++)
                    {
                        a[i] = tmp[i];
                    }
                }
                y /= 2;
            }
            e = res[^1].num;
        }

        var mul = NumberTheory.ModLog(3, m, Mint2.Mod);
        var f = NumberTheory.ModDiv(mul, e, Mint1.Mod);
        if (f == -1)
        {
            Console.WriteLine("-1");
            return;
        }

        Console.WriteLine(Mint2.Pow(3, f));
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}


namespace CompLib.Mathematics
{
    public struct DynamicModInt<T> where T : struct, M
    {
        public long num;
        public static long Mod => default(T).Mod;
        public DynamicModInt(long n)
        {
            num = n;
        }

        public override string ToString() { return num.ToString(); }
        public static DynamicModInt<T> operator +(DynamicModInt<T> l, DynamicModInt<T> r) { l.num += r.num; if (l.num >= Mod) l.num -= Mod; return l; }
        public static DynamicModInt<T> operator -(DynamicModInt<T> l, DynamicModInt<T> r) { l.num -= r.num; if (l.num < 0) l.num += Mod; return l; }
        public static DynamicModInt<T> operator *(DynamicModInt<T> l, DynamicModInt<T> r) { return new DynamicModInt<T>(l.num * r.num % Mod); }
        public static implicit operator DynamicModInt<T>(long n) { n %= Mod; if (n < 0) n += Mod; return new DynamicModInt<T>(n); }
        /// <summary>
        /// 与えられた 2 つの数値からべき剰余を計算します．
        /// </summary>
        /// <param name="v">べき乗の底</param>
        /// <param name="k">べき指数</param>
        /// <returns>繰り返し二乗法により O(N log N) で実行されます．</returns>
        public static DynamicModInt<T> Pow(DynamicModInt<T> v, long k) { return Pow(v.num, k); }

        /// <summary>
        /// 与えられた 2 つの数値からべき剰余を計算します．
        /// </summary>
        /// <param name="v">べき乗の底</param>
        /// <param name="k">べき指数</param>
        /// <returns>繰り返し二乗法により O(N log N) で実行されます．</returns>
        public static DynamicModInt<T> Pow(long v, long k)
        {
            long ret = 1;
            for (k %= Mod - 1; k > 0; k >>= 1, v = v * v % Mod)
                if ((k & 1) == 1) ret = ret * v % Mod;
            return new DynamicModInt<T>(ret);
        }
        /// <summary>
        /// 与えられた数の逆元を計算します．
        /// </summary>
        /// <param name="v">逆元を取る対象となる数</param>
        /// <returns>逆元となるような値</returns>
        /// <remarks>法が素数であることを仮定して，フェルマーの小定理に従って逆元を O(log N) で計算します．</remarks>
        public static DynamicModInt<T> Inverse(DynamicModInt<T> v) { return Pow(v, Mod - 2); }


    }

    public interface M
    {
        /// <summary>
        /// Mod
        /// </summary>
        public long Mod { get; }
        /// <summary>
        /// 原始根
        /// </summary>
        public long G { get; }
    }

    public struct Mod1000000007 : M
    {
        public long G => -1;
        public long Mod => 1000000007;
    }

    public struct Mod998244353 : M
    {
        public long G => 3;
        public long Mod => 998244353;
    }

    public struct Mod998244352 : M
    {
        public long G => -1;
        public long Mod => 998244352;
    }
}


namespace CompLib.Mathematics
{
    using System.Collections.Generic;
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
