using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.Collections.Generic;
using CompLib.Collections.Generic;

public class Program
{
    int N, M;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();


        List<(int l, int r)>[] seg = new List<(int l, int r)>[N];

        {
            var seg2 = new List<(int p, int n)>[N];
            for (int i = 0; i < N; i++)
            {
                seg2[i] = new List<(int p, int n)>();
            }

            for (int i = 0; i < M; i++)
            {
                int ii = sc.NextInt() - 1;
                int ll = sc.NextInt() - 1;
                int rr = sc.NextInt();
                seg2[ii].Add((ll, 1));
                seg2[ii].Add((rr, -1));
            }

            for (int i = 0; i < N; i++)
            {
                seg2[i].Sort((l, r) => l.p != r.p ? l.p.CompareTo(r.p) : r.n.CompareTo(l.n));
                seg[i] = new List<(int l, int r)>();
                int cnt = 0;
                int l = -1;
                foreach ((int p, int n) in seg2[i])
                {
                    if (n > 0 && cnt == 0)
                    {
                        l = p;
                    }
                    cnt += n;
                    if (n < 0 && cnt == 0)
                    {
                        seg[i].Add((l, p));
                    }
                }
            }
        }

        var uq = new List<int>();
        var mp = new Dictionary<int, int>();
        int K;
        {
            var tl = new List<int>();
            for (int i = 0; i < N; i++)
            {
                foreach ((int l, int r) in seg[i])
                {
                    tl.Add(l);
                    tl.Add(r);
                }
            }
            tl.Sort();
            for (int i = 0; i < tl.Count; i++)
            {
                if (i == 0 || tl[i - 1] != tl[i])
                {
                    uq.Add(tl[i]);
                }
            }
            K = uq.Count;
            for (int i = 0; i < uq.Count; i++)
            {
                mp[uq[i]] = i;
            }
        }

        var stMax = new LazySegmentTreeACL<(int cnt, int idx), (int num, int idx, bool flag)>(
            K,
            (l, r) => l.cnt >= r.cnt ? l : r,
            (0, -1),
            (f, x) => f.flag ? (f.num, f.idx) : x,
            (f, g) => f.flag ? f : g,
            (-1, -1, false)
        );

        int[] cnt2 = new int[N];
        int[] from = new int[N];
        for (int i = 0; i < N; i++)
        {
            int max = 0;
            int f = -1;
            foreach ((int l, int r) in seg[i])
            {
                var q = stMax.Query(mp[l], mp[r]);
                if (max < q.cnt)
                {
                    max = q.cnt;
                    f = q.idx;
                }
            }
            cnt2[i] = max + 1;
            from[i] = f;
            foreach ((int l, int r) in seg[i])
            {
                stMax.Apply(mp[l], mp[r], (max + 1, i, true));
            }
        }

        int tmp = 0;
        int idx = -1;
        for (int i = 0; i < N; i++)
        {
            if (tmp < cnt2[i])
            {
                tmp = cnt2[i];
                idx = i;
            }
        }

        bool[] flag = new bool[N];
        flag[idx] = true;
        for (int i = 1; i < tmp; i++)
        {
            idx = from[idx];
            flag[idx] = true;
        }

        var ls = new List<int>();
        for (int i = 0; i < N; i++)
        {
            if (!flag[i]) ls.Add(i + 1);
        }

        Console.WriteLine(N - tmp);
        if (N - tmp > 0) Console.WriteLine(string.Join(" ", ls));
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

namespace CompLib.Collections.Generic
{
    using System;

    public class RangeUpdateQuery<T>
    {
        private readonly int N;
        private T[] _array;
        private readonly bool[] flag;

        private readonly T _identity;
        private readonly Func<T, T, T> _operation;

        /// <summary>
        /// 区間更新、点取得
        /// </summary>
        /// <param name="operation">更新用の演算</param>
        /// <param name="identity">(T, operation)の左単位元</param>
        public RangeUpdateQuery(int size, Func<T, T, T> operation, T identity)
        {
            N = 1;
            while (N < size) N *= 2;
            _identity = identity;
            _operation = operation;
            _array = new T[N * 2];
            for (int i = 1; i < N * 2; i++)
            {
                _array[i] = _identity;
            }

            flag = new bool[N * 2];
        }

        private void Eval(int k, int l, int r)
        {
            if (flag[k])
            {
                if (r - l > 1)
                {
                    _array[k * 2] = _operation(_array[k * 2], _array[k]);
                    flag[k * 2] = true;
                    _array[k * 2 + 1] = _operation(_array[k * 2 + 1], _array[k]);
                    flag[k * 2 + 1] = true;
                    _array[k] = _identity;
                }

                flag[k] = false;
            }
        }

        private void Update(int left, int right, int k, int l, int r, T item)
        {
            Eval(k, l, r);
            if (r <= left || right <= l)
            {
                return;
            }

            if (left <= l && r <= right)
            {
                _array[k] = _operation(_array[k], item);
                flag[k] = true;
                Eval(k, l, r);
                return;
            }

            Update(left, right, k * 2, l, (l + r) / 2, item);
            Update(left, right, k * 2 + 1, (l + r) / 2, r, item);
        }

        /// <summary> 
        /// [left,right)をoperation(A[i],item)に更新
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="item"></param>
        public void Update(int left, int right, T item)
        {
            Update(left, right, 1, 0, N, item);
        }


        /// <summary>
        /// A[i]を取得 O(log N)
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public T Get(int i)
        {
            int l = 0;
            int r = N;
            int k = 1;
            while (r - l > 1)
            {
                Eval(k, l, r);
                int m = (l + r) / 2;
                if (i < m)
                {
                    r = m;
                    k = k * 2;
                }
                else
                {
                    l = m;
                    k = k * 2 + 1;
                }
            }

            return _array[k];
        }

        public T this[int i]
        {
            get { return Get(i); }
        }
    }
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
