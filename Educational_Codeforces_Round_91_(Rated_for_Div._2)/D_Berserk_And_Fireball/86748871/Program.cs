using System;
using System.IO;
using CompLib.Collections.Generic;
using CompLib.Util;

public class Program
{
    private int N, M;
    private int K;
    private long X, Y;
    private int[] A;
    private int[] B;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        X = sc.NextInt();
        K = sc.NextInt();
        Y = sc.NextInt();
        A = sc.IntArray();
        B = sc.IntArray();

        // N人 iの強さ A_i
        /*
         * x消費して 連続するk体破壊
         *
         * y消費して連続する2体選んで弱い方を破壊
         *
         * AをBにする最小マナ消費
         */

        int[] index = new int[N + 1];
        for (int i = 0; i <= N; i++)
        {
            index[i] = -1;
        }

        for (int i = 0; i < N; i++)
        {
            index[A[i]] = i;
        }

        var st = new SegmentTree<int>(N, Math.Max, int.MinValue);
        for (int i = 0; i < N; i++)
        {
            st[i] = A[i];
        }

        long ans = 0;
        for (int i = 0; i < M; i++)
        {
            if (index[B[i]] == -1)
            {
                Console.WriteLine(-1);
                return;
            }

            if (i > 0 && index[B[i - 1]] > index[B[i]])
            {
                Console.WriteLine(-1);
                return;
            }

            // 前との間にあるやつの
            if (i == 0)
            {
                int d = index[B[i]];
                var max = st.Query(0, index[B[i]]);
                if (Y * K <= X)
                {
                    // 1つずつ消した方が得
                    // 区間内最大
                    if (max < B[i])
                    {
                        // Bで消せる
                        ans += Y * d;
                    }
                    else
                    {
                        // 消せない
                        // K個にしてからX消費
                        if (d < K)
                        {
                            Console.WriteLine(-1);
                            return;
                        }

                        ans += (d - K) * Y + X;
                    }
                }
                else
                {
                    // できるだけX使う
                    if (d < K && max > B[i])
                    {
                        Console.WriteLine(-1);
                        return;
                    }

                    ans += (d / K) * X + (d % K) * Y;
                }
            }
            else
            {
                int d = index[B[i]] - index[B[i - 1]] - 1;
                var max = st.Query(index[B[i - 1]] + 1, index[B[i]]);
                if (Y * K <= X)
                {
                    // 1つずつ消した方が得
                    // 区間内最大
                    if (max < Math.Max(B[i - 1], B[i]))
                    {
                        // Bで消せる
                        ans += Y * d;
                    }
                    else
                    {
                        // 消せない
                        // K個にしてからX消費
                        if (d < K)
                        {
                            Console.WriteLine(-1);
                            return;
                        }

                        ans += (d - K) * Y + X;
                    }
                }
                else
                {
                    if (d < K && max > Math.Max(B[i - 1], B[i]))
                    {
                        Console.WriteLine(-1);
                        return;
                    }

                    ans += (d / K) * X + (d % K) * Y;
                }
            }
        }

        {
            // 最後
            int d = N - 1 - index[B[M - 1]];
            var max = st.Query(index[B[M - 1]] + 1, N);
            if (Y * K <= X)
            {
                // 1つずつ消した方が得
                // 区間内最大
                if (max < B[M - 1])
                {
                    // Bで消せる
                    ans += Y * d;
                }
                else
                {
                    // 消せない
                    // K個にしてからX消費
                    if (d < K)
                    {
                        Console.WriteLine(-1);
                        return;
                    }

                    ans += (d - K) * Y + X;
                }
            }
            else
            {
                if (d < K && max > B[M - 1])
                {
                    Console.WriteLine(-1);
                    return;
                }

                ans += (d / K) * X + (d % K) * Y;
            }
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