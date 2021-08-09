using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Collections.Generic;

public class Program
{
    private int N;
    private int[] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();
        int ans = 0;

        for (int max = 1; max <= 30; max++)
        {
            int sum = 0;
            int min = 0;
            for (int i = 0; i < N; i++)
            {
                if (A[i] <= max)
                {
                    sum += A[i];
                    min = Math.Min(min, sum);

                    ans = Math.Max(ans, sum - min - max);
                }
                else
                {
                    sum = 0;
                    min = 0;
                }
            }
        }

        Console.WriteLine(ans);
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


namespace CompLib.Collections.Generic
{
    using System;
    using System.Diagnostics;

    public class SegmentTree<T>
    {
        // 見かけ上の大きさ、実際の大きさ
        private readonly int _n, _size;
        private T[] _array;

        private T _identity;
        private Func<T, T, T> _operation;

        public SegmentTree(int n, Func<T, T, T> operation, T identity)
        {
            _n = n;
            _size = 1;
            while (_size < _n)
            {
                _size *= 2;
            }

            _identity = identity;
            _operation = operation;
            _array = new T[_size * 2];
            for (int i = 1; i < _size * 2; i++)
            {
                _array[i] = _identity;
            }
        }

        public SegmentTree(T[] a, Func<T, T, T> operation, T identity)
        {
            _n = a.Length;
            _size = 1;
            while (_size < _n)
            {
                _size *= 2;
            }

            _identity = identity;
            _operation = operation;
            _array = new T[_size * 2];
            for (int i = 0; i < a.Length; i++)
            {
                _array[i + _size] = a[i];
            }

            for (int i = a.Length; i < _size; i++)
            {
                _array[i + _size] = identity;
            }

            for (int i = _size - 1; i >= 1; i--)
            {
                _array[i] = operation(_array[i * 2], _array[i * 2 + 1]);
            }
        }

        /// <summary>
        /// A[i]をnに更新 O(log N)
        /// </summary>
        /// <param name="i"></param>
        /// <param name="n"></param>
        public void Update(int i, T n)
        {
            Debug.Assert(0 <= i && i < _n);
            i += _size;
            _array[i] = n;
            while (i > 1)
            {
                i /= 2;
                _array[i] = _operation(_array[i * 2], _array[i * 2 + 1]);
            }
        }

        /// <summary>
        /// A[left] op A[left+1] ... op A[right-1]を求める
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public T Query(int left, int right)
        {
            Debug.Assert(0 <= left && left <= right && right <= _n);
            T sml = _identity;
            T smr = _identity;

            left += _size;
            right += _size;
            while (left < right)
            {
                if ((left & 1) != 0) sml = _operation(sml, _array[left++]);
                if ((right & 1) != 0) smr = _operation(_array[--right], smr);
                left >>= 1;
                right >>= 1;
            }

            return _operation(sml, smr);
        }

        /// <summary>
        /// op(a[0],a[1],...,a[n-1])を返します
        /// </summary>
        /// <returns></returns>
        public T All()
        {
            return _array[1];
        }

        /// <summary>
        /// f(op(a[l],a[l+1],...a[r-1])) = trueとなる最大のrを返します
        /// </summary>
        /// <param name="l"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public int MaxRight(int l, Func<T, bool> f)
        {
            Debug.Assert(0 <= l && l <= _n);
#if DEBUG
            Debug.Assert(f(_identity));
#endif
            if (l == _n) return _n;
            l += _size;
            T sm = _identity;
            do
            {
                while (l % 2 == 0) l >>= 1;
                if (!f(_operation(sm, _array[l])))
                {
                    while (l < _size)
                    {
                        l <<= 1;
                        if (f(_operation(sm, _array[l])))
                        {
                            sm = _operation(sm, _array[l]);
                            l++;
                        }
                    }

                    return l - _size;
                }

                sm = _operation(sm, _array[l]);
                l++;
            } while ((l & -l) != l);

            return _n;
        }

        /// <summary>
        /// f(op(a[l],a[l+1],...a[r-1])) = trueとなる最小のlを返します
        /// </summary>
        /// <param name="r"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public int MinLeft(int r, Func<T, bool> f)
        {
            Debug.Assert(0 <= r && r <= _n);
#if DEBUG
            Debug.Assert(f(_identity));
#endif
            if (r == 0) return 0;
            r += _size;
            T sm = _identity;

            do
            {
                r--;
                while (r > 1 && (r % 2 != 0)) r >>= 1;
                if (!f(_operation(_array[r], sm)))
                {
                    while (r < _size)
                    {
                        r = (2 * r + 1);
                        if (f(_operation(_array[r], sm)))
                        {
                            sm = _operation(_array[r], sm);
                            r--;
                        }
                    }

                    return r + 1 - _size;
                }

                sm = _operation(_array[r], sm);
            } while ((r & -r) != r);

            return 0;
        }

        public T this[int i]
        {
            set { Update(i, value); }
            get
            {
                Debug.Assert(0 <= i && i < _n);
                return _array[i + _size];
            }
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