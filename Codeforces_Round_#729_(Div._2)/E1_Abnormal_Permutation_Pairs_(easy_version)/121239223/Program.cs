using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using CompLib.Mathematics;
using CompLib.Algorithm;

public class Program
{
    int N;
    int Mod;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        ModInt.Mod = sc.NextInt();

        var m = new ModInt[N + 1][];
        m[1] = new ModInt[1] { 1 };
        for (int i = 2; i <= N; i++)
        {
            // 1の位置
            int max = (i - 1) * i / 2;
            m[i] = new ModInt[max + 1];

            for (int j = 0; j < i; j++)
            {
                for (int k = 0; k < m[i - 1].Length; k++)
                {
                    m[i][j + k] += m[i - 1][k];
                }
            }
        }

        var m2 = new ModInt[N + 1][];
        for (int i = 1; i <= N; i++)
        {
            m2[i] = new ModInt[m[i].Length + 1];
            for (int j = 0; j < m[i].Length; j++)
            {
                m2[i][j + 1] = m2[i][j] + m[i][j];
            }
        }


        ModInt ans = 0;

        for (int i = 0; i <= N - 2; i++)
        {
            // 先頭i個同じ
            // 選び方
            ModInt q = 1;
            for (int j = 0; j < i; j++) q *= N - j;

            ModInt w = 0;
            // i個目 
            for (int j = 0; j < N - i; j++)
            {
                for (int k = 0; k < j; k++)
                {
                    // 残り N-i-1個の分布 m[N-i-1]
                    // i+1個目の転倒数への寄与 j,k

                    for (int l = j - k; l < m[N - i - 1].Length; l++)
                    {
                        w += m2[N - i - 1][l - (j - k)] * m[N - i - 1][l];
                    }
                }
            }

            ans += q * w;
        }
        Console.WriteLine(ans);
    }
    /*
    1 0
    1
    2 0
    1 1
    3 0
    1 2 2 1
    4 17
    1 3 5 6 5 3 1
    5 904
    1 4 9 15 20 22 20 15 9 4 1
    6 45926
    1 5 14 29 49 71 90 101 101 90 71 49 29 14 5 1
    7 2725016
    1 6 20 49 98 169 259 359 455 531 573 573 531 455 359 259 169 98 49 20 6 1
    8 196884712
    1 7 27 76 174 343 602 961 1415 1940 2493 3017 3450 3736 3836 3736 3450 3017 2493 1940 1415 961 602 343 174 76 27 7 1 
    */

    void F(int n)
    {
        int[] p = new int[n];
        for (int i = 0; i < n; i++)
        {
            p[i] = i;
        }

        int[] cnt = new int[(n - 1) * n / 2 + 1];

        long ans = 0;
        do
        {
            int c = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (p[i] > p[j]) c++;
                }
            }

            for (int i = c + 1; i < cnt.Length; i++)
            {
                ans += cnt[i];
            }

            cnt[c]++;
        } while (Algorithm.NextPermutation(p));

        Console.WriteLine($"{n} {ans}");
        Console.WriteLine(string.Join(" ", cnt));

        // 5 9 14 20 27
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}



namespace CompLib.Algorithm
{
    using System;
    using System.Collections.Generic;
    public static partial class Algorithm
    {
        private static void Swap<T>(ref T a, ref T b)
        {
            T tmp = a;
            a = b;
            b = tmp;
        }

        private static void Reverse<T>(T[] array, int begin)
        {
            // [begin, array.Length)を反転
            if (array.Length - begin >= 2)
            {
                for (int i = begin, j = array.Length - 1; i < j; i++, j--)
                {
                    Swap(ref array[i], ref array[j]);
                }
            }
        }

        /// <summary>
        /// arrayを辞書順で次の順列にする 存在しないときはfalseを返す
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool NextPermutation<T>(T[] array, Comparison<T> comparison)
        {
            for (int i = array.Length - 2; i >= 0; i--)
            {
                if (comparison(array[i], array[i + 1]) < 0)
                {
                    int j = array.Length - 1;
                    for (; j > i; j--)
                    {
                        if (comparison(array[i], array[j]) < 0)
                        {
                            break;
                        }
                    }

                    Swap(ref array[i], ref array[j]);
                    Reverse(array, i + 1);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// arrayを辞書順で次の順列にする 存在しないときはfalseを返す
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool NextPermutation<T>(T[] array, Comparer<T> comparer) =>
            NextPermutation(array, comparer.Compare);

        /// <summary>
        /// arrayを辞書順で次の順列にする 存在しないときはfalseを返す
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool NextPermutation<T>(T[] array) => NextPermutation(array, Comparer<T>.Default);
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
        public static long Mod = (int)1e9 + 7;
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
