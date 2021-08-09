using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Mathematics;

public class Program
{
    private int M;
    private ModInt InvM;
    private ModInt[] Memo;
    private bool[] Flag;

    private List<int>[] Divs;
    private int[][] Cnt;

    public void Solve()
    {
        var sc = new Scanner();
        M = sc.NextInt();
        InvM = ModInt.Inverse(M);
        Memo = new ModInt[M + 1];
        Flag = new bool[M + 1];
        /*
         * 空配列a, M
         *
         * 0 -> 生き残ってる素因数 -> 1までの回数期待値
         *
         * 生き残ってる素因数
         * 
         */

        /*
         * 1 1/4 0回 
         * 2 1/2 2回
         * 3 1/4 4/3回
         *
         * 1/4, 1/2, 1/4
         */

        // 1/4 + 3/2 + 7/12

        Divs = new List<int>[M + 1];
        for (int i = 1; i <= M; i++)
        {
            Divs[i] = new List<int>();
        }

        for (int i = 1; i <= M; i++)
        {
            for (int j = i; j <= M; j += i)
            {
                Divs[j].Add(i);
            }
        }

        Cnt = new int[M + 1][];
        for (int num = 1; num <= M; num++)
        {
            Cnt[num] = new int[Divs[num].Count];
            for (int i = Divs[num].Count - 1; i >= 0; i--)
            {
                Cnt[num][i] = M / Divs[num][i];
                for (int j = i + 1; j < Divs[num].Count; j++)
                {
                    if (Divs[num][j] % Divs[num][i] == 0) Cnt[num][i] -= Cnt[num][j];
                }
            }
        }

        Console.WriteLine(Go(0));
    }

    ModInt Go(int cur)
    {
        if (Flag[cur]) return Memo[cur];
        ref ModInt ans = ref Memo[cur];
        if (cur == 1)
        {
            ans = 0;
        }
        else if (cur == 0)
        {
            ans = 0;
            for (int i = 1; i <= M; i++)
            {
                ans += (Go(i) + 1) * InvM;
            }
        }
        else
        {
            ans = 0;
            // 小さい値に行くまでの回数期待値
            ModInt tmp = ModInt.Inverse(M - Cnt[cur][Divs[cur].Count - 1]);
            for (int i = 0; i < Divs[cur].Count - 1; i++)
            {
                ans += (Go(Divs[cur][i]) + M * tmp) * Cnt[cur][i] * tmp;
            }
        }

        Flag[cur] = true;
        return ans;
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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

namespace CompLib.Mathematics
{
    public static partial class Combinatorics
    {
        private const int Max = 5000000;
        private static ModInt[] F;

        static Combinatorics()
        {
            F = new ModInt[Max + 1];
            F[0] = 1;
            for (int i = 1; i <= Max; i++)
            {
                F[i] = F[i - 1] * i;
            }
        }

        /// <summary>
        /// 階乗 n!
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static ModInt Factorial(int n)
        {
            return F[n];
        }

        /// <summary>
        /// 順列
        /// </summary>
        /// <remarks>
        /// n個からm個選んで並べる
        /// </remarks>
        /// <param name="n"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static ModInt P(int n, int m)
        {
            return F[n] * ModInt.Inverse(F[n - m]);
        }

        /// <summary>
        /// 組み合わせ
        /// </summary>
        /// <remarks>
        /// n個からm個取り出す
        /// </remarks>
        /// <param name="n"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static ModInt C(int n, int m)
        {
            return F[n] * ModInt.Inverse(F[n - m] * F[m]);
        }

        /// <summary>
        /// 重複組み合わせ
        /// </summary>
        /// <remarks>
        /// n種類のものから重複を許して r個選ぶ
        /// </remarks>
        /// <param name="n"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static ModInt H(int n, int r)
        {
            return C(n + r - 1, r);
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
        public static int GCD(int n, int m)
        {
            return (int) GCD((long) n, m);
        }


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
        public static long LCM(long n, long m)
        {
            return (n / GCD(n, m)) * m;
        }
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
                else
                    for (int j = i * i; j <= max; j += i)
                        isPrime[j] = false;
            if (primes != null)
                for (int i = 0; i <= max; i++)
                    if (isPrime[i])
                        primes.Add(i);

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