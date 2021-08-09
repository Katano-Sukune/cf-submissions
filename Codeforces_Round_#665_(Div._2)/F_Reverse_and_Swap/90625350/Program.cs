using CompLib.Collections.Generic;
using CompLib.Util;
using System;

public class Program
{
    int N, Q;
    int[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        Q = sc.NextInt();
        A = sc.IntArray();

        /*
         * 2^n配列 A
         * 
         * クエリq個
         * 
         * rep(x,y) a_x:= y セグ木
         * 
         * rev(k) 長さ2^kの連続部分列を反転
         * 反転フラグを持つ
         * 
         * 
         * swap 偶数番目の2^k列　奇数番目を反転
         * 
         * sum(l,r) [l,r]の総和
         */

        var st = new SegTree(N, A);

        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        for (int i = 0; i < Q; i++)
        {
            int t = sc.NextInt();
            if (t == 1)
            {
                int x = sc.NextInt();
                int k = sc.NextInt();
                st.Replace(x - 1, k);
            }
            else if (t == 2)
            {
                int k = sc.NextInt();
                st.Reverse(k);
            }
            else if (t == 3)
            {
                int k = sc.NextInt();
                st.Swap(k);
            }
            else
            {
                int l = sc.NextInt() - 1;
                int r = sc.NextInt();
                Console.WriteLine(st.Sum(l, r));
            }
        }
        Console.Out.Flush();

    }

    public static void Main(string[] args) => new Program().Solve();
}

class SegTree
{
    readonly int Size;
    readonly int N;
    long[] Node;
    RangeUpdateQuery<bool> Sw;
    RangeUpdateQuery<bool> Rev;

    public SegTree(int n, int[] a)
    {
        N = n;
        Size = 1 << n;
        Node = new long[2 * Size];
        Sw = new RangeUpdateQuery<bool>(2 * Size, (l, r) => l ^ r, false);
        Rev = new RangeUpdateQuery<bool>(2 * Size, (l, r) => l ^ r, false);

        for (int i = 0; i < Size; i++)
        {
            Node[i + Size] = a[i];
        }
        for (int i = Size - 1; i >= 1; i--)
        {
            Node[i] = Node[i * 2] + Node[i * 2 + 1];
        }
    }

    private void Eval(int k, int l, int r)
    {
        if (r - l <= 1) return;
        if (Rev[k])
        {
            Sw.Update(k, k + 1, true);
            Rev.Update(k, k + 1, true);
            Rev.Update(k * 2, k * 2 + 2, true);
        }
    }

    private long Q(int left, int right, int k, int l, int r)
    {
        Eval(k, l, r);
        if (left <= l && r <= right)
        {
            return Node[k];
        }
        if (right <= l || r <= left)
        {
            return 0;
        }
        int m = (l + r) / 2;
        return Q(left, right, k * 2, Sw[k] ? m : l, Sw[k] ? r : m) + Q(left, right, k * 2 + 1, Sw[k] ? l : m, Sw[k] ? m : r);
    }

    public long Sum(int l, int r)
    {
        return Q(l, r, 1, 0, Size);
    }

    public void Swap(int k)
    {
        // N-1 [1,2)
        // N-2 [2,4)
        // N-3 [4,8)
        int d = N - k;
        Sw.Update(1 << (d - 1), 1 << d, true);
    }

    public void Reverse(int k)
    {
        int d = N - k;
        Rev.Update(1 << d, 1 << (d + 1), true);
    }

    public void Replace(int x, int k, int t, int l, int r)
    {
        Eval(t, l, r);
        if (r - l == 1 && l == x)
        {
            Node[t] = k;
            return;
        }
        if (x + 1 <= l || r <= x)
        {
            return;
        }
        int m = (l + r) / 2;
        Replace(x, k, t * 2, Sw[t] ? m : l, Sw[t] ? r : m);
        Replace(x, k, t * 2 + 1, Sw[t] ? l : m, Sw[t] ? m : r);
        Node[t] = Node[t * 2] + Node[t * 2 + 1];
    }

    public void Replace(int x, int k)
    {
        Replace(x, k, 1, 0, Size);
    }
}


namespace CompLib.Collections.Generic
{
    using System;

    public class RangeUpdateQuery<T>
    {
        private readonly int N;
        private T[] _array;
        private readonly bool[] flag;

        private readonly T _identity;
        private readonly Func<T, T, T> _operation;

        /// <summary>
        /// 区間更新、点取得
        /// </summary>
        /// <param name="operation">更新用の演算</param>
        /// <param name="identity">(T, operation)の左単位元</param>
        public RangeUpdateQuery(int size, Func<T, T, T> operation, T identity)
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

            flag = new bool[N * 2];
        }

        private void Eval(int k, int l, int r)
        {
            if (flag[k])
            {
                if (r - l > 1)
                {
                    _array[k * 2] = _operation(_array[k * 2], _array[k]);
                    flag[k * 2] = true;
                    _array[k * 2 + 1] = _operation(_array[k * 2 + 1], _array[k]);
                    flag[k * 2 + 1] = true;
                    _array[k] = _identity;
                }

                flag[k] = false;
            }
        }

        private void Update(int left, int right, int k, int l, int r, T item)
        {
            Eval(k, l, r);
            if (r <= left || right <= l)
            {
                return;
            }

            if (left <= l && r <= right)
            {
                _array[k] = _operation(_array[k], item);
                flag[k] = true;
                Eval(k, l, r);
                return;
            }

            Update(left, right, k * 2, l, (l + r) / 2, item);
            Update(left, right, k * 2 + 1, (l + r) / 2, r, item);
        }

        /// <summary> 
        /// [left,right)をoperation(A[i],item)に更新
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="item"></param>
        public void Update(int left, int right, T item)
        {
            Update(left, right, 1, 0, N, item);
        }


        /// <summary>
        /// A[i]を取得 O(log N)
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public T Get(int i)
        {
            int l = 0;
            int r = N;
            int k = 1;
            while (r - l > 1)
            {
                Eval(k, l, r);
                int m = (l + r) / 2;
                if (i < m)
                {
                    r = m;
                    k = k * 2;
                }
                else
                {
                    l = m;
                    k = k * 2 + 1;
                }
            }

            return _array[k];
        }

        public T this[int i]
        {
            get { return Get(i); }
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
