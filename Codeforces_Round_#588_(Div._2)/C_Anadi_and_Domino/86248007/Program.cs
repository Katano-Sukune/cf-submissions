using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    private int N, M;
    private int[] A, B;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        A = new int[M];
        B = new int[M];

        for (int i = 0; i < M; i++)
        {
            A[i] = sc.NextInt() - 1;
            B[i] = sc.NextInt() - 1;
        }

        /*
         * ドミノ 2つの1~6の数字が書いてある 21種類
         * グラフ
         * 辺にドミノを置く
         * ある頂点に向かっている数字は同じ
         *
         * 何種類のドミノを置けるか?
         */
        Node = new int[N];
        Console.WriteLine(Go(0));
    }

    private int[] Node;

    int Go(int i)
    {
        if (i >= N)
        {
            // Console.WriteLine(string.Join(" ", Node));
            var hs = new HashSet<S>();
            for (int j = 0; j < M; j++)
            {
                hs.Add(new S(Node[A[j]], Node[B[j]]));
            }

            return hs.Count;
        }

        int max = 0;
        for (int j = 1; j <= 6; j++)
        {
            Node[i] = j;

            max = Math.Max(max, Go(i + 1));
        }


        return max;
    }

    public static void Main(string[] args) => new Program().Solve();
}

struct S
{
    public int X, Y;

    public S(int x, int y)
    {
        if (x > y)
        {
            x ^= y;
            y ^= x;
            x ^= y;
        }

        X = x;
        Y = y;
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