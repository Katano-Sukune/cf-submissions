using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Mathematics;

public class Program
{
    private long N;
    private int F1, F2, F3, C;
    private const long Mod = (long) 1e9 + 7;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextLong();
        F1 = sc.NextInt();
        F2 = sc.NextInt();
        F3 = sc.NextInt();
        C = sc.NextInt();

        ModInt[] a = new ModInt[5];
        a[0] = 1;
        ModInt[] b = new ModInt[5];
        b[1] = 1;
        long tmp = N - 1;
        while (tmp > 0)
        {
            if (tmp % 2 == 1)
            {
                ModInt[] c = new ModInt[5];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        c[i + j] += a[i] * b[j];
                    }
                }

                for (int i = 4; i >= 3; i--)
                {
                    c[i - 1] += c[i];
                    c[i - 2] += c[i];
                    c[i - 3] += c[i];
                    c[i] = 0;
                }

                a = c;
            }

            {
                ModInt[] c = new ModInt[5];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        c[i + j] += b[i] * b[j];
                    }
                }

                for (int i = 4; i >= 3; i--)
                {
                    c[i - 1] += c[i];
                    c[i - 2] += c[i];
                    c[i - 3] += c[i];
                    c[i] = 0;
                }

                b = c;
            }

            tmp /= 2;
        }

        // C
        var m1 = new Matrix(1, 5);
        m1[0, 3] = 2;
        m1[0, 4] = 2;

        var m2 = new Matrix(5, 5);
        m2[1, 0] = 1;

        m2[2, 1] = 1;

        m2[0, 2] = 1;
        m2[1, 2] = 1;
        m2[2, 2] = 1;
        m2[3, 2] = 1;

        m2[3, 3] = 1;
        m2[4, 3] = 1;

        m2[4, 4] = 1;

        var m3 = Matrix.Pow(m2, N - 3);

        var m4 = m1 * m3;


        long ans = 1;
        ans *= Pow(F1, a[0].num);
        ans %= Mod;

        ans *= Pow(F2, a[1].num);
        ans %= Mod;

        ans *= Pow(F3, a[2].num);
        ans %= Mod;

        ans *= Pow(C, m4[0, 2].num);
        ans %= Mod;

        Console.WriteLine(ans);

        // Console.WriteLine(m4[0, 0]);
        // Console.WriteLine(m4[0, 1]);
        // Console.WriteLine(m4[0, 2]);
        // Console.WriteLine(m4[0, 3]);
        // Console.WriteLine(m4[0, 4]);
        //
        // long[] f = new long[N + 1];
        // f[1] = F1;
        // f[2] = F2;
        // f[3] = F3;
        // for (int i = 4; i <= N; i++)
        // {
        //     f[i] = 1;
        //     for (int j = 0; j < 2 * i - 6; j++)
        //     {
        //         f[i] *= C;
        //         f[i] %= Mod;
        //     }
        //
        //     for (int j = 1; j <= 3; j++)
        //     {
        //         f[i] *= f[i - j];
        //         f[i] %= Mod;
        //     }
        // }
        //
        // Console.WriteLine(f[N]);
    }

    long Pow(long a, long b)
    {
        long res = 1;
        while (b > 0)
        {
            if (b % 2 == 1)
            {
                res *= a;
                res %= Mod;
            }

            a *= a;
            a %= Mod;
            b /= 2;
        }

        return res;
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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
        public const long Mod = (int) 1e9 + 6;
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


// https://bitbucket.org/camypaper/complib
namespace CompLib.Mathematics
{
    using System.Diagnostics;
    using N = ModInt;

    #region Matrix

    public class Matrix
    {
        int row, col;
        public N[] mat;

        /// <summary>
        /// <paramref name="r"/> ??? <paramref name="c"/> ??????????????????????????????????????????????????????
        /// </summary>
        /// <param name="r">????????????</param>
        /// <param name="c">????????????</param>
        public N this[int r, int c]
        {
            get { return mat[r * col + c]; }
            set { mat[r * col + c] = value; }
        }

        public Matrix(int r, int c)
        {
            row = r;
            col = c;
            mat = new N[row * col];
        }

        public static Matrix operator *(Matrix l, Matrix r)
        {
            Debug.Assert(l.col == r.row);
            var ret = new Matrix(l.row, r.col);
            for (int i = 0; i < l.row; i++)
            for (int k = 0; k < l.col; k++)
            for (int j = 0; j < r.col; j++)
                ret.mat[i * r.col + j] = (ret.mat[i * r.col + j] + l.mat[i * l.col + k] * r.mat[k * r.col + j]);
            return ret;
        }

        /// <summary>
        /// <paramref name="m"/>^<paramref name="n"/> ??? O(<paramref name="m"/>^3 log <paramref name="n"/>) ?????????????????????
        /// </summary>
        public static Matrix Pow(Matrix m, long n)
        {
            var ret = new Matrix(m.row, m.col);
            for (int i = 0; i < m.row; i++)
                ret.mat[i * m.col + i] = 1;
            for (; n > 0; m *= m, n >>= 1)
                if ((n & 1) == 1)
                    ret = ret * m;
            return ret;
        }

        public N[][] Items
        {
            get
            {
                var a = new N[row][];
                for (int i = 0; i < row; i++)
                {
                    a[i] = new N[col];
                    for (int j = 0; j < col; j++)
                        a[i][j] = mat[i * col + j];
                }

                return a;
            }
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