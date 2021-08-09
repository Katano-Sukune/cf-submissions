using System;
using System.Linq;
using System.Text;
using CompLib.Collections.Generic;
using CompLib.Mathematics;
using CompLib.Util;

public class Program
{
    private int N, M;
    private int[] C;
    private int[] X, D;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        C = sc.NextCharArray().Select(c => c - '0').Reverse().ToArray();
        X = new int[M];
        D = new int[M];
        for (int i = 0; i < M; i++)
        {
            X[i] = sc.NextInt();
            D[i] = sc.NextInt();
        }

        // 先頭1桁の作り方 * (n-1桁の作り方) + 先頭2桁の作り方 * (n-2桁の作り方)


        var input = new Matrix[N];
        for (int i = 0; i < N; i++)
        {
            var tmp = new Matrix();
            tmp[1, 0] = 1;
            tmp[1, 1] = C[i] + 1;

            if (i > 0 && C[i] == 1 && C[i - 1] <= 8)
            {
                int num = C[i] * 10 + C[i - 1];
                tmp[0, 1] = 18 - num + 1;
            }

            input[i] = tmp;
        }

        var id = new Matrix();
        id[0, 0] = 1;
        id[1, 1] = 1;

        var st = new SegmentTree(id, input);
        var sb = new StringBuilder();

        for (int i = 0; i < M; i++)
        {
            int x = X[i];
            int y = N - x;
            int d = D[i];
            C[y] = d;

            var tmp = new Matrix();
            tmp[1, 0] = 1;
            tmp[1, 1] = C[y] + 1;

            if (y > 0 && C[y] == 1 && C[y - 1] <= 8)
            {
                int num = C[y] * 10 + C[y - 1];
                tmp[0, 1] = 18 - num + 1;
            }

            st[y] = tmp;

            if (y + 1 < N)
            {
                var tmp2 = new Matrix();
                tmp2[1, 0] = 1;
                tmp2[1, 1] = C[y + 1] + 1;
                if (C[y + 1] == 1 && C[y] <= 8)
                {
                    int num = C[y + 1] * 10 + C[y];
                    tmp2[0, 1] = 18 - num + 1;
                }

                st[y + 1] = tmp2;
            }

            var b = st._array[1];
            sb.AppendLine(b[1, 1].ToString());
        }

        Console.Write(sb);
    }

    public static void Main(string[] args) => new Program().Solve();
}

namespace CompLib.Collections.Generic
{
    using System;

    public class SegmentTree
    {
        private const int N = 1 << 19;
        public Matrix[] _array;

        private Matrix _identity;

        public SegmentTree(Matrix identity, Matrix[] input)
        {
            _identity = identity;
            _array = new Matrix[N * 2];
            for (int i = 1; i < N * 2; i++)
            {
                _array[i] = _identity;
            }

            for (int i = 0; i < input.Length; i++)
            {
                _array[i + N] = input[i];
            }

            for (int i = N - 1; i >= 1; i--)
            {
                _array[i] = _array[i * 2] * _array[i * 2 + 1];
            }
        }

        /// <summary>
        /// A[i]をnに更新 O(log N)
        /// </summary>
        /// <param name="i"></param>
        /// <param name="n"></param>
        public void Update(int i, Matrix n)
        {
            i += N;
            _array[i] = n;
            while (i > 1)
            {
                i /= 2;
                _array[i] = _array[i * 2] * _array[i * 2 + 1];
            }
        }

        private Matrix Query(int left, int right, int k, int l, int r)
        {
            if (r <= left || right <= l)
            {
                return _identity;
            }

            if (left <= l && r <= right)
            {
                return _array[k];
            }

            return Query(left, right, k * 2, l, (l + r) / 2) *
                   Query(left, right, k * 2 + 1, (l + r) / 2, r);
        }

        /// <summary>
        /// A[left] op A[left+1] ... op A[right-1]を求める
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public Matrix Query(int left, int right)
        {
            return Query(left, right, 1, 0, N);
        }

        public Matrix this[int i]
        {
            set { Update(i, value); }
            get { return _array[i + N]; }
        }
    }
}

public struct Matrix
{
    /*
     * {
     * {A,B}
     * {C,D}
     * }
     */
    private ModInt A, B, C, D;

    public Matrix(ModInt a, ModInt b, ModInt c, ModInt d)
    {
        A = a;
        B = b;
        C = c;
        D = d;
    }

    public ModInt this[int i, int j]
    {
        get
        {
            if (i == 0)
            {
                if (j == 0)
                {
                    return A;
                }
                else
                {
                    return B;
                }
            }
            else
            {
                if (j == 0)
                {
                    return C;
                }
                else
                {
                    return D;
                }
            }
        }
        set
        {
            if (i == 0)
            {
                if (j == 0)
                {
                    A = value;
                }
                else
                {
                    B = value;
                }
            }
            else
            {
                if (j == 0)
                {
                    C = value;
                }
                else
                {
                    D = value;
                }
            }
        }
    }

    public static Matrix operator *(Matrix l, Matrix r)
    {
        return new Matrix(l.A * r.A + l.B * r.C,
            l.A * r.B + l.B * r.D, l.C * r.A + l.D * r.C, l.C * r.B + l.D * r.D);
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