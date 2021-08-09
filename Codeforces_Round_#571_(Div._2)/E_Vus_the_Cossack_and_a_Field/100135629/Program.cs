using System;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Algorithm;

public class Program
{
    private long N, M;
    private int Q;
    private int[][] C;

    private long[][] Sum;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextLong();
        M = sc.NextLong();
        Q = sc.NextInt();
        C = new int[N][];
        for (int i = 0; i < N; i++)
        {
            C[i] = sc.Next().Select(ch => ch - '0').ToArray();
        }

        Sum = new long[N + 1][];
        Sum[0] = new long[M + 1];
        for (int i = 0; i < N; i++)
        {
            Sum[i + 1] = new long[M + 1];
            var ln = new long[M + 1];
            for (int j = 0; j < M; j++)
            {
                ln[j + 1] = ln[j] + C[i][j];
                Sum[i + 1][j + 1] = Sum[i][j + 1] + ln[j + 1];
            }
        }

        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        for (int i = 0; i < Q; i++)
        {
            long x1 = sc.NextLong() - 1;
            long y1 = sc.NextLong() - 1;

            long x2 = sc.NextLong();
            long y2 = sc.NextLong();

            Console.WriteLine(F(x2, y2) - F(x2, y1) - F(x1, y2) + F(x1, y1));
        }

        Console.Out.Flush();
    }

    long F(long x, long y)
    {
        checked
        {
            long ans = 0;

            long dx = x / N;
            long mx = x % N;

            long dy = y / M;
            long my = y % M;

            // 1001011001101001
            // 0110100110010110
            // 中
            if (dx % 2 == 0 || dy % 2 == 0)
            {
                ans += (dx * dy / 2) * N * M;
            }
            else
            {
                ans += ((dx * dy - 1) / 2) * N * M;
                if (G(dx - 1, dy - 1)) ans += N * M - Sum[N][M];
                else ans += Sum[N][M];
            }

            // 縁
            ans += (dx / 2) * N * my;
            if (dx % 2 == 1)
            {
                if (G(dx - 1, dy)) ans += N * my - Sum[N][my];
                else ans += Sum[N][my];
            }

            ans += (dy / 2) * mx * M;
            if (dy % 2 == 1)
            {
                if (G(dx, dy - 1)) ans += mx * M - Sum[mx][M];
                else ans += Sum[mx][M];
            }

            // 角
            if (G(dx, dy)) ans += mx * my - Sum[mx][my];
            else ans += Sum[mx][my];

            return ans;
        }
    }

    // (x,y)のやつは反転してるか?
    bool G(long x, long y)
    {
        return (Algorithm.BitCount(x) + Algorithm.BitCount(y)) % 2 == 1;
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

namespace CompLib.Algorithm
{
    static class Algorithm
    {
        /// <summary>
        /// nの立っているbitの数
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int BitCount(long n)
        {
            n = (n & 0x5555555555555555) + (n >> 1 & 0x5555555555555555);
            n = (n & 0x3333333333333333) + (n >> 2 & 0x3333333333333333);
            n = (n & 0x0f0f0f0f0f0f0f0f) + (n >> 4 & 0x0f0f0f0f0f0f0f0f);
            n = (n & 0x00ff00ff00ff00ff) + (n >> 8 & 0x00ff00ff00ff00ff);
            n = (n & 0x0000ffff0000ffff) + (n >> 16 & 0x0000ffff0000ffff);
            return (int) ((n & 0x00000000ffffffff) + (n >> 32 & 0x00000000ffffffff));
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