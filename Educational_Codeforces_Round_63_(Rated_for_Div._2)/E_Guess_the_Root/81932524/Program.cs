using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Mathematics;
using CompLib.Util;

public class Program
{
    const int Mod = 1000003;
    const int MaxK = 10;

    readonly ModInt[] Test = new ModInt[11] { 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 };

    public void Solve()
    {
        // k <= 10
        // k次式 f(x)がある

        // 50回まで f(x_q)を聞く

        // f(x_0) = 0 (mod 1000003)なx_0を探す

        // 0聞く a_0確定

        ModInt[][] matrix = new ModInt[MaxK + 1][];
        for (int i = 0; i <= MaxK; i++)
        {
            matrix[i] = new ModInt[MaxK + 1];
        }
        ModInt[] ans = new ModInt[MaxK + 1];

        for (int i = 0; i <= MaxK; i++)
        {
            ans[i] = Query(i + 1);
            ModInt tmp = 1;
            for (int j = 0; j <= MaxK; j++)
            {
                matrix[i][j] = tmp;
                tmp *= i + 1;
            }
        }

        for (int i = 0; i < MaxK; i++)
        {
            var inv = ModInt.Inverse(matrix[i][i]);
            for (int j = i; j <= MaxK; j++)
            {
                matrix[i][j] *= inv;
            }
            ans[i] *= inv;

            for (int j = i + 1; j <= MaxK; j++)
            {
                var tmp = matrix[j][i];
                for (int k = i; k <= MaxK; k++)
                {
                    matrix[j][k] -= matrix[i][k] * tmp;
                }
                ans[j] -= ans[i] * tmp;
            }
        }

        var a = new ModInt[MaxK + 1];
        for (int i = MaxK; i >= 0; i--)
        {
            var inv = ModInt.Inverse(matrix[i][i]);
            matrix[i][i] *= inv;
            ans[i] *= inv;
            a[i] = ans[i];

            for (int j = i - 1; j >= 0; j--)
            {
                var tmp = matrix[j][i];
                matrix[j][i] -= matrix[i][i] * tmp;
                ans[j] -= ans[i] * tmp;
            }
        }

        for (int x = 0; x < ModInt.Mod; x++)
        {
            if (Calc(a, x).num == 0)
            {
                Console.WriteLine($"! {x}");
                return;
            }
        }
        Console.WriteLine("! -1");
    }

    ModInt Calc(ModInt[] a, ModInt x)
    {
        ModInt result = 0;
        ModInt tmp = 1;
        for (int i = 0; i <= MaxK; i++)
        {
            result += a[i] * tmp;
            tmp *= x;
        }
        return result;
    }


    ModInt Query(int n)
    {
#if DEBUG
        return Calc(Test, n);
#else
        Console.WriteLine($"? {n}");
        return int.Parse(Console.ReadLine());
#endif
    }

    public static void Main(string[] args) => new Program().Solve();
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
        public const long Mod = 1000003;

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
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(Separator);
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
            _line = Console.ReadLine().Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}
