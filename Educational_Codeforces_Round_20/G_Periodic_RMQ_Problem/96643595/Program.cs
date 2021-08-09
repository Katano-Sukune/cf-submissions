using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Collections.Generic;

public class Program
{
    private int N, K;
    private int[] B;
    private int Q;
    private int[] T, L, R, X;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        B = sc.IntArray();
        Q = sc.NextInt();
        T = new int[Q];
        L = new int[Q];
        R = new int[Q];
        X = new int[Q];
        for (int i = 0; i < Q; i++)
        {
            T[i] = sc.NextInt();
            L[i] = sc.NextInt() - 1;
            R[i] = sc.NextInt();
            if (T[i] == 1)
            {
                X[i] = sc.NextInt();
            }
        }

        var ls = new List<int>(2 * Q);
        for (int i = 0; i < Q; i++)
        {
            ls.Add(L[i]);
            ls.Add(R[i]);
        }

        ls.Sort();
        var uq = new List<int>(2 * Q);
        for (int i = 0; i < ls.Count; i++)
        {
            if (i == 0 || ls[i - 1] != ls[i])
            {
                uq.Add(ls[i]);
            }
        }

        var map = new Dictionary<int, int>();
        for (int i = 0; i < uq.Count; i++)
        {
            map[uq[i]] = i;
        }

        var st = new SegmentTree<int>(B, Math.Min, int.MaxValue);

        var lst = new LazySegmentTree<int>(uq.Count - 1, Math.Min, int.MaxValue, (l, r) => l, (l, r) => r,
            int.MaxValue);
        // a_i A[uq_i:uq_{i+1}]の最小値
        for (int i = 0; i < uq.Count - 1; i++)
        {
            if (uq[i] % N == 0)
            {
                lst.Update(i, uq[i] / N == uq[i + 1] / N ? st.Query(0, uq[i + 1] % N) : st.All());
            }
            else
            {
                if (uq[i + 1] % N == 0)
                {
                    lst.Update(i, uq[i] / N + 1 == uq[i + 1] / N ? st.Query(uq[i] % N, N) : st.All());
                }
                else
                {
                    if (uq[i] / N + 1 < uq[i + 1] / N)
                    {
                        lst.Update(i, st.All());
                    }
                    else if (uq[i] / N == uq[i + 1] / N)
                    {
                        lst.Update(i, st.Query(uq[i] % N, uq[i + 1] % N));
                    }
                    else
                    {
                        lst.Update(i, Math.Min(st.Query(uq[i] % N, N), st.Query(0, uq[i + 1] % N)));
                    }
                }
            }

        }


        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        for (int i = 0; i < Q; i++)
        {
            int l = map[L[i]];
            int r = map[R[i]];
            if (T[i] == 1)
            {
                lst.Update(l, r, X[i]);
            }
            else if (T[i] == 2)
            {
                Console.WriteLine(lst.Query(l, r));
            }
        }

        Console.Out.Flush();
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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

namespace CompLib.Collections.Generic
{
    using System;

    public class LazySegmentTree<T>
    {
        private readonly int N;
        private readonly T[] _array;
        private readonly T[] _tmp;
        private readonly bool[] _flag;


        private readonly T _identity, _updateIdentity;
        private readonly Func<T, T, T> _operation, _update;
        private readonly Func<T, int, T> _multiplication;

        /// <summary>
        /// コンストラクタ updateIdentityで初期化
        /// </summary>
        /// <param name="operation">区間演算用の演算</param>
        /// <param name="identity">(T, operation)の単位元</param>
        /// <param name="multiplication">(T, operation)のスカラー乗算</param>
        /// <param name="update">区間更新用の演算</param>
        /// <param name="updateIdentity">(T, update)の左単位元</param>
        public LazySegmentTree(int size, Func<T, T, T> operation, T identity, Func<T, int, T> multiplication,
            Func<T, T, T> update,
            T updateIdentity)
        {
            N = 1;
            while (N < size) N *= 2;
            _operation = operation;
            _identity = identity;
            _multiplication = multiplication;
            _update = update;
            _updateIdentity = updateIdentity;
            _array = new T[2 * N];
            for (int i = 0; i < N; i++)
            {
                _array[i + N] = _updateIdentity;
            }

            for (int i = N - 1; i >= 1; i--)
            {
                _array[i] = _operation(_array[i * 2], _array[i * 2 + 1]);
            }

            _tmp = new T[2 * N];
            for (int i = 1; i < 2 * N; i++)
            {
                _tmp[i] = _updateIdentity;
            }

            _flag = new bool[2 * N];
        }

        private void Eval(int k, int l, int r)
        {
            if (_flag[k])
            {
                if (r - l > 1)
                {
                    _tmp[k * 2] = _update(_tmp[k * 2], _tmp[k]);
                    _flag[k * 2] = true;
                    _tmp[k * 2 + 1] = _update(_tmp[k * 2 + 1], _tmp[k]);
                    _flag[k * 2 + 1] = true;
                }

                _array[k] = _update(_array[k], _multiplication(_tmp[k], r - l));
                _tmp[k] = _updateIdentity;
                _flag[k] = false;
            }
        }

        private void Update(int left, int right, int k, int l, int r, T n)
        {
            Eval(k, l, r);
            if (r <= left || right <= l) return;
            if (left <= l && r <= right)
            {
                // 本当は _update(tmp[k], n)だけど 上でEval()したので _tmp[k]は単位元
                _tmp[k] = n;
                _flag[k] = true;
                Eval(k, l, r);
            }
            else
            {
                Update(left, right, k * 2, l, (l + r) / 2, n);
                Update(left, right, k * 2 + 1, (l + r) / 2, r, n);
                _array[k] = _operation(_array[k * 2], _array[k * 2 + 1]);
            }
        }

        /// <summary>
        /// [left, right)をupdate(A[i], n)に更新する
        /// </summary>
        /// <param name="left">右端</param>
        /// <param name="right">左端</param>
        /// <param name="n">値</param>
        public void Update(int left, int right, T n) => Update(left, right, 1, 0, N, n);

        /// <summary>
        /// A[i]をupdate(A[i] ,n)に更新する
        /// </summary>
        /// <param name="i">index</param>
        /// <param name="n">値</param>
        public void Update(int i, T n) => Update(i, i + 1, n);

        private T Query(int left, int right, int k, int l, int r)
        {
            Eval(k, l, r);
            if (r <= left || right <= l) return _identity;
            if (left <= l && r <= right) return _array[k];
            return _operation(Query(left, right, k * 2, l, (l + r) / 2), Query(left, right, k * 2 + 1, (l + r) / 2, r));
        }

        /// <summary>
        /// A[left] op A[left+1] ... A[right-1]を求める O(log N)
        /// </summary>
        /// <param name="left">左端</param>
        /// <param name="right">右端</param>
        /// <returns></returns>
        public T Query(int left, int right) => Query(left, right, 1, 0, N);

        public T this[int i]
        {
            get { return Query(i, i + 1); }
        }

        public T[] ToArray()
        {
            T[] result = new T[N];
            for (int i = 0; i < N; i++)
            {
                result[i] = this[i];
            }

            return result;
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