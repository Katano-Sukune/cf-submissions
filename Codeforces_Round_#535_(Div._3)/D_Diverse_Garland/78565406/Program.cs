using CompLib.Util;
using System;
using System.Linq;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        string s = sc.Next();
        // i番目、最後の色
        string rgb = "RGB";
        var dp = new int[n, 3];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                dp[i, j] = int.MaxValue;
            }
        }
        for (int i = 0; i < 3; i++)
        {
            dp[0, i] = s[0] == rgb[i] ? 0 : 1;
        }

        for (int i = 1; i < n; i++)
        {
            // 前
            for (int j = 0; j < 3; j++)
            {
                // 今
                for (int k = 0; k < 3; k++)
                {
                    if (j == k) continue;
                    dp[i, k] = Math.Min(dp[i, k], dp[i - 1, j] + (s[i] == rgb[k] ? 0 : 1));
                }
            }
        }

        int r = Math.Min(dp[n - 1, 0], Math.Min(dp[n - 1, 1], dp[n - 1, 2]));
        var t = new char[n];
        int p = -1;
        for (int i = 0; i < 3; i++) if (dp[n - 1, i] == r) p = i;
        t[n - 1] = rgb[p];
        for (int i = n - 2; i >= 0; i--)
        {
            int prev = -1;
            for (int j = 0; j < 3; j++)
            {
                if (p == j) continue;
                if (dp[i, j] + (s[i + 1] == rgb[p] ? 0 : 1) == dp[i + 1, p]) prev = j;
            }
            p = prev;
            t[i] = rgb[p];
        }
        Console.WriteLine(r);
        Console.WriteLine(new string(t));
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
