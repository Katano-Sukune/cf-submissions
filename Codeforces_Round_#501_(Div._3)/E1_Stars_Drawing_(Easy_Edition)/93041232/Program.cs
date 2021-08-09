using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.Collections.Generic;

public class Program
{
    int N, M;
    char[][] S;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        S = new char[N][];
        for (int i = 0; i < N; i++)
        {
            S[i] = sc.NextCharArray();
        }

        int[,] u, d, l, r;
        u = new int[N, M];
        d = new int[N, M];
        l = new int[N, M];
        r = new int[N, M];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                int i2 = N - i - 1;
                int j2 = M - j - 1;
                if (S[i][j] == '*')
                {
                    u[i, j] = i == 0 ? 1 : u[i - 1, j] + 1;
                    l[i, j] = j == 0 ? 1 : l[i, j - 1] + 1;
                }
                if (S[i2][j] == '*')
                {
                    d[i2, j] = i == 0 ? 1 : d[i2 + 1, j] + 1;
                }
                if (S[i][j2] == '*')
                {
                    r[i, j2] = j == 0 ? 1 : r[i, j2 + 1] + 1;
                }

            }
        }

        int[,] imos = new int[N + 1, M + 1];
        List<(int x, int y, int s)> ls = new List<(int x, int y, int s)>();
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                int s = Math.Min(u[i, j], Math.Min(d[i, j], Math.Min(l[i, j], r[i, j]))) - 1;
                if (s >= 1)
                {
                    ls.Add((i + 1, j + 1, s));
                    imos[i - s, j]++;
                    imos[i - s, j + 1]--;
                    imos[i + s + 1, j]--;
                    imos[i + s + 1, j + 1]++;

                    imos[i, j - s]++;
                    imos[i + 1, j - s]--;
                    imos[i, j + s + 1]--;
                    imos[i + 1, j + s + 1]++;
                }
            }
        }

        for (int i = 0; i <= N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                imos[i, j + 1] += imos[i, j];
            }
        }
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j <= M; j++)
            {
                imos[i + 1, j] += imos[i, j];
            }
        }

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                if ((S[i][j] == '*') != (imos[i, j] > 0))
                {
                    Console.WriteLine("-1");
                    return;
                }
            }
        }
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        Console.WriteLine(ls.Count);
        foreach (var t in ls)
        {
            Console.WriteLine($"{t.x} {t.y} {t.s}");
        }
        Console.Out.Flush();
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
