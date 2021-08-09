using System;
using System.Linq;
using CompLib.Util;

using CompLib.Mathematics;
using CompLib.Algorithm;
using System.Collections.Generic;

public class Program
{
    int N, X, Y;
    int[] A;
    int Q;
    int[] L;

    List<int>[] Div;

    const int MaxL = 500000;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        X = sc.NextInt();
        Y = sc.NextInt();
        A = sc.IntArray();
        Q = sc.NextInt();
        L = sc.IntArray();

        Div = new List<int>[MaxL + 1];
        for (int i = 1; i <= MaxL; i++)
        {
            Div[i] = new List<int>();
        }

        for (int i = 1; i <= MaxL; i++)
        {
            for (int j = i; j <= MaxL; j += i)
            {
                Div[j].Add(i);
            }
        }

        /*
         * 水平セグメント 2つ (0,0)-(x,0), (0,y)-(x,y)
         *
         * 垂直セグメントn+1個
         * 
         * (a_i,0)-(a_i,y)
         * 
         * ラップ 線分に沿って進み、1周
         * 交差しない
         * 
         * qステージ
         * 長さ l_i ラップの長さがl_iの約数になるようなラップ
         * 長さ最大
         * 
         * 各ステージについてラップの長さ
         */

        /*
         * 垂直2回 2Y
         * 
         * 距離がdのセグメント2つが存在するか?
         */

        /*
         * a-b+Xがいくつあるか
         */

        int sz = 2 * X + 1;
        int len = 1;
        int lg = 0;
        while (len < sz)
        {
            lg++;
            len <<= 1;
        }

        var s = new ModInt[len];
        var t = new ModInt[len];
        for (int i = 0; i <= N; i++)
        {
            s[A[i]] = 1;
            t[X - A[i]] = 1;
        }

        FastFourierTransform.Convolution(ref s, t, len, lg);

        var ans = new int[Q];
        for (int i = 0; i < Q; i++)
        {
            ans[i] = -1;
            // たてY
            // よこtX

            // 2n(Y + tX) = L

            int l = L[i] / 2;
            if (l <= Y) continue;
            for (int j = Div[l].Count - 1; j >= 0; j--)
            {
                int d = Div[l][j];
                int tx = d - Y;
                if (0 < tx && tx + X < s.Length && s[tx + X].num != 0)
                {
                    ans[i] = 2 * d;
                    break;
                }
            }
        }

        Console.WriteLine(string.Join(" ", ans));
    }

    public static void Main(string[] args) => new Program().Solve();
}

namespace CompLib.Algorithm
{
    using System;
    using System.Numerics;

    public static class FastFourierTransform
    {
        private const int E = 23;
        public static readonly ModInt O = new ModInt(31);

        // private const int E = 20;
        // public static readonly ModInt O = new ModInt(646);
        // public static readonly ModInt InvO = new ModInt(208611436);

        // Z[i] 1の原始2^i乗根
        public static readonly ModInt[] Z;
        public static readonly ModInt[] InvZ;

        static FastFourierTransform()
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
        }

        /// <summary>
        /// a,bからc[k]=Σ(a[i]*b[k-i])を求め、aに代入
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static void Convolution(ref ModInt[] a, ModInt[] b, int len, int lg)
        {

            Transform(a, lg, len, false);
            Transform(b, lg, len, false);
            for (int i = 0; i < len; i++)
            {
                a[i] *= b[i];
            }

            Transform(a, lg, len, true);
            var invLen = ModInt.Inverse(len);
            for (int i = 0; i < len; i++)
            {
                a[i] *= invLen;
            }

            // Array.Resize(ref a, sz);
        }

        private static void Transform(ModInt[] a, int lg, int len, bool inverse)
        {
            for (int i = 0; i < len; i++)
            {
                int rev = BitsReverse(i, lg);
                if (i < rev)
                {
                    var tmp = a[i];
                    a[i] = a[rev];
                    a[rev] = tmp;
                }
            }

            for (int i = 1; i <= lg; i++)
            {
                int b = 1 << (i - 1);
                ModInt z = 1;
                for (int j = 0; j < b; j++)
                {
                    for (int k = j; k < len; k += (b << 1))
                    {
                        ModInt t = z * a[k + b];
                        ModInt u = a[k];
                        a[k] = u + t;
                        a[k + b] = u - t;
                    }

                    z *= inverse ? InvZ[i] : Z[i];
                }
            }
        }

        /// <summary>
        /// lg桁のbit列vのbitを逆順にする
        /// </summary>
        /// <param name="v"></param>
        /// <param name="lg"></param>
        /// <returns></returns>
        public static int BitsReverse(int v, int lg)
        {
            int result = 0;
            for (int i = 0; i < lg; i++)
            {
                if (((1 << i) & v) > 0)
                {
                    result |= (1 << (lg - 1 - i));
                }
            }

            return result;
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
