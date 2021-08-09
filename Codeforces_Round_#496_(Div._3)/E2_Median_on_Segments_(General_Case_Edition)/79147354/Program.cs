using System;
using System.Linq;
using CompLib.Collections.Generic;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int m = sc.NextInt();
        int[] a = sc.IntArray();
        // [l,r] にx以下が ceil(len/2)以上 かつ満たすx最大
        // m以下 - m超 偶数 0 奇数 1 以上
        long ans = 0;
        // m
        var st1 = new SegmentTree<int>((l, r) => l + r, 0);
        st1[200000]++;
        var tmp1 = 200000;
        // m-1
        var st2 = new SegmentTree<int>((l, r) => l + r, 0);
        st2[200000]++;
        var tmp2 = 200000;
        for (int i = 0; i < n; i++)
        {
            tmp1 += (a[i] <= m) ? 1 : -1;
            tmp2 += (a[i] <= m - 1) ? 1 : -1;

            // tmp - j >= 0
            ans += st1.Query(0, tmp1 + 1) - st2.Query(0, tmp2 + 1);
            // Console.WriteLine($"{i} {ans}");
            st1[tmp1]++;
            st2[tmp2]++;
        }
        Console.WriteLine(ans);
    }

    public static void Main(string[] args) => new Program().Solve();
}


namespace CompLib.Collections.Generic
{
    using System;

    public class SegmentTree<T>
    {
        // 制約に合った2の冪
        private const int N = 1 << 21;
        private T[] _array;

        private T _identity;
        private Func<T, T, T> _operation;

        public SegmentTree(Func<T, T, T> operation, T identity)
        {
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
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(Separator);
                _index = 0;
            }

            return _line[_index++];
        }

        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public decimal NextDecimal() => decimal.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            _line = Console.ReadLine().Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}
