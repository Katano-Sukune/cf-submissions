using System;
using System.IO;
using System.Linq;
using System.Text;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, M;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();

        /*
         * N*Mマス
         *
         * (1,1)にいる
         *
         * (x,y) から (x+dx, y+dy)に行ける
         *
         * (dx,dy)は1回だけ使える
         *
         * 全部通れるか?
         */

        var sb = new StringBuilder();
        for (int i = 0; i < N / 2; i++)
        {
            for (int j = 0; j < M; j++)
            {
                int r1 = i;
                int c1 = j;
                sb.AppendLine($"{r1 + 1} {c1 + 1}");
                int r2 = N - i - 1;
                int c2 = M - j - 1;
                sb.AppendLine($"{r2 + 1} {c2 + 1}");
            }
        }

        if (N % 2 == 1)
        {
            int r = N / 2;
            for (int i = 0; i < M; i++)
            {
                int c = i % 2 == 0 ? i / 2 : M - i / 2 - 1;
                sb.AppendLine($"{r+1} {c+1}");
            }
        }

        Console.Write(sb);
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