using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Collections.Generic;
using CompLib.Util;

public class Program
{
    int N, M;
    int[] A;

    int[] L, R;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        A = sc.IntArray();
        L = new int[M];
        R = new int[M];
        for (int i = 0; i < M; i++)
        {
            L[i] = sc.NextInt() - 1;
            R[i] = sc.NextInt();
        }

        int d = 0;
        int q = 0;
        List<int> c = new List<int>();
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (i == j) continue;
                List<int> tmp = new List<int>();
                int min = A[i];
                int max = A[j];
                for (int k = 0; k < M; k++)
                {
                    // minが入ってる かつmaxが入ってないなら適用
                    bool a = L[k] <= i && i < R[k];
                    bool b = L[k] <= j && j < R[k];
                    if(a && !b)
                    {
                        min--;
                        tmp.Add(k + 1);
                    }
                }

                if(d < max - min)
                {
                    d = max - min;
                    q = tmp.Count;
                    c = tmp;
                }
            }
        }

        Console.WriteLine(d);
        Console.WriteLine(q);
        Console.WriteLine(string.Join(" ",c));
    }

    public static void Main(string[] args) => new Program().Solve();
}

struct Segment
{
    public int L, R, Index;
    public Segment(int l, int r, int i)
    {
        L = l;
        R = r;
        Index = i;
    }
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
