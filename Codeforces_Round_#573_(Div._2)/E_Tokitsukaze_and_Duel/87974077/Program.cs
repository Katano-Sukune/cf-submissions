using CompLib.Collections.Generic;
using CompLib.Util;
using System;

public class Program
{
    private int N, K;
    private string S;
    private const string Sente = "tokitsukaze";
    private const string Gote = "quailty";
    private const string Draw = "once again";

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        S = sc.Next();

        // 連続するK枚を全部1 or 全部0にする
        // Sを全部同じにしたほうが勝ち
        // 先手 tokitsukaze 後手 quailty
        // 決着つかない once again

        var left0 = new SegmentTree<long>(N, Math.Min, int.MaxValue);
        var right0 = new SegmentTree<long>(N, Math.Max, int.MinValue);
        var left1 = new SegmentTree<long>(N, Math.Min, int.MaxValue);
        var right1 = new SegmentTree<long>(N, Math.Max, int.MinValue);
        for (int i = 0; i < N; i++)
        {
            if (S[i] == '0')
            {
                left0[i] = i;
                right0[i] = i;
            }
            else
            {
                left1[i] = i;
                right1[i] = i;
            }
        }

        // 最初に勝てる
        {
            var l0 = left0.Query(0, N);
            var r0 = right0.Query(0, N);
            
            if (r0 - l0 + 1 <= K)
            {
                Console.WriteLine(Sente);
                return;
            }

            var l1 = left1.Query(0, N);
            var r1 = right1.Query(0, N);
            if (r1 - l1 + 1 <= K)
            {
                Console.WriteLine(Sente);
                return;
            }
        }
        // 先手の操作にかかわらず勝てる

        bool f = true;
        for (int i = 0; i + K <= N && f; i++)
        {
            long l0, r0, l1, r1;
            // 0にする
            {
                l0 = Math.Min(left0.Query(0, i), i);
                r0 = Math.Max(i + K - 1, right0.Query(i + K, N));
                bool a = (r0 - l0 + 1 <= K);

                l1 = Math.Min(left1.Query(0, i), left1.Query(i + K, N));
                r1 = Math.Max(right1.Query(0, i), right1.Query(i + K, N));
                bool b = (r1 - l1 + 1 <= K);
                f &= a || b;
            }

            // 1
            {
                l1 = Math.Min(left1.Query(0, i), i);
                r1 = Math.Max(i + K - 1, right1.Query(i + K, N));
                bool a = (r1 - l1 + 1 > K);

                l0 = Math.Min(left0.Query(0, i), left0.Query(i + K, N));
                r0 = Math.Max(right0.Query(0, i), right0.Query(i + K, N));
                bool b = (r0 - l0 + 1 <= K);
                f &= a || b;
            }
        }

        if (f)
        {
            Console.WriteLine(Gote);
            return;
        }

        Console.WriteLine(Draw);
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