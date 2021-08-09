using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        for (int i = 0; i < t; i++)
        {
            int n = sc.NextInt();
            int m = sc.NextInt();
            string[] a = new string[n];
            for (int j = 0; j < n; j++)
            {
                a[j] = sc.Next();
            }
            Console.WriteLine(Q(n, m, a));
        }
        Console.Out.Flush();
    }

    string Q(int n, int m, string[] a)
    {
        // i文字目、最後の文字
        char[,] dp = new char[m, 1 << n];
        for (char c = 'a'; c <= 'z'; c++)
        {
            int tmp = 0;
            for (int i = 0; i < n; i++)
            {
                if (a[i][0] != c) tmp |= 1 << i;
            }
            dp[0, tmp] = c;
        }

        for (int i = 1; i < m; i++)
        {
            for (char c = 'a'; c <= 'z'; c++)
            {
                int tmp = 0;
                for (int j = 0; j < n; j++)
                {
                    if (a[j][i] != c) tmp |= 1 << j;
                }

                for (int j = 0; j < (1 << n); j++)
                {
                    if ((tmp & j) > 0) continue;
                    if (dp[i - 1, j] == default(char)) continue;
                    dp[i, j | tmp] = c;
                }
            }
        }

        int t = -1;
        for (int i = 0; i < (1 << n); i++)
        {
            if (dp[m - 1, i] != default(char))
            {
                t = i;
                break;
            }
        }
        if (t == -1) return "-1";

        char[] ans = new char[m];
        ans[m - 1] = dp[m - 1, t];
        for (int i = m - 1; i >= 1; i--)
        {
            char c = dp[i, t];
            int tmp = 0;
            for (int j = 0; j < n; j++)
            {
                if (a[j][i] != c) tmp |= 1 << j;
            }

            t -= tmp;

            ans[i - 1] = dp[i - 1, t];
        }

        return new string(ans);
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
