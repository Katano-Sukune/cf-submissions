using System;
using CompLib.Collections;
using CompLib.Util;

public class Program
{
    private int N, M;
    private long[] X, Y;
    private int[] U, V;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();

        X = new long[N];
        Y = new long[N];
        for (int i = 0; i < N; i++)
        {
            X[i] = sc.NextInt();
            Y[i] = sc.NextInt();
        }

        U = new int[M];
        V = new int[M];
        for (int i = 0; i < M; i++)
        {
            U[i] = sc.NextInt() - 1;
            V[i] = sc.NextInt() - 1;
        }

        /*
         * N人
         * 1チーム2人ペア 2つ問題
         *
         * 問題2つ
         *
         * 
         * 人iが 問題1を解くとペナX_i 2だとペナY_i
         *
         * 2人チームを作るとペナの和が最小になるように問題配分する
         *
         * M個組
         * U_i, V_iのペアは作れない
         *
         * 作れるペア総当たり 人iがいたチームのペナ総和
         */

        /*
         * X_i + Y_j < X_j + Y_i
         * X_i - Y_i < X_j - Y_j
         *
         * 上の不等式を満たすやつはiがX,jがYを解く
         *
         * 
         */

        var sorted = new int[N];
        for (int i = 0; i < N; i++)
        {
            sorted[i] = i;
        }

        Array.Sort(sorted, (l, r) => (X[l] - Y[l]).CompareTo(X[r] - Y[r]));
        long[] ans = new long[N];

        long sumX = 0;
        long sumY = 0;
        for (int i = 0; i < N; i++)
        {
            // i以前のやつとは、iはYを解く
            ans[sorted[i]] += sumX + Y[sorted[i]] * i;
            sumX += X[sorted[i]];
            int j = N - i - 1;
            // i以降とは iはXを解く
            ans[sorted[j]] += sumY + X[sorted[j]] * i;
            sumY += Y[sorted[j]];
        }

        // 作れないペア引く
        for (int i = 0; i < M; i++)
        {
            long tmp = Math.Min(X[U[i]] + Y[V[i]], Y[U[i]] + X[V[i]]);
            ans[U[i]] -= tmp;
            ans[V[i]] -= tmp;
        }

        Console.WriteLine(string.Join(" ", ans));
    }

    public static void Main(string[] args) => new Program().Solve();
}

namespace CompLib.Collections
{
    using Num = Int32;

    public class FenwickTree
    {
        private readonly Num[] _array;
        public readonly int Count;

        public FenwickTree(int size)
        {
            _array = new Num[size + 1];
            Count = size;
        }

        /// <summary>
        /// A[i]にnを加算
        /// </summary>
        /// <param name="i"></param>
        /// <param name="n"></param>
        public void Add(int i, Num n)
        {
            i++;
            for (; i <= Count; i += i & -i)
            {
                _array[i] += n;
            }
        }

        /// <summary>
        /// [0,r)の和を求める
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public Num Sum(int r)
        {
            Num result = 0;
            for (; r > 0; r -= r & -r)
            {
                result += _array[r];
            }

            return result;
        }

        /// <summary>
        /// [0,i)の和がw以上になるi
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        public int LowerBound(int w)
        {
            if (w <= 0) return 0;
            int x = 0;
            int k = 1;
            while (k * 2 < Count) k *= 2;
            for (; k > 0; k /= 2)
            {
                if (x + k < Count && _array[x + k] < w)
                {
                    w -= _array[x + k];
                    x += k;
                }
            }

            return x + 1;
        }

        // [l,r)の和を求める
        public Num Sum(int l, int r) => Sum(r) - Sum(l);
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