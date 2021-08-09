using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Collections.Generic;
using CompLib.Mathematics;

public class Program
{
    private int N, M;
    private const int MaxK = 5;
    private ModInt[][] Sum;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();

        Sum = new ModInt[MaxK + 1][];
        for (int i = 0; i <= MaxK; i++)
        {
            Sum[i] = new ModInt[N + 1];
            for (int j = 1; j <= N; j++)
            {
                ModInt tmp = 1;
                for (int k = 0; k < i; k++)
                {
                    tmp *= j;
                }

                Sum[i][j] = Sum[i][j - 1] + tmp;
            }
        }

        var ar = new S[N];
        for (int i = 0; i < N; i++)
        {
            int a = sc.NextInt();
            ar[i] = new S(a, a, a, a, a, a, 1);
        }

        var lst = new LazySegmentTreeACL<S, (int num, bool f)>(ar,
            OP,
            new S(0, 0, 0, 0, 0, 0, 0),
            Mapping,
            (f, g) => f.f ? f : g,
            (0, false));

#if !DEBUG
System.Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
#endif
        for (int i = 0; i < M; i++)
        {
            char t = sc.NextChar();
            if (t == '?')
            {
                int l = sc.NextInt() - 1;
                int r = sc.NextInt();
                int k = sc.NextInt();
                Console.WriteLine(lst.Query(l, r)[k]);
            }
            else if (t == '=')
            {
                int l = sc.NextInt() - 1;
                int r = sc.NextInt();
                int x = sc.NextInt();
                lst.Apply(l, r, (x, true));
            }
        }

        Console.Out.Flush();
    }

    S OP(S l, S r)
    {
        ModInt l0 = 1;
        ModInt l1 = l0 * l.Len;
        ModInt l2 = l1 * l.Len;
        ModInt l3 = l2 * l.Len;
        ModInt l4 = l3 * l.Len;
        ModInt l5 = l4 * l.Len;
        ModInt n0 = l[0] + r[0];
        ModInt n1 = l[1] + r[1] + r[0] * l1;
        ModInt n2 = l[2] + r[2] + 2 * r[1] * l1 + r[0] * l2;
        ModInt n3 = l[3] + r[3] + 3 * r[2] * l1 + 3 * r[1] * l2 + r[0] * l3;
        ModInt n4 = l[4] + r[4] + 4 * r[3] * l1 + 6 * r[2] * l2 + 4 * r[1] * l3 + r[0] * l4;
        ModInt n5 = l[5] + r[5] + 5 * r[4] * l1 + 10 * r[3] * l2 + 10 * r[2] * l3 + 5 * r[1] * l4 + r[0] * l5;
        return new S(n0, n1, n2, n3, n4, n5, l.Len + r.Len);
    }

    S Mapping((int num, bool f) f, S x)
    {
        if (!f.f) return x;
        ModInt n0 = f.num * Sum[0][x.Len];
        ModInt n1 = f.num * Sum[1][x.Len];
        ModInt n2 = f.num * Sum[2][x.Len];
        ModInt n3 = f.num * Sum[3][x.Len];
        ModInt n4 = f.num * Sum[4][x.Len];
        ModInt n5 = f.num * Sum[5][x.Len];
        return new S(n0, n1, n2, n3, n4, n5, x.Len);
    }


    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

public struct S
{
    private ModInt N0, N1, N2, N3, N4, N5;
    public int Len { get; private set; }

    public S(ModInt n0, ModInt n1, ModInt n2, ModInt n3, ModInt n4, ModInt n5, int len)
    {
        N0 = n0;
        N1 = n1;
        N2 = n2;
        N3 = n3;
        N4 = n4;
        N5 = n5;
        Len = len;
    }

    public ModInt this[int i]
    {
        get
        {
            switch (i)
            {
                case 0:
                    return N0;
                case 1:
                    return N1;
                case 2:
                    return N2;
                case 3:
                    return N3;
                case 4:
                    return N4;
                case 5:
                    return N5;
                default:
                    throw new IndexOutOfRangeException();
            }
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


namespace CompLib.Collections.Generic
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// 長さnの配列の区間演算、区間更新ができるデータ構造
    /// </summary>
    /// <typeparam name="S">モノイドの型</typeparam>
    /// <typeparam name="F">写像の型</typeparam>
    public class LazySegmentTreeACL<S, F>
    {
        private readonly int _n;
        private readonly int _size;
        private int _log;

        private readonly S[] _d;
        private readonly F[] _lz;

        private readonly Func<S, S, S> _op;
        private readonly S _e;
        private readonly Func<F, S, S> _mapping;
        private readonly Func<F, F, F> _composition;
        private readonly F _id;

        /// <param name="n">サイズ</param>
        /// <param name="op">区間演算</param>
        /// <param name="e">Sの単位元</param>
        /// <param name="mapping">関数f(x)</param>
        /// <param name="composition">f*g, f(g(x))</param>
        /// <param name="id">f(x) = xとなるf (恒等写像)</param>
        public LazySegmentTreeACL(int n, Func<S, S, S> op, S e, Func<F, S, S> mapping, Func<F, F, F> composition, F id)
        {
            _n = n;
            _op = op;
            _e = e;
            _mapping = mapping;
            _composition = composition;
            _id = id;
            _size = 1;
            _log = 0;
            while (_size < _n)
            {
                _size <<= 1;
                _log++;
            }

            _d = new S[2 * _size];
            for (int i = 0; i < 2 * _size; i++)
            {
                _d[i] = _e;
            }

            _lz = new F[_size];
            for (int i = 0; i < _size; i++)
            {
                _lz[i] = _id;
            }
        }

        /// <param name="v">元配列</param>
        /// <param name="op">区間演算</param>
        /// <param name="e">Sの単位元</param>
        /// <param name="mapping">関数f(x)</param>
        /// <param name="composition">fの積</param>
        /// <param name="id">f(x) = xとなるf (恒等写像)</param>
        public LazySegmentTreeACL(S[] v, Func<S, S, S> op, S e, Func<F, S, S> mapping, Func<F, F, F> composition, F id)
        {
            _n = v.Length;
            _op = op;
            _e = e;
            _mapping = mapping;
            _composition = composition;
            _id = id;
            _size = 1;
            _log = 0;
            while (_size < _n)
            {
                _size <<= 1;
                _log++;
            }

            _d = new S[2 * _size];
            for (int i = 0; i < _n; i++)
            {
                _d[i + _size] = v[i];
            }

            for (int i = _n; i < _size; i++)
            {
                _d[i + _size] = _e;
            }

            for (int i = _size - 1; i >= 1; i--)
            {
                Update(i);
            }

            _lz = new F[_size];
            for (int i = 0; i < _size; i++)
            {
                _lz[i] = _id;
            }
        }

        /// <summary>
        /// A[p]にxを代入 O(log n)
        /// </summary>
        /// <param name="p"></param>
        /// <param name="x"></param>
        public void Set(int p, S x)
        {
            Debug.Assert(0 <= p && p < _n);
            p += _size;
            for (int i = _log; i >= 1; i--) Push(p >> i);
            _d[p] = x;
            for (int i = 1; i <= _log; i++) Update(p >> i);
        }

        /// <summary>
        /// A[p] を返す O(log n)
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public S Get(int p)
        {
            Debug.Assert(0 <= p && p < _n);
            p += _size;
            for (int i = _log; i >= 1; i--) Push(p >> i);
            return _d[p];
        }

        /// <summary>
        /// op(A[l,r))を計算します O(log n)
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public S Query(int l, int r)
        {
            Debug.Assert(0 <= l && l <= r && r <= _n);
            if (l == r) return _e;
            l += _size;
            r += _size;
            for (int i = _log; i >= 1; i--)
            {
                if (((l >> i) << i) != l) Push(l >> i);
                if (((r >> i) << i) != r) Push(r >> i);
            }

            S sml = _e, smr = _e;
            while (l < r)
            {
                if ((l & 1) != 0) sml = _op(sml, _d[l++]);
                if ((r & 1) != 0) smr = _op(_d[--r], smr);
                l >>= 1;
                r >>= 1;
            }

            return _op(sml, smr);
        }

        /// <summary>
        /// op(A)を計算します O(1)
        /// </summary>
        /// <returns></returns>
        public S All()
        {
            return _d[1];
        }

        /// <summary>
        /// A[p]にf(A[p])を代入します O(log n)
        /// </summary>
        /// <param name="p"></param>
        /// <param name="f"></param>
        public void Apply(int p, F f)
        {
            Debug.Assert(0 <= p && p < _n);
            p += _size;
            for (int i = _log; i >= 1; i--) Push(p >> i);
            _d[p] = _mapping(f, _d[p]);
            for (int i = 1; i <= _log; i++) Update(p >> i);
        }

        /// <summary>
        /// i = l,l+1,...,r-1について A[i]にf(A[i])を代入します O(log n)
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <param name="f"></param>
        public void Apply(int l, int r, F f)
        {
            Debug.Assert(0 <= l && l <= r && r <= _n);
            if (l == r) return;

            l += _size;
            r += _size;

            for (int i = _log; i >= 1; i--)
            {
                if (((l >> i) << i) != l) Push(l >> i);
                if (((r >> i) << i) != r) Push((r - 1) >> i);
            }

            {
                int l2 = l, r2 = r;
                while (l < r)
                {
                    if ((l & 1) > 0) AllApply(l++, f);
                    if ((r & 1) > 0) AllApply(--r, f);
                    l >>= 1;
                    r >>= 1;
                }

                l = l2;
                r = r2;
            }

            for (int i = 1; i <= _log; i++)
            {
                if (((l >> i) << i) != l) Update(l >> i);
                if (((r >> i) << i) != r) Update((r - 1) >> i);
            }
        }

        /// <summary>
        /// g(op(A[l,r))) = true となる最大のrを探します O(log n)
        /// </summary>
        /// <param name="l"></param>
        /// <param name="g"></param>
        /// <returns></returns>
        public int MaxRight(int l, Func<S, bool> g)
        {
            Debug.Assert(0 <= l && l <= _n);
#if DEBUG
            Debug.Assert(g(_e));
#endif
            if (l == _n) return _n;
            l += _size;
            for (int i = _log; i >= 1; i--) Push(l >> i);
            S sm = _e;
            do
            {
                while (l % 2 == 0) l >>= 1;
                if (!g(_op(sm, _d[l])))
                {
                    while (l < _size)
                    {
                        Push(l);
                        l = (2 * l);
                        if (g(_op(sm, _d[l])))
                        {
                            sm = _op(sm, _d[l]);
                            l++;
                        }
                    }

                    return l - _size;
                }

                sm = _op(sm, _d[l]);
                l++;
            } while ((l & -l) != l);

            return _n;
        }

        /// <summary>
        /// g(op(A[l,r))) = trueとなる最小のlを探します O(log n)
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <returns></returns>
        public int MinLeft(int r, Func<S, bool> g)
        {
            Debug.Assert(0 <= r && r <= _n);
#if DEBUG
            Debug.Assert(g(_e));
#endif
            if (r == 0) return 0;
            r += _size;
            for (int i = _log; i >= 1; i--) Push((r - 1) >> i);
            S sm = _e;
            do
            {
                r--;
                while (r > 1 && (r % 2) != 0) r >>= 1;
                if (!g(_op(_d[r], sm)))
                {
                    while (r < _size)
                    {
                        Push(r);
                        r = (2 * r + 1);
                        if (g(_op(_d[r], sm)))
                        {
                            sm = _op(_d[r], sm);
                            r--;
                        }
                    }

                    return r + 1 - _size;
                }

                sm = _op(_d[r], sm);
            } while ((r & -r) != r);

            return 0;
        }

        public S this[int p]
        {
            get { return Get(p); }
            set { Set(p, value); }
        }

        private void Update(int k)
        {
            _d[k] = _op(_d[2 * k], _d[2 * k + 1]);
        }

        private void AllApply(int k, F f)
        {
            _d[k] = _mapping(f, _d[k]);
            if (k < _size) _lz[k] = _composition(f, _lz[k]);
        }

        private void Push(int k)
        {
            AllApply(2 * k, _lz[k]);
            AllApply(2 * k + 1, _lz[k]);
            _lz[k] = _id;
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