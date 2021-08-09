using System;
using System.Collections;
using System.Collections.Generic;
using CompLib.Collections.Generic;
using CompLib.Util;

public class Program
{
    int N, K, X;
    int[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        X = sc.NextInt();
        A = sc.IntArray();

        var dp = new long[X + 1, N + 1];
        for (int i = 0; i <= X; i++)
        {
            for (int j = 0; j <= N; j++)
            {
                dp[i, j] = long.MinValue;
            }
        }
        dp[0, 0] = 0;
        long ans = -1;
        for (int i = 1; i <= X; i++)
        {
            // i個使う
            var q = new MinimumQueue<long>((l, r) => r.CompareTo(l));
            q.Enqueue(dp[i - 1, 0]);
            for (int j = 0; j < N; j++)
            {
                var max = q.Min();
                if (max != long.MinValue) dp[i, j + 1] = max + A[j];
                if (i == X && j + K >= N) ans = Math.Max(ans, dp[i, j + 1]);
                q.Enqueue(dp[i - 1, j + 1]);
                if (q.Count > K) q.Dequeue();
            }
        }
        Console.WriteLine(ans);
    }

    public static void Main(string[] args) => new Program().Solve();
}


namespace CompLib.Collections.Generic
{
    using System.Collections.Generic;

    /// <summary>
    /// 最小値を持てるスタック
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class MinimumStack<T>
    {
        private readonly Comparison<T> _compare;
        // 値、最大値
        private readonly Stack<Pair> _stack;

        public MinimumStack(Comparison<T> comparison)
        {
            _compare = comparison;
            _stack = new Stack<Pair>();
        }

        public MinimumStack(IComparer<T> comparer) : this(comparer.Compare) { }

        public MinimumStack() : this(Comparer<T>.Default) { }

        /// <summary>
        /// itemを末尾に追加する
        /// </summary>
        /// <param name="item"></param>
        public void Push(T item)
        {
            if (_stack.Count == 0) _stack.Push(new Pair(item, item));
            else _stack.Push(new Pair(item, _Min(item, _stack.Peek().Second)));
        }

        /// <summary>
        /// 末尾の要素を取り出す
        /// </summary>
        /// <returns></returns>
        public T Pop() => _stack.Pop().First;

        /// <summary>
        /// 末尾の要素を取得する
        /// </summary>
        /// <returns></returns>
        public T Peek() => _stack.Peek().First;

        /// <summary>
        /// 要素の最小値
        /// </summary>
        /// <returns></returns>
        public T Min() => _stack.Peek().Second;

        public int Count => _stack.Count;

        private T _Min(T x, T y)
        {
            return _compare(x, y) <= 0 ? x : y;
        }

        struct Pair
        {
            public T First, Second;
            public Pair(T f, T s)
            {
                First = f;
                Second = s;
            }
        }
    }
}

namespace CompLib.Collections.Generic
{
    using System.Collections.Generic;

    /// <summary>
    /// 最小値を持てるスタック
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class MinimumQueue<T>
    {
        private readonly Comparison<T> _compare;
        // 前、後ろ
        private readonly MinimumStack<T> _s1, _s2;

        public MinimumQueue(Comparison<T> comparison)
        {
            _compare = comparison;
            _s1 = new MinimumStack<T>(comparison);
            _s2 = new MinimumStack<T>(comparison);
        }

        public MinimumQueue(IComparer<T> comparer) : this(comparer.Compare) { }

        public MinimumQueue() : this(Comparer<T>.Default) { }

        /// <summary>
        /// itemを末尾に追加
        /// </summary>
        /// <param name="item"></param>
        public void Enqueue(T item)
        {
            _s2.Push(item);
        }

        /// <summary>
        /// 先頭の要素を取り出す
        /// </summary>
        /// <returns></returns>
        public T Dequeue()
        {
            if (_s1.Count == 0) Move();
            return _s1.Pop();
        }

        /// <summary>
        /// 先頭の値
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            if (_s1.Count == 0) Move();
            return _s1.Peek();
        }

        // s2の要素を反転してs1に入れる
        // s1が空の時
        private void Move()
        {
            while (_s2.Count > 0)
            {
                _s1.Push(_s2.Pop());
            }
        }

        /// <summary>
        /// 要素の最小値
        /// </summary>
        /// <returns></returns>
        public T Min()
        {
            if (_s1.Count == 0) return _s2.Min();
            if (_s2.Count == 0) return _s1.Min();
            return _Min(_s1.Min(), _s2.Min());
        }

        private T _Min(T x, T y)
        {
            return _compare(x, y) <= 0 ? x : y;
        }

        public int Count => _s1.Count + _s2.Count;
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
