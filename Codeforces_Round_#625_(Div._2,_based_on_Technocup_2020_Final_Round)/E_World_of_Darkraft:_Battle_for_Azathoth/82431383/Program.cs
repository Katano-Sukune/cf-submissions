using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Collections.Generic;
using CompLib.Util;

public class Program
{
    int N, M, P;
    int[] A, CA;

    int[] B, CB;

    int[] X, Y, Z;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        P = sc.NextInt();

        A = new int[N];
        CA = new int[N];
        for (int i = 0; i < N; i++)
        {
            A[i] = sc.NextInt();
            CA[i] = sc.NextInt();
        }

        B = new int[M];
        CB = new int[M];
        for (int i = 0; i < M; i++)
        {
            B[i] = sc.NextInt();
            CB[i] = sc.NextInt();
        }

        X = new int[P];
        Y = new int[P];
        Z = new int[P];
        for (int i = 0; i < P; i++)
        {
            X[i] = sc.NextInt();
            Y[i] = sc.NextInt();
            Z[i] = sc.NextInt();
        }


        /* 
         * N種類　武器
         * M種類　防具
         * 
         * 1つだけ買う
         * 武器i 攻撃力 A_i 価値 CA_i
         * 
         * 防具j 防御 B_j 価値 CB_j
         * 
         * モンスターP体
         * 
         * モンスターk 防御X_k 攻撃Y_k コインZ_k
         * 
         * a_i > x_k && b_j > y_kの時　モンスターkを倒せる
         * 
         * 倒したモンスターの報酬 - 防具、武器の価格 を最大化 出力
         */


        /*
         * モンスターを Xの昇順で見る
         * 
         * モンスターiのとき
         * 
         * 武器 Aがx_i強 最安
         * 
         * 防具 jを使う B_j未満の Y_kを持つモンスターを倒せる
         * 
         * 
         */


        var stA = new SegmentTree<long>(1000001, Math.Min, long.MaxValue);
        for (int i = 0; i < N; i++)
        {
            stA[A[i]] = Math.Min(stA[A[i]], CA[i]);
        }

        var stB = new LazySegmentTree<long>(1000001, Math.Max, long.MinValue, (l, r) => l, (l, r) => l + r, 0);
        {
            var sortedByB = new int[M];
            for (int i = 0; i < M; i++)
            {
                sortedByB[i] = i;
            }

            Array.Sort(sortedByB, (l, r) => B[l] == B[r] ? CB[l].CompareTo(CB[r]) : B[r].CompareTo(B[l]));

            // 使う候補 これより安くて防御力の高い防具が無い
            var ls = new List<int>();
            int last = int.MaxValue;
            foreach (var i in sortedByB)
            {
                if (CB[i] >= last) continue;
                ls.Add(i);
                last = CB[i];
            }

            ls.Reverse();
            last = 0;
            foreach (int i in ls)
            {
                stB.Update(last, B[i] + 1, -CB[i]);
                last = B[i] + 1;
            }
            stB.Update(last, 1000001, long.MinValue / 2);
        }

        long ans = -(CA.Min() + CB.Min());

        var sortedByX = new int[P];
        for (int i = 0; i < P; i++)
        {
            sortedByX[i] = i;
        }

        Array.Sort(sortedByX, (l, r) => X[l].CompareTo(X[r]));

        foreach (var i in sortedByX)
        {
            // iまでのモンスター
            var minA = stA.Query(X[i] + 1, 1000001);
            stB.Update(Y[i] + 1, 1000001, Z[i]);
            var maxB = stB.Query(0, 1000001);


            if (minA == long.MaxValue) continue;


            ans = Math.Max(ans, maxB - minA);

        }
        Console.WriteLine(ans);
    }

    public static void Main(string[] args) => new Program().Solve();
}


namespace CompLib.Collections.Generic
{
    using System;

    public class LazySegmentTree<T>
    {
        private readonly int N;
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
        public LazySegmentTree(int size, Func<T, T, T> operation, T identity, Func<T, int, T> multiplication, Func<T, T, T> update,
            T updateIdentity)
        {
            N = 1;
            while (N < size) N *= 2;
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
