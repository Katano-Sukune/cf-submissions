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
    const int MaxF = 6;
    readonly int[] D;

    // 区間kにi回操作したときの和
    Array2D<long> T;

    int[] F;
    int[] G;
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
        T = new Array2D<long>(Size * 2, MaxF + 1);

        for (int i = 0; i < a.Length; i++)
        {
            int tmp = a[i];
            for (int j = 0; j <= MaxF; j++)
            {
                T[i + Size, j] = tmp;
                tmp = D[tmp];
            }
        }

        for (int i = Size - 1; i >= 1; i--)
        {
            for (int j = 0; j <= MaxF; j++)
            {
                T[i, j] += T[i * 2, j] + T[i * 2 + 1, j];
            }
        }
        #endregion

        // kはF[k]回未操作
        F = new int[2 * Size];

        G = new int[2 * Size];
        for (int i = 1; i < 2 * Size; i++)
        {
            G[i] = MaxF;
        }
    }

    void Eval(int k, int l, int r)
    {
        if (F[k] > 0)
        {
            // T[k]の先頭 F[k]個消す
            for (int i = 0; i < G[k]; i++)
            {
                T[k, i] = T[k, Math.Min(G[k], i + F[k])];
            }
            if (r - l >= 2)
            {
                F[k * 2] += F[k];
                F[k * 2 + 1] += F[k];
            }
            G[k] = Math.Max(1, G[k] - F[k]);
            F[k] = 0;
        }
    }


    private void Replace(int left, int right, int k, int l, int r)
    {

        if (left <= l && r <= right)
        {
            F[k]++;
            Eval(k, l, r);
            return;
        }

        Eval(k, l, r);
        if (right <= l || r <= left)
        {
            return;
        }
        Replace(left, right, k * 2, l, (l + r) / 2);
        Replace(left, right, k * 2 + 1, (l + r) / 2, r);

        for (int i = 0; i <= MaxF; i++)
        {
            T[k, i] = T[k * 2, i] + T[k * 2 + 1, i];
        }
    }
    public void Replace(int l, int r)
    {
        Replace(l, r, 1, 0, Size);
    }

    private long Sum(int left, int right, int k, int l, int r)
    {
        Eval(k, l, r);
        if (left <= l && r <= right)
        {
            return T[k, 0];
        }

        if (right <= l || r <= left)
        {
            return 0;
        }

        long tmpL = Sum(left, right, k * 2, l, (l + r) / 2);
        long tmpR = Sum(left, right, k * 2 + 1, (l + r) / 2, r);

        for (int i = 0; i <= MaxF; i++)
        {
            T[k, i] = T[k * 2, i] + T[k * 2 + 1, i];
        }

        return tmpL + tmpR;
    }
    public long Sum(int l, int r)
    {
        return Sum(l, r, 1, 0, Size);
    }
}

class Array2D<T>
{
    public readonly int R, C;
    private readonly T[] Table;
    public Array2D(int r, int c)
    {
        R = r;
        C = c;
        Table = new T[R * C];
    }

    public new T this[int i, int j]
    {
        get
        {
            return Table[i * C + j];
        }
        set
        {
            Table[i * C + j] = value;
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
