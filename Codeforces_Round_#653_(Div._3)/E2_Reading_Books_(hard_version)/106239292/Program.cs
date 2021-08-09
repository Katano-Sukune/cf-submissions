using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Collections.Generic;

public class Program
{
    private int N, M, K;
    private int[] T, A, B;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        K = sc.NextInt();
        T = new int[N];
        A = new int[N];
        B = new int[N];
        for (int i = 0; i < N; i++)
        {
            T[i] = sc.NextInt();
            A[i] = sc.NextInt();
            B[i] = sc.NextInt();
        }

        /*
         * N冊本
         * A_i = 1なら alice好き
         * B_i = 1なら bob好き
         *
         * T_iかかる
         *
         * m冊読む
         * k以上好き
         * 時間最小
         */

        // 両方好き
        var both = new List<(int t, int idx)>();
        var alice = new List<(int t, int idx)>();
        var bob = new List<(int t, int idx)>();
        var all = new (long t, int idx, int cnt)[N];
        for (int i = 0; i < N; i++)
        {
            all[i] = (T[i], i, 1);
            if (A[i] == 1)
            {
                if (B[i] == 1)
                {
                    both.Add((T[i], i));
                }
                else
                {
                    alice.Add((T[i], i));
                }
            }
            else if (B[i] == 1)
            {
                bob.Add((T[i], i));
            }
        }

        Array.Sort(all, (l, r) => l.t.CompareTo(r.t));
        var sorted = new int[N];
        for (int i = 0; i < N; i++)
        {
            sorted[all[i].idx] = i;
        }

        var st = new SegmentTree<(long t, int idx, int cnt)>(all, (l, r) => (l.t + r.t, l.idx, l.cnt + r.cnt),
            (0, 0, 0));

        // 両方好きなやつから
        both.Sort((l, r) => l.t.CompareTo(r.t));
        alice.Sort((l, r) => l.t.CompareTo(r.t));
        bob.Sort((l, r) => l.t.CompareTo(r.t));
        long min;
        int minCnt;

        long time1;
        int cnt;
        if (both.Count >= K)
        {
            // 両方好きなやつからK個取る
            time1 = 0;
            for (int i = 0; i < K; i++)
            {
                st[sorted[both[i].idx]] = (0, 0, 0);
                time1 += both[i].t;
            }

            cnt = K;

            var rr = st.MaxRight(0, t => t.cnt <= M - K);
            min = time1 + st.Query(0, rr).t;
            minCnt = K;
        }
        else
        {
            // 両方好きなやつから取れるだけ取る
            // 足りない分はa,bから取る
            if (both.Count + Math.Min(alice.Count, bob.Count) < K || 2 * K - both.Count > M)
            {
                Console.WriteLine("-1");
                return;
            }

            time1 = 0;
            for (int i = 0; i < both.Count; i++)
            {
                st[sorted[both[i].idx]] = (0, 0, 0);
                time1 += both[i].t;
            }

            for (int i = 0; i < K - both.Count; i++)
            {
                st[sorted[alice[i].idx]] = (0, 0, 0);
                time1 += alice[i].t;
                st[sorted[bob[i].idx]] = (0, 0, 0);
                time1 += bob[i].t;
            }

            cnt = both.Count;

            var rr = st.MaxRight(0, t => t.cnt <= M - (2 * K - both.Count));
            min = time1 + st.Query(0, rr).t;
            minCnt = cnt;
        }

        cnt--;
        for (; cnt >= 0 && Math.Min(alice.Count, bob.Count) >= K - cnt && 2 * K - cnt <= M; cnt--)
        {
            // 両方好きなやつ1つ外す
            st[sorted[both[cnt].idx]] = (both[cnt].t, both[cnt].idx, 1);
            time1 -= both[cnt].t;

            // 片方だけ好きなやつ1つずつ追加
            int j = K - (cnt + 1);
            st[sorted[alice[j].idx]] = (0, 0, 0);
            time1 += alice[j].t;
            st[sorted[bob[j].idx]] = (0, 0, 0);
            time1 += bob[j].t;

            int k = M - (2 * K - cnt);
            var rr = st.MaxRight(0, t => t.cnt <= k);
            long time = time1 + st.Query(0, rr).t;
            if (time < min)
            {
                min = time;
                minCnt = cnt;
            }
        }


        var flag = new bool[N];
        var ans = new List<int>();
        for (int i = 0; i < minCnt; i++)
        {
            flag[both[i].idx] = true;
            ans.Add(both[i].idx + 1);
        }

        for (int i = 0; i < K - minCnt; i++)
        {
            flag[alice[i].idx] = true;
            flag[bob[i].idx] = true;
            ans.Add(alice[i].idx + 1);
            ans.Add(bob[i].idx + 1);
        }

        int p = 0;
        for (int i = 0; i < N && p < M - (2 * K - minCnt); i++)
        {
            if (flag[all[i].idx]) continue;
            ans.Add(all[i].idx + 1);
            p++;
        }

        ans.Sort();
        Console.WriteLine(min);
        Console.WriteLine(string.Join(" ", ans));
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