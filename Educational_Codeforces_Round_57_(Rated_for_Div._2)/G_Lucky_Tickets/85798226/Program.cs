using System;
using System.Collections.Generic;
using System.Diagnostics;
using CompLib.Mathematics;
using CompLib.Util;

public class Program
{
    private const int MaxLg = 20;


    private int N, K;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();

        ModInt[] a = new ModInt[1 << MaxLg];
        a[0] = 1;

        ModInt[] b = new ModInt[1 << MaxLg];
        int min = int.MaxValue;
        int max = int.MinValue;
        for (int i = 0; i < K; i++)
        {
            int d = sc.NextInt();
            min = Math.Min(min, d);
            max = Math.Max(max, d);
            b[d] = 1;
        }

        int lenA = 1;
        int lenB = max + 1;

        int h = N / 2;
        int tmp = h;
        while (tmp > 0)
        {
            if ((tmp & 1) > 0)
            {
                FFT.Convolusion(ref a, lenA, b, lenB);
                lenA = lenA + lenB - 1;
            }

            if (tmp > 1)
            {
                FFT.Convolusion(ref b, lenB);
                lenB = lenB + lenB - 1;
            }

            tmp /= 2;
        }

        ModInt ans = 0;
        for (int i = min * h; i <= max * h; i++)
        {
            ans += a[i] * a[i];
        }

        Console.WriteLine(ans);
    }


    public static void Main(string[] args) => new Program().Solve();
}

static class FFT
{
    private const int E = 23;
    private const int MaxLg = 20;
    private static readonly ModInt O = 31;

    private static readonly ModInt[] Z, InvZ;

    private static readonly List<(int f, int t)>[] Rev;
    private static readonly ModInt[] InvLen;

    static FFT()
    {
        Z = new ModInt[E + 1];
        InvZ = new ModInt[E + 1];
        Z[E] = O;
        InvZ[E] = ModInt.Inverse(O);

        for (int i = E - 1; i >= 0; i--)
        {
            Z[i] = Z[i + 1] * Z[i + 1];
            InvZ[i] = InvZ[i + 1] * InvZ[i + 1];
        }

        Rev = new List<(int f, int t)>[MaxLg + 1];
        for (int i = 0; i <= MaxLg; i++)
        {
            int len = 1 << i;
            Rev[i] = new List<(int f, int t)>();
            for (int j = 0; j < len; j++)
            {
                int r = BitsReverse(j, i);
                if (j < r) Rev[i].Add((j, r));
            }
        }

        InvLen = new ModInt[MaxLg + 1];
        for (int i = 0; i <= MaxLg; i++)
        {
            InvLen[i] = ModInt.Inverse(1 << i);
        }
    }

    public static void Transform(ModInt[] a, int lg, int len, bool inverse)
    {
        foreach (var pair in Rev[lg])
        {
            var t = a[pair.f];
            a[pair.f] = a[pair.t];
            a[pair.t] = t;
        }

        for (int i = 1; i <= lg; i++)
        {
            int b = 1 << (i - 1);
            ModInt z = 1;
            for (int j = 0; j < b; j++)
            {
                for (int k = j; k < len; k += (b << 1))
                {
                    var t = z * a[k + b];
                    var u = a[k];
                    a[k] = u + t;
                    a[k + b] = u - t;
                }

                z *= inverse ? InvZ[i] : Z[i];
            }
        }
    }

    public static void Convolusion(ref ModInt[] a, int lenA, ModInt[] b, int lenB)
    {
        int sz = lenA + lenB - 1;
        int len = 1;
        int lg = 0;
        while (len < sz)
        {
            len <<= 1;
            lg++;
        }

        Transform(a, lg, len, false);
        var tmpB = new ModInt[len];
        Array.Copy(b, tmpB, len);
        Transform(tmpB, lg, len, false);
        for (int i = 0; i < len; i++)
        {
            a[i] *= tmpB[i];
        }

        Transform(a, lg, len, true);
        for (int i = 0; i < sz; i++)
        {
            a[i] *= InvLen[lg];
        }
    }

    public static void Convolusion(ref ModInt[] a, int lenA)
    {
        int sz = lenA * 2 - 1;
        int len = 1;
        int lg = 0;
        while (len < sz)
        {
            len <<= 1;
            lg++;
        }

        Transform(a, lg, len, false);
        for (int i = 0; i < len; i++)
        {
            a[i] *= a[i];
        }

        Transform(a, lg, len, true);
        for (int i = 0; i < sz; i++)
        {
            a[i] *= InvLen[lg];
        }
    }

    private static int BitsReverse(int v, int lg)
    {
        if (v == 0) return v;
        int r = 0;
        if (lg % 2 == 1) r |= v & (1 << (lg / 2));
        for (int i = 0; i < lg / 2; i++)
        {
            r |= ((v >> i) & 1) << (lg - i - 1);
            r |= ((v >> (lg - i - 1)) & 1) << i;
        }

        return r;
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
                string s = Console.ReadLine();
                while (s.Length == 0)
                {
                    s = Console.ReadLine();
                }

                _line = s.Split(Separator);
                _index = 0;
            }

            return _line[_index++];
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