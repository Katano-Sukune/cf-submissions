using CompLib.Util;
using System;
using System.Collections.Generic;

public class Program
{
    int N, M;
    int[] A;


    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        A = sc.IntArray();
        var st = new SegmentTree(A);
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        for (int i = 0; i < M; i++)
        {
            int t = sc.NextInt();
            int l = sc.NextInt() - 1;
            int r = sc.NextInt();
            if (t == 1)
            {
                st.Replace(l, r);
            }
            else if (t == 2)
            {
                Console.WriteLine(st.Sum(l, r));
            }
        }

        Console.Out.Flush();


    }

    public static void Main(string[] args) => new Program().Solve();
}

class SegmentTree
{
    // [約数個数,番号] = 個数

    const int Size = 1 << 19;
    const int MaxA = 1000000;
    readonly int[] D;

    long[] T;
    bool[] F;

    public SegmentTree(int[] a)
    {
        #region 約数個数
        // D(i)
        D = new int[MaxA + 1];

        for (int i = 1; i <= MaxA; i++)
        {
            for (int j = i; j <= MaxA; j += i)
            {
                D[j]++;
            }
        }
        #endregion

        #region T作る
        // 区間kの総和
        T = new long[2 * Size];
        // 区間kに2超過(操作して値が変わる)あるか
        F = new bool[2 * Size];

        for (int i = 0; i < a.Length; i++)
        {
            T[i + Size] = a[i];
            F[i + Size] = a[i] > 2;
        }

        for (int k = Size - 1; k >= 1; k--)
        {
            T[k] = T[k * 2] + T[k * 2 + 1];
            F[k] = F[k * 2] || F[k * 2 + 1];
        }
        #endregion
    }


    private void Replace(int left, int right, int k, int l, int r)
    {
        if (right <= l || r <= left)
        {
            // 範囲外 終了
            return;
        }

        if (!F[k])
        {
            // 操作しても変わらない 終了
            return;
        }

        if (r - l == 1)
        {
            // 操作
            T[k] = D[T[k]];
            F[k] = T[k] > 2;
            return;
        }

        Replace(left, right, k * 2, l, (l + r) / 2);
        Replace(left, right, k * 2 + 1, (l + r) / 2, r);
        T[k] = T[k * 2] + T[k * 2 + 1];
        F[k] = F[k * 2] || F[k * 2 + 1];
    }
    public void Replace(int l, int r)
    {
        Replace(l, r, 1, 0, Size);
    }

    private long Sum(int left, int right, int k, int l, int r)
    {
        if (left <= l && r <= right)
        {
            return T[k];
        }

        if (right <= l || r <= left)
        {
            return 0;
        }

        return Sum(left, right, k * 2, l, (l + r) / 2) + Sum(left, right, k * 2 + 1, (l + r) / 2, r);
    }
    public long Sum(int l, int r)
    {
        return Sum(l, r, 1, 0, Size);
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
