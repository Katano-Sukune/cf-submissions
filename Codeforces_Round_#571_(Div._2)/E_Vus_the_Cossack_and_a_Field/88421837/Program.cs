using System;
using System.Text;
using CompLib.Algorithm;
using CompLib.Util;

public class Program
{
    private long N, M;
    private int Q;
    private char[][] C;
    private long[,] T;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        Q = sc.NextInt();
        C = new char[N][];
        for (int i = 0; i < N; i++)
        {
            C[i] = sc.NextCharArray();
        }

        T = new long[N + 1, M + 1];
        for (int i = 0; i < N; i++)
        {
            var ln = new long[M + 1];
            for (int j = 0; j < M; j++)
            {
                ln[j + 1] = ln[j] + (C[i][j] - '0');
                T[i + 1, j + 1] = T[i, j + 1] + ln[j + 1];
            }
        }

        var sb = new StringBuilder();
        for (int i = 0; i < Q; i++)
        {
            sb.AppendLine(Query(sc.NextInt() - 1, sc.NextInt() - 1, sc.NextInt(), sc.NextInt()).ToString());
        }
        
        Console.Write(sb);
    }

    long Query(int x1, int y1, int x2, int y2)
    {
        return Sum(x2, y2) - Sum(x1, y2) - Sum(x2, y1) + Sum(x1, y1);
    }

    long Sum(int x, int y)
    {
        long dx = x / N;
        long dy = y / M;

        long mx = x % N;
        long my = y % M;

        long ans = 0;

        // Cはdx*dy個
        {
            ans += (dx * dy / 2) * N * M;
            if ((dx * dy) % 2 != 0)
            {
                ans += S(N, M, I(dx - 1, dy - 1));
            }
        }

        {
            // 縦mx、横M dy個
            ans += (dy / 2) * mx * M;
            if (dy % 2 != 0)
            {
                ans += S(mx, M, I(dx, dy - 1));
            }
        }

        {
            // 縦N 横my dx個
            ans += (dx / 2) * N * my;
            if (dx % 2 != 0)
            {
                ans += S(N, my, I(dx - 1, dy));
            }
        }

        {
            // 縦mx 横my 1個
            ans += S(mx, my, I(dx, dy));
        }

        return ans;
    }

    bool I(long x, long y)
    {
        return (Algorithm.BitCount(x) + Algorithm.BitCount(y)) % 2 == 1;
    }

    long S(long x, long y, bool inverse)
    {
        if (inverse)
        {
            return x * y - T[x, y];
        }
        else
        {
            return T[x, y];
        }
    }

    public static void Main(string[] args) => new Program().Solve();
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