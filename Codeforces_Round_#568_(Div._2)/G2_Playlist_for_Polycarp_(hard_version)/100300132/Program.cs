using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Mathematics;

public class Program
{
    private int N, T;
    private int[] t, g;
    private ModInt[] F;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        T = sc.NextInt();
        t = new int[N];
        g = new int[N];
        int[] cnt = new int[3];
        for (int i = 0; i < N; i++)
        {
            t[i] = sc.NextInt();
            g[i] = sc.NextInt() - 1;
            cnt[g[i]]++;
        }

        var dp = new Array4D(T + 1, cnt[0] + 1, cnt[1] + 1, cnt[2] + 1);
        dp[0, 0, 0, 0] = 1;

        int[] c2 = new int[3];
        for (int i = 0; i < N; i++)
        {
            for (int j = T; j >= 0; j--)
            {
                for (int k = 0; k <= c2[0]; k++)
                {
                    for (int l = 0; l <= c2[1]; l++)
                    {
                        for (int m = 0; m <= c2[2]; m++)
                        {
                            if (j + t[i] > T) continue;
                            if (g[i] == 0 && k + 1 <= cnt[0])
                            {
                                dp[j + t[i], k + 1, l, m] += dp[j, k, l, m];
                            }

                            if (g[i] == 1 && l + 1 <= cnt[1])
                            {
                                dp[j + t[i], k, l + 1, m] += dp[j, k, l, m];
                            }

                            if (g[i] == 2 && m + 1 <= cnt[2])
                            {
                                dp[j + t[i], k, l, m + 1] += dp[j, k, l, m];
                            }
                        }
                    }
                }
            }

            c2[g[i]]++;
        }

        var dp2 = new Array4D(cnt[0] + 1, cnt[1] + 1, cnt[2] + 1, 3);
        if (cnt[0] > 0) dp2[1, 0, 0, 0] = 1;
        if (cnt[1] > 0) dp2[0, 1, 0, 1] = 1;
        if (cnt[2] > 0) dp2[0, 0, 1, 2] = 1;

        for (int i = 0; i <= cnt[0]; i++)
        {
            for (int j = 0; j <= cnt[1]; j++)
            {
                for (int k = 0; k <= cnt[2]; k++)
                {
                    for (int l = 0; l < 3; l++)
                    {
                        if (l != 0 && i + 1 <= cnt[0])
                        {
                            dp2[i + 1, j, k, 0] += dp2[i, j, k, l] * (i + 1);
                        }

                        if (l != 1 && j + 1 <= cnt[1])
                        {
                            dp2[i, j + 1, k, 1] += dp2[i, j, k, l] * (j + 1);
                        }

                        if (l != 2 && k + 1 <= cnt[2])
                        {
                            dp2[i, j, k + 1, 2] += dp2[i, j, k, l] * (k + 1);
                        }
                    }
                }
            }
        }

        ModInt ans = 0;
        for (int i = 0; i <= cnt[0]; i++)
        {
            for (int j = 0; j <= cnt[1]; j++)
            {
                for (int k = 0; k <= cnt[2]; k++)
                {
                    ans += dp[T, i, j, k] * (dp2[i, j, k, 0] + dp2[i, j, k, 1] + dp2[i, j, k, 2]);
                }
            }
        }

        Console.WriteLine(ans);
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

class Array4D
{
    public readonly int I, J, K, L;
    private ModInt[] T;

    public Array4D(int i, int j, int k, int l)
    {
        I = i;
        J = j;
        K = k;
        L = l;
        T = new ModInt[i * j * k * l];
    }

    public ModInt this[int i, int j, int k, int l]
    {
        set { T[i * J * K * L + j * K * L + k * L + l] = value; }
        get { return T[i * J * K * L + j * K * L + k * L + l]; }
    }
}

// https://bitbucket.org/camypaper/complib
namespace CompLib.Mathematics
{
    #region ModInt

    /// <summary>
    /// [0,<see cref="Mod"/>) ?????????????????????????????????
    /// </summary>
    public struct ModInt
    {
        /// <summary>
        /// ?????????????????????
        /// </summary>
        public const long Mod = (int) 1e9 + 7;
        // public const long Mod = 998244353;

        /// <summary>
        /// ??????????????????
        /// </summary>
        public long num;

        /// <summary>
        /// ?????? <paramref name="n"/> ?????????????????????????????????????????????????????????
        /// </summary>
        /// <param name="n">??????????????????????????????</param>
        /// <remarks>????????????????????????????????????????????????????????????????????????????????????????????????????????????<paramref name="n"/> ??? [0,<see cref="Mod"/>) ????????????????????? <paramref name="n"/> ????????????????????????????????????????????????????????? O(1) ????????????????????????</remarks>
        public ModInt(long n)
        {
            num = n;
        }

        /// <summary>
        /// ??????????????????????????????????????????????????????????????????
        /// </summary>
        /// <returns>[0,<see cref="Mod"/>) ???????????????????????? 10 ????????????????????????</returns>
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
        /// ??????????????? 2 ???????????????????????????????????????????????????
        /// </summary>
        /// <param name="v">???????????????</param>
        /// <param name="k">????????????</param>
        /// <returns>?????????????????????????????? O(N log N) ????????????????????????</returns>
        public static ModInt Pow(ModInt v, long k)
        {
            return Pow(v.num, k);
        }

        /// <summary>
        /// ??????????????? 2 ???????????????????????????????????????????????????
        /// </summary>
        /// <param name="v">???????????????</param>
        /// <param name="k">????????????</param>
        /// <returns>?????????????????????????????? O(N log N) ????????????????????????</returns>
        public static ModInt Pow(long v, long k)
        {
            long ret = 1;
            for (k %= Mod - 1; k > 0; k >>= 1, v = v * v % Mod)
                if ((k & 1) == 1)
                    ret = ret * v % Mod;
            return new ModInt(ret);
        }

        /// <summary>
        /// ????????????????????????????????????????????????
        /// </summary>
        /// <param name="v">?????????????????????????????????</param>
        /// <returns>???????????????????????????</returns>
        /// <remarks>????????????????????????????????????????????????????????????????????????????????????????????? O(log N) ?????????????????????</remarks>
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