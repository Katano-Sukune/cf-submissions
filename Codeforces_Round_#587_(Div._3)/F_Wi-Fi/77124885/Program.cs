using System;
using System.Linq;
using CompLib.Collections.Generic;
using CompLib.Util;

public class Program
{
    int N, K;
    string S;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        S = sc.Next();

        var dp = new LazySegmentTree<long>(Math.Min, long.MaxValue, (x, y) => x, Math.Min, long.MaxValue);
        dp.Update(0, 0);
        for (int i = 0; i < N; i++)
        {
            dp.Update(i + 1, dp[i] + i + 1);
            if (S[i] == '1')
            {
                int left = Math.Max(0, i - K) ;
                int right = Math.Min(N, i + K + 1);
                dp.Update(left + 1, right + 1, dp.Query(left, right) + i + 1);
            }
        }
        Console.WriteLine(dp[N]);
    }

    public static void Main(string[] args) => new Program().Solve();
}


namespace CompLib.Collections.Generic
{
    using System;

    public class LazySegmentTree<T>
    {
        private const int N = 1 << 18;
        private readonly T[] _array;
        private readonly T[] _tmp;
        private readonly bool[] _flag;


        private readonly T _identity, _updateIdentity;
        private readonly Func<T, T, T> _operation, _update;
        private readonly Func<T, int, T> _multiplication;

        /// <summary>
        /// コンストラクタ updateIdentityで初期化
        /// </summary>
        /// <param name="operation">区間演算用の演算</param>
        /// <param name="identity">(T, operation)の単位元</param>
        /// <param name="multiplication">(T, operation)のスカラー乗算</param>
        /// <param name="update">区間更新用の演算</param>
        /// <param name="updateIdentity">(T, update)の左単位元</param>
        public LazySegmentTree(Func<T, T, T> operation, T identity, Func<T, int, T> multiplication, Func<T, T, T> update,
            T updateIdentity)
        {
            _operation = operation;
            _identity = identity;
            _multiplication = multiplication;
            _update = update;
            _updateIdentity = updateIdentity;
            _array = new T[2 * N];
            for (int i = 0; i < N; i++)
            {
                _array[i + N] = _updateIdentity;
            }

            for (int i = N - 1; i >= 1; i--)
            {
                _array[i] = _operation(_array[i * 2], _array[i * 2 + 1]);
            }

            _tmp = new T[2 * N];
            for (int i = 1; i < 2 * N; i++)
            {
                _tmp[i] = _updateIdentity;
            }

            _flag = new bool[2 * N];
        }

        private void Eval(int k, int l, int r)
        {
            if (_flag[k])
            {
                if (r - l > 1)
                {
                    _tmp[k * 2] = _update(_tmp[k * 2], _tmp[k]);
                    _flag[k * 2] = true;
                    _tmp[k * 2 + 1] = _update(_tmp[k * 2 + 1], _tmp[k]);
                    _flag[k * 2 + 1] = true;
                }

                _array[k] = _update(_array[k], _multiplication(_tmp[k], r - l));
                _tmp[k] = _updateIdentity;
                _flag[k] = false;
            }
        }

        private void Update(int left, int right, int k, int l, int r, T n)
        {
            Eval(k, l, r);
            if (r <= left || right <= l) return;
            if (left <= l && r <= right)
            {
                // 本当は _update(tmp[k], n)だけど 上でEval()したので _tmp[k]は単位元
                _tmp[k] = n;
                _flag[k] = true;
                Eval(k, l, r);
            }
            else
            {
                Update(left, right, k * 2, l, (l + r) / 2, n);
                Update(left, right, k * 2 + 1, (l + r) / 2, r, n);
                _array[k] = _operation(_array[k * 2], _array[k * 2 + 1]);
            }
        }

        /// <summary>
        /// [left, right)をupdate(A[i], n)に更新する
        /// </summary>
        /// <param name="left">右端</param>
        /// <param name="right">左端</param>
        /// <param name="n">値</param>
        public void Update(int left, int right, T n) => Update(left, right, 1, 0, N, n);

        /// <summary>
        /// A[i]をupdate(A[i] ,n)に更新する
        /// </summary>
        /// <param name="i">index</param>
        /// <param name="n">値</param>
        public void Update(int i, T n) => Update(i, i + 1, n);

        private T Query(int left, int right, int k, int l, int r)
        {
            Eval(k, l, r);
            if (r <= left || right <= l) return _identity;
            if (left <= l && r <= right) return _array[k];
            return _operation(Query(left, right, k * 2, l, (l + r) / 2), Query(left, right, k * 2 + 1, (l + r) / 2, r));
        }

        /// <summary>
        /// A[left] op A[left+1] ... A[right-1]を求める O(log N)
        /// </summary>
        /// <param name="left">左端</param>
        /// <param name="right">右端</param>
        /// <returns></returns>
        public T Query(int left, int right) => Query(left, right, 1, 0, N);

        public T this[int i]
        {
            get { return Query(i, i + 1); }
        }

        public T[] ToArray()
        {
            T[] result = new T[N];
            for (int i = 0; i < N; i++)
            {
                result[i] = this[i];
            }

            return result;
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
