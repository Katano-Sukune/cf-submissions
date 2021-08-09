using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Collections;

public class Program
{
    private List<(int X1, int X2, int Y)> H;
    private List<(int Y1, int Y2, int X)> V;

    private int N;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();

        H = new List<(int X1, int X2, int Y)>(N);
        V = new List<(int Y1, int Y2, int X)>(N);
        for (int i = 0; i < N; i++)
        {
            int x1 = sc.NextInt();
            int y1 = sc.NextInt();
            int x2 = sc.NextInt();
            int y2 = sc.NextInt();

            if (y1 == y2)
            {
                if (x1 > x2) (x1, x2) = (x2, x1);
                H.Add((x1, x2, y1));
            }

            else if (x1 == x2)
            {
                if (y1 > y2) (y1, y2) = (y2, y1);
                V.Add((y1, y2, x1));
            }
        }

        H.Sort((l, r) => l.Y.CompareTo(r.Y));

        V.Sort((l, r) => r.Y2.CompareTo(l.Y2));

        long ans = 0;
        for (int h1 = 0; h1 < H.Count; h1++)
        {
            
            var ft = new FenwickTree(10001);
            int ptr = 0;
            for (int h2 = H.Count - 1; h2 > h1; h2--)
            {
                while (ptr < V.Count && V[ptr].Y2 >= H[h2].Y)
                {
                    if (V[ptr].Y1 <= H[h1].Y) ft.Add(V[ptr].X + 5000, 1);
                    ptr++;
                }

                int min = Math.Max(H[h1].X1, H[h2].X1);
                int max = Math.Min(H[h1].X2, H[h2].X2);

                if (min > max) continue;

                int cnt = ft.Sum(min + 5000, max + 5000 + 1);
                ans += (cnt - 1) * cnt / 2;
            }
        }

        Console.WriteLine(ans);
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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
        public int LowerBound(Num w)
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