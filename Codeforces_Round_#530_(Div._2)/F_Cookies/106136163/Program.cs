using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using CompLib.Collections.Generic;

public class Program
{
    int N;
    long Time;
    const int MaxT = 1000000;
    long[] X;
    int[] T;

    long[] L;
    List<int>[] E;

    List<int> Uq;
    int[] Map;

    SegmentTree<(long cnt, long sum)> St;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        Time = sc.NextLong();

        X = sc.LongArray();
        T = sc.IntArray();

        L = new long[N];
        E = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<int>();
        }

        for (int i = 1; i < N; i++)
        {
            int p = sc.NextInt() - 1;
            E[p].Add(i);
            L[i] = sc.NextLong();
        }

        var ar = new int[N];
        for (int i = 0; i < N; i++)
        {
            ar[i] = T[i];
        }
        Array.Sort(ar);
        Uq = new List<int>();
        for (int i = 0; i < N; i++)
        {
            if (i == 0 || ar[i - 1] != ar[i]) Uq.Add(ar[i]);
        }
        Map = new int[MaxT + 1];
        for (int i = 0; i < Uq.Count; i++)
        {
            Map[Uq[i]] = i;
        }

        St = new SegmentTree<(long cnt, long sum)>(MaxT + 1, (l, r) => (l.cnt + r.cnt, l.sum + r.sum), (0, 0));

        Console.WriteLine(Go(0, 0));
    }

    long Go(int cur, long time)
    {
        checked
        {
            if (time > Time) return long.MinValue;
            long max = long.MinValue;
            long max2 = long.MinValue;
            var tuple = St[T[cur]];
            St[T[cur]] = (tuple.cnt + X[cur], tuple.sum + X[cur] * T[cur]);
            foreach (var to in E[cur])
            {
                long tmp = Go(to, time + 2*L[to]);
                if (tmp > max)
                {
                    max2 = max;
                    max = tmp;
                }
                else if (tmp > max2)
                {
                    max2 = tmp;
                }
            }

            long tt = Time - time;
            long result = cur == 0 ? max : max2;
            var rr = St.MaxRight(0, t => t.sum <= tt);
            var tmp2 = St.Query(0, rr);
            if (rr == MaxT + 1)
            {
                result = Math.Max(result, tmp2.cnt);
            }
            else
            {
                result = Math.Max(result, tmp2.cnt + Math.Min((tt - tmp2.sum) / rr, St[rr].cnt));
            }

            St[T[cur]] = tuple;
            // Console.WriteLine($"{cur} {result}");
            return result;
        }
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
