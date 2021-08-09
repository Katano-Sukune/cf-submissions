using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, Sx, Sy;
    private (int X, int Y)[] P;

    private int C = int.MinValue;
    private int Px = -1, Py = -1;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        Sx = sc.NextInt();
        Sy = sc.NextInt();
        P = new (int X, int Y)[N];
        for (int i = 0; i < N; i++)
        {
            P[i] = (sc.NextInt(), sc.NextInt());
        }

        /*
         * sx,syに学校
         * iの家はP_i
         *
         * 学校から家までの最短経路に
         */
        int x = Sx;
        int y = Sy;
        int[] dx = new int[] {0, -1, 0, 1};
        int[] dy = new int[] {1, 0, -1, 0};
        for (int i = 0; i < 4; i++)
        {
            int res = Calc();
            if (C < res)
            {
                Px = x + dx[i];
                Py = y + dy[i];
                C = res;
            }

            for (int j = 0; j < N; j++)
            {
                P[j] = (P[j].Y, -P[j].X);
            }

            (Sx, Sy) = (Sy, -Sx);
        }

        Console.WriteLine(C);
        Console.WriteLine($"{Px} {Py}");
    }

    int Calc()
    {
        // 上
        int cnt1 = 0;
        foreach ((int X, int Y) in P)
        {
            if (Sy + 1 <= Y) cnt1++;
        }

        return cnt1;
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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