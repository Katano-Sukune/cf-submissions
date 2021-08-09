using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int[] a = new int[n + 2];
        for (int i = 1; i <= n; i++)
        {
            a[i] = sc.NextInt();
        }

        int t = (n + 1) / 2;

        // iまで見る
        // 1つ前、2つ前を丘にするために減らしたか?
        // k個丘
        // 最小

        var dp = new long[n + 3, 2, t + 1];
        for (int i = 0; i <= n + 2; i++)
        {
            for (int j = 0; j <= 1; j++)
            {
                for (int k = 0; k <= t; k++)
                {
                    dp[i, j, k] = long.MaxValue;
                }
            }
        }

        dp[1, 0, 0] = 0;
        for (int i = 1; i <= n; i++)
        {
            for (int j = 0; j <= 1; j++)
            {
                for (int k = 0; k <= t; k++)
                {
                    if (dp[i, j, k] == long.MaxValue) continue;
                    // iを丘にしない
                    dp[i + 1, 0, k] = Math.Min(dp[i + 1, 0, k], dp[i, j, k]);

                    int prev = j == 0 ? a[i - 1] : a[i - 2] - 1;

                    long cost = Math.Max(0, prev - (a[i] - 1)) + Math.Max(0, a[i + 1] - (a[i] - 1));
                    int nJ = a[i + 1] >= a[i] ? 1 : 0;
                    dp[i + 2, nJ, k + 1] = Math.Min(dp[i + 2, nJ, k + 1], dp[i, j, k] + cost);
                }
            }
        }

        for (int j = 0; j <= 1; j++)
        {
            for (int k = 0; k <= t; k++)
            {
                dp[n + 2, 0, k] = Math.Min(dp[n + 2, 0, k], dp[n + 1, j, k]);
            }
        }

        long[] ans = new long[t];
        for (int i = 0; i < t; i++)
        {
            ans[i] = dp[n + 2, 0, i + 1];
        }

        Console.WriteLine(string.Join(" ", ans));
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