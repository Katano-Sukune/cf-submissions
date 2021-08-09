using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using CompLib.Mathematics;

public class Program
{
    int N, K;
    int[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        A = sc.IntArray();

        int cnt1 = A.Count(num => num == 1);

        int f = 0;
        for (int i = 0; i < cnt1; i++)
        {
            if (A[N - i - 1] == 1) f++;
        }
        // 後ろcnt1個が全部1ならok
        long all = N * (N - 1) / 2;
        var inv = ModInt.Inverse(all);

        // 後ろcnt1個に1がi個ある
        var a = new Matrix(1, cnt1 + 1);
        a[0, f] = 1;

        var b = new Matrix(cnt1 + 1, cnt1 + 1);



        for (int j = 0; j <= cnt1; j++)
        {
            // 後ろcnt1個選ぶ
            ModInt t = all;

            if (j - 1 >= 0)
            {
                // 後ろcnt1個から1
                // 前 N-cnt1 から0選ぶ
                b[j, j - 1] += (j * ((N - cnt1) - (cnt1 - j))) * inv;
                t -= j * ((N - cnt1) - (cnt1 - j));
            }

            if (j + 1 <= cnt1)
            {
                b[j, j + 1] += ((cnt1 - j) * (cnt1 - j)) * inv;
                t -= (cnt1 - j) * (cnt1 - j);
            }
            b[j, j] += t * inv;
        }


        //for (int i = 0; i <= cnt1; i++)
        //{
        //    for (int j = 0; j <= cnt1; j++)
        //    {
        //        Console.Write($"{b[i, j]} ");
        //    }
        //    Console.WriteLine();
        //}

        var c = Matrix.Pow(b, K);
        var d = a * c;
        Console.WriteLine(d[0, cnt1]);

        //var e = a * b;
        //for (int i = 0; i <= cnt1; i++)
        //{
        //    Console.Write($"{e[0, i]} ");
        //}


    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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
        /// <paramref name="r"/> 行 <paramref name="c"/> 列目の要素へのアクセスを提供します。
        /// </summary>
        /// <param name="r">行の番号</param>
        /// <param name="c">列の番号</param>
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
        /// <paramref name="m"/>^<paramref name="n"/> を O(<paramref name="m"/>^3 log <paramref name="n"/>) で計算します。
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
        public const long Mod = (int)1e9 + 7;
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
