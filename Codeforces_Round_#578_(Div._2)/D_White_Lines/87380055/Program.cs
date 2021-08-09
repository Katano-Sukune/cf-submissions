using System;
using CompLib.Collections.Generic;
using CompLib.Util;

public class Program
{
    private int N, K;
    private char[][] S;


    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        S = new char[N][];
        for (int i = 0; i < N; i++)
        {
            S[i] = sc.NextCharArray();
        }

        // (i,j)を消す
        // (i,j)を左上、K*Kの範囲がWになる

        // Bが無い行、列を最大化


        var row = new int[N, N + 1];
        var col = new int[N, N + 1];


        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                row[i, j + 1] = row[i, j];
                col[j, i + 1] = col[j, i];
                if (S[i][j] == 'B')
                {
                    row[i, j + 1]++;
                    col[j, i + 1]++;
                }
            }
        }

        int[,] t = new int[N - K + 1, N - K + 1];


        for (int i = 0; i <= N - K; i++)
        {
            int tmpR = 0;
            int tmpC = 0;
            for (int j = 0; j < N; j++)
            {
                if (j < K)
                {
                    tmpR += row[j, N] - row[j, i + K] + row[j, i] == 0 ? 1 : 0;
                    tmpC += col[j, N] - col[j, i + K] + col[j, i] == 0 ? 1 : 0;
                }
                else
                {
                    tmpR += row[j, N] == 0 ? 1 : 0;
                    tmpC += col[j, N] == 0 ? 1 : 0;
                }
            }

            t[0, i] += tmpR;
            t[i, 0] += tmpC;
            for (int j = 0; j < N - K; j++)
            {
                {
                    int all = row[j, N];
                    if (all > 0 && all - row[j, i + K] + row[j, i] == 0) tmpR--;
                    int next = row[j + K, N];
                    if (next > 0 && next - row[j + K, i + K] + row[j + K, i] == 0) tmpR++;
                }

                {
                    int all = col[j, N];
                    if (all > 0 && all - col[j, i + K] + col[j, i] == 0) tmpC--;
                    int next = col[j + K, N];
                    if (next > 0 && next - col[j + K, i + K] + col[j + K, i] == 0) tmpC++;
                }
                t[j + 1, i] += tmpR;
                t[i, j + 1] += tmpC;
            }
        }

        int ans = 0;
        for (int i = 0; i <= N - K; i++)
        {
            for (int j = 0; j <= N - K; j++)
            {
                ans = Math.Max(ans, t[i, j]);
      
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