using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CompLib.Collections.Generic;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        int[] a = new int[n];
        int[] f = new int[n];
        for (int i = 0; i < n; i++)
        {
            a[i] = sc.NextInt();
            f[i] = sc.NextInt();
        }

        int[] cnt = new int[n + 1];
        int[] cnt2 = new int[n + 1];
        for (int i = 0; i < n; i++)
        {
            cnt[a[i]]++;
            if (f[i] == 1) cnt2[a[i]]++;
        }

        var sorted = new int[n + 1];
        for (int i = 0; i <= n; i++)
        {
            sorted[i] = i;
        }

        Array.Sort(sorted, (l, r) => cnt2[r].CompareTo(cnt2[l]));

        /*
         * 個数多い順に貪欲に取る
         * 
         * f最大
         * 
         * 目標個数t
         * 
         * 個数t以上
         * f最大
         */
        var st = new SegmentTree<(int num, int index)>(n + 1, (l, r) => l.num >= r.num ? l : r, (int.MinValue, -1));

        // 個数i個のやつ f多い順に
        var q = new Queue<int>[n + 1];
        for (int i = 0; i <= n; i++)
        {
            q[i] = new Queue<int>();
        }

        foreach (var i in sorted)
        {
            q[cnt[i]].Enqueue(cnt2[i]);
        }

        for (int i = 0; i <= n; i++)
        {
            q[i].Enqueue(int.MinValue);
            if (q[i].Count >= 2)
            {
                st[i] = (q[i].Dequeue(), i);
            }
        }

        long ans1 = 0;
        long ans2 = 0;
        for (int t = n; t >= 1; t--)
        {
            var max = st.Query(t, n + 1);
            if (max.num == int.MinValue) continue;
            ans1 += t;
            ans2 += Math.Min(max.num, t);
            st[max.index] = (q[max.index].Dequeue(), max.index);
        }
        Console.WriteLine($"{ans1} {ans2}");
    }

    public static void Main(string[] args) => new Program().Solve();
}


namespace CompLib.Collections.Generic
{
    using System;

    public class SegmentTree<T>
    {
        // 制約に合った2の冪
        private readonly int N;
        private T[] _array;

        private T _identity;
        private Func<T, T, T> _operation;

        public SegmentTree(int size, Func<T, T, T> operation, T identity)
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
        }

        /// <summary>
        /// A[i]をnに更新 O(log N)
        /// </summary>
        /// <param name="i"></param>
        /// <param name="n"></param>
        public void Update(int i, T n)
        {
            i += N;
            _array[i] = n;
            while (i > 1)
            {
                i /= 2;
                _array[i] = _operation(_array[i * 2], _array[i * 2 + 1]);
            }
        }

        private T Query(int left, int right, int k, int l, int r)
        {
            if (r <= left || right <= l)
            {
                return _identity;
            }

            if (left <= l && r <= right)
            {
                return _array[k];
            }

            return _operation(Query(left, right, k * 2, l, (l + r) / 2),
                Query(left, right, k * 2 + 1, (l + r) / 2, r));
        }

        /// <summary>
        /// A[left] op A[left+1] ... op A[right-1]を求める
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public T Query(int left, int right)
        {
            return Query(left, right, 1, 0, N);
        }

        public T this[int i]
        {
            set { Update(i, value); }
            get { return _array[i + N]; }
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
