using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Collections.Generic;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        int[] a = sc.IntArray();

        int[] f = new int[n];
        var st1 = new SegmentTree<int>(n + 1, Math.Max, -1);
        for (int i = 0; i < n; i++)
        {
            f[i] = i - st1.Query(0, a[i]);
            st1[a[i]] = i;
        }


        int[] b = new int[n];
        var st2 = new SegmentTree<int>(n + 1, Math.Min, n);
        for (int i = n - 1; i >= 0; i--)
        {
            b[i] = st2.Query(0, a[i] + 1) - i;
            st2[a[i]] = i;
        }


        int[] min = new int[n + 1];
        int[] max = new int[n + 1];
        int[] cnt = new int[n + 1];
        Array.Fill(min, int.MaxValue);
        Array.Fill(max, int.MinValue);
        for (int i = 0; i < n; i++)
        {
            int len = b[i] + f[i] - 1;
            min[a[i]] = Math.Min(min[a[i]], len);
            max[a[i]] = Math.Max(max[a[i]], len);
            cnt[a[i]]++;
        }

        // Console.WriteLine(string.Join(" ", f));
        // Console.WriteLine(string.Join(" ", b));
        var st3 = new RangeUpdateQuery<int>(n + 1, (l, r) => l + r, 0);
        var st4 = new RangeUpdateQuery<int>(n + 1, (l, r) => l + r, 0);
        for (int i = 1; i <= n; i++)
        {
            if (cnt[i] == 0) continue;
            st4.Update(1, n - i + 2, 1);
            if (cnt[i] >= 2) st3.Update(min[i] + 1, max[i] + 1, 1);
            else st3.Update(1, max[i] + 1, 1);
        }

        int[] ans = new int[n];
        for (int i = 1; i <= n; i++)
        {
            // Console.WriteLine($"{st3}");
            ans[i - 1] = st3[i] == n - i + 1 && st4[i] == n - i + 1 ? 1 : 0;
        }

        Console.WriteLine(string.Join("", ans));
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
        /// <param name="size"></param>
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