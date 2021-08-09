using System;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < t; i++)
        {
            sb.AppendLine(Q(sc.NextInt(), sc.IntArray()));
        }

        Console.Write(sb.ToString());
    }

    string Q(int n, int[] t)
    {
        var dp = new int[n, 3, 3];
        var prev = new int[n, 3, 3];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    dp[i, j, k] = int.MaxValue;
                    prev[i, j, k] = -1;
                }
            }
        }

        dp[0, 0, 0] = 0;
        dp[0, 1, 1] = 1;
        dp[0, 2, 2] = 2;
        for (int i = 1; i < n; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    for (int l = 0; l < 3; l++)
                    {
                        if (t[i - 1] != t[i] && j == k) continue;
                        if (dp[i - 1, j, l] == int.MaxValue) continue;
                        if (i == n - 1 && t[n - 1] != t[0] && l == k) continue;
                        if (Math.Max(dp[i - 1, j, l], k) < dp[i, k, l])
                        {
                            dp[i, k, l] = Math.Max(dp[i - 1, j, l], k);
                            prev[i, k, l] = j;
                        }
                    }
                }
            }
        }


        int f = 0;
        int p = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (dp[n - 1, i, j] < dp[n - 1, p, f])
                {
                    p = i;
                    f = j;
                }
            }
        }

        // Console.WriteLine($"{p} {f}");
        int kk = dp[n - 1, p, f] + 1;
        var ans = new int[n];
        for (int i = n - 1; i >= 0; i--)
        {
            ans[i] = p+1;
            if (i > 0) p = prev[i, p, f];
        }

        return $"{kk}\n{string.Join(" ", ans)}";
    }

    public static void Main(string[] args) => new Program().Solve();
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
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(Separator);
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
            _line = Console.ReadLine().Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}