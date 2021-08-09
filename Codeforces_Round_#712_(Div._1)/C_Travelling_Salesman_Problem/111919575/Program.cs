using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Collections.Generic;

public class Program
{
    private int N;
    private long[] A, C;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = new long[N];
        C = new long[N];
        for (int i = 0; i < N; i++)
        {
            A[i] = sc.NextLong();
            C[i] = sc.NextLong();
        }

        // iからjに移動
        // max(C_i, A[j] - A[i])

        // Cが無い
        // 0

        // C_i >= A_j - A_i
        // A_j - A_i <= C_iな辺
        // 可能か?
        // sum(C)
        const long INF = long.MaxValue / 2;
        var sorted = new int[N];
        for (int i = 0; i < N; i++)
        {
            sorted[i] = i;
        }

        Array.Sort(sorted, (l, r) => A[l].CompareTo(A[r]));

        var st1 = new SegmentTree<long>(N, Math.Min, INF);
        var st2 = new SegmentTree<long>(N, Math.Min, INF);
        st1[N - 1] = 0;
        st2[N - 1] = A[sorted[N - 1]];
        for (int i = N - 2; i >= 0; i--)
        {
            // A_j - A_i <= C_i
            int idx = sorted[i];
            int ok = i;
            int ng = N;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                if (A[sorted[mid]] - A[idx] <= C[idx]) ok = mid;
                else ng = mid;
            }

           
            long t1 = st1.Query(i + 1, ng);
            long t2 = st2.Query(ng, N);

            long cur = Math.Min(t1, t2 - A[idx] - C[idx]);
            // Console.WriteLine($"{idx} {sorted[ok]} {cur}");
            st1[i] = cur;
            st2[i] = cur + A[idx];
        }

        long ans = st1[0];
        foreach (long l in C)
        {
            ans += l;
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