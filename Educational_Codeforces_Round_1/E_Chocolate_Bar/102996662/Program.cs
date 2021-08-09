using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;

public class Program
{
    long[,,] DP;

    public void Solve()
    {
        DP = new long[31, 31, 51];
        for (int i = 0; i <= 30; i++)
        {
            for (int j = 0; j <= 30; j++)
            {
                for (int k = 0; k <= 50; k++)
                {
                    DP[i, j, k] = int.MaxValue;
                }
            }
        }

        for (int i = 0; i <= 30; i++)
        {
            for (int j = 0; j <= 30; j++)
            {
                for (int k = 0; k <= Math.Min(i * j, 50); k++)
                {
                    Go(i, j, k);
                }
            }
        }

        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
        Console.Out.Flush();
    }

    void Go(int n, int m, int k)
    {
        if (DP[n, m, k] != int.MaxValue) return;
        if (n * m == k || k == 0)
        {
            DP[n, m, k] = 0;
            return;
        }

        // よこ
        for (int i = 1; i <= n - 1; i++)
        {
            for (int nk = 0; nk <= k; nk++)
            {
                if (i * m < nk) continue;
                if ((n - i) * m < k - nk) continue;
                Go(i, m, nk);
                Go(n - i, m, k - nk);

                DP[n, m, k] = Math.Min(DP[n, m, k], m * m + DP[i, m, nk] + DP[n - i, m, k - nk]);
            }
        }

        for (int j = 1; j <= m - 1; j++)
        {
            for (int nk = 0; nk <= k; nk++)
            {
                if (n * j < nk) continue;
                if (n * (m - j) < k - nk) continue;
                Go(n, j, nk);
                Go(n, m - j, k - nk);

                DP[n, m, k] = Math.Min(DP[n, m, k], n * n + DP[n, j, nk] + DP[n, m - j, k - nk]);
            }
        }
    }

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        int m = sc.NextInt();
        int k = sc.NextInt();

        Console.WriteLine(DP[n, m, k]);
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
