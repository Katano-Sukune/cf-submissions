using System;
using System.Linq;
using CompLib.Collections.Generic;
using CompLib.Util;

public class Program
{
    private int N, M;
    private int[] X, S;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        X = new int[N];
        S = new int[N];
        for (int i = 0; i < N; i++)
        {
            X[i] = sc.NextInt();
            S[i] = sc.NextInt();
        }

        // アンテナ 位置X_i 範囲S_i
        // コイン1枚 範囲+1できる

        // [1,m]をカバーするのに必要なコイン


        // [1,i]をカバーするのに必要なコイン
        long[] dp = new long[M + 1];
        for (int i = 1; i <= M; i++)
        {
            dp[i] = int.MaxValue;
        }

        dp[0] = 0;
        var sorted = new int[N];
        for (int i = 0; i < N; i++)
        {
            sorted[i] = i;
        }

        Array.Sort(sorted, (l, r) => (X[l] - S[l]).CompareTo(X[r] - S[r]));

        foreach (var i in sorted)
        {
            long min = int.MaxValue;
            for (int j = 0; j < S[i]; j++)
            {
                int r = Math.Min(M, X[i] + j);
                int l = Math.Max(0, X[i] - j - 1);
                min = Math.Min(min, Math.Min(dp[r], dp[l]));
            }
          
            for (int c = 0; c <= M; c++)
            {
                int r = Math.Min(M, X[i] + S[i] + c);
                int l = Math.Max(0, X[i] - S[i] - c -1);
                min = Math.Min(min, Math.Min(dp[r], dp[l]));
                dp[r] = Math.Min(dp[r], min + c);
            }
        }

        Console.WriteLine(dp[M]);
    }

    public static void Main(string[] args) => new Program().Solve();
}

namespace CompLib.Collections.Generic
{
    using System;

    public class SegmentTree<T>
    {
        private int N;
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