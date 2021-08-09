using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CompLib.Collections.Generic;
using CompLib.Util;

public class Program
{
    int N, M;
    int[] Y, LX, RX;

    int[] X, LY, RY;
    const int L = 1000000;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();

        Y = new int[N];
        LX = new int[N];
        RX = new int[N];
        for (int i = 0; i < N; i++)
        {
            Y[i] = sc.NextInt();
            LX[i] = sc.NextInt();
            RX[i] = sc.NextInt();
        }

        X = new int[M];
        LY = new int[M];
        RY = new int[M];
        for (int i = 0; i < M; i++)
        {
            X[i] = sc.NextInt();
            LY[i] = sc.NextInt();
            RY[i] = sc.NextInt();
        }

        /*
         * 1辺 1e6の正方形
         * 
         * 線分引く
         * 
         * 正方形の少なくとも1辺と交差する
         * 
         * 何個に分割されるか?
         */

        /*
         * 交点の個数+1
         * 
         * 下から生える縦
         * 
         * Y小さい順に見る
         * 
         * 横左から生える
         * Y未満
         * 
         * 
         */
        long ans = 1;

        var u1 = new List<(int x, int ry)>();
        var u2 = new List<(int x, int ry)>();
        for (int i = 0; i < M; i++)
        {
            if (LY[i] == 0)
            {
                u1.Add((X[i], RY[i]));
                if (RY[i] == L) ans++;
            }
            else
            {
                u2.Add((L - X[i], L - LY[i]));
            }
        }

        var l1 = new List<(int y, int rx)>();
        var l2 = new List<(int y, int rx)>();
        var r1 = new List<(int y, int lx)>();
        var r2 = new List<(int y, int lx)>();
        for (int i = 0; i < N; i++)
        {
            if (LX[i] == 0)
            {
                l1.Add((Y[i], RX[i]));
                r2.Add((L - Y[i], L - RX[i]));

                if (RX[i] == L) ans++;
            }
            else
            {
                r1.Add((Y[i], LX[i]));
                l2.Add((L - Y[i], L - LX[i]));
            }
        }

        ans += F(u1, l1, r1);
        ans += F(u2, l2, r2);


        Console.WriteLine(ans);
    }

    // 下から生える
    // 左、右
    long F(List<(int x, int ry)> up, List<(int y, int rx)> left, List<(int y, int lx)> right)
    {
        up.Sort((l, r) => l.ry.CompareTo(r.ry));
        left.Sort((l, r) => l.y.CompareTo(r.y));
        right.Sort((l, r) => l.y.CompareTo(r.y));
        var lSt = new SegmentTree<long>(L + 1, (l, r) => l + r, 0);
        var rSt = new SegmentTree<long>(L + 1, (l, r) => l + r, 0);

        long ans = 0;
        int li = 0;
        int ri = 0;
        for (int i = 0; i < up.Count; i++)
        {
            // yがry以下
            while (li < left.Count && left[li].y <= up[i].ry)
            {
                lSt[left[li].rx]++;
                li++;
            }
            while (ri < right.Count && right[ri].y <= up[i].ry)
            {
                rSt[right[ri].lx]++;
                ri++;
            }

            // leftのx以上
            ans += lSt.Query(up[i].x, L + 2);
            // rightのx以下
            ans += rSt.Query(0, up[i].x + 1);
        }

        return ans;
    }

    public static void Main(string[] args) => new Program().Solve();
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
