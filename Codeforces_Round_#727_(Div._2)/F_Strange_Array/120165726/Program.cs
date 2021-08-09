using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.Collections.Generic;
using CompLib.Collections.Generic;

public class Program
{
    int N;
    int[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();

        // (未満-以上)/2
        // (超過-以下+2)/2

        var idx = new List<int>[N + 1];
        for (int i = 1; i <= N; i++)
        {
            idx[i] = new List<int>();
        }
        for (int i = 0; i < N; i++)
        {
            idx[A[i]].Add(i);
        }

        // 全部0以上
        var ar = new int[N + 1];
        for (int i = 0; i <= N; i++)
        {
            ar[i] = -i;
        }

        var lstMin = new LazySegmentTreeACL<int, int>(ar, Math.Min, int.MaxValue, (l, r) => l + r, (f, g) => f + g, 0);
        var lstMax = new LazySegmentTreeACL<int, int>(ar, Math.Max, int.MinValue, (l, r) => l + r, (f, g) => f + g, 0);

        int[] ans = new int[N];

        for (int i = 1; i <= N; i++)
        {
            foreach (int j in idx[i])
            {
                int min = lstMin.Query(j + 1, N + 1);
                int max = lstMax.Query(0, j + 1);
                // Console.WriteLine($"未満-以上 {j} {max - min}");

                ans[j] = Math.Max(ans[j], (max - min) / 2);
            }

            foreach (int j in idx[i])
            {
                lstMin.Apply(j + 1, N + 1, 2);
                lstMax.Apply(j + 1, N + 1, 2);
            }

            // for (int j = 0; j <= N; j++)
            // {
            //     Console.Write($"{lstMin[j]} ");
            // }
            // Console.WriteLine();

            foreach (int j in idx[i])
            {
                int max = lstMax.Query(j + 1, N + 1);
                int min = lstMin.Query(0, j + 1);
                // Console.WriteLine($"超過-以下 {j} {min} {max} {max - min}");
                // Console.WriteLine();
                ans[j] = Math.Max(ans[j], (max - min - 1) / 2);
            }
        }

        Console.WriteLine(string.Join(" ", ans));
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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
        /// <param name="mapping">関数f(x) 区間ではなくnodeの値に操作する</param>
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
