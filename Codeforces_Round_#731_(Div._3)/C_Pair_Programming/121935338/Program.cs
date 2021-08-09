using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
#if !DEBUG
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
#endif
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int k = sc.NextInt();
        int n = sc.NextInt();
        int m = sc.NextInt();
        int[] a = sc.IntArray();
        int[] b = sc.IntArray();

        int[,] dp = new int[n + 1, m + 1];
        for (int i = 0; i <= n; i++)
        {
            for (int j = 0; j <= m; j++)
            {
                dp[i, j] = int.MinValue;
            }
        }

        dp[0, 0] = k;
        for (int i = 0; i <= n; i++)
        {
            for (int j = 0; j <= m; j++)
            {
                if (dp[i, j] == int.MinValue) continue;
                if (i < n)
                {
                    if (a[i] == 0)
                    {
                        dp[i + 1, j] = Math.Max(dp[i + 1, j], dp[i, j] + 1);
                    }
                    else if (dp[i, j] >= a[i])
                    {
                        dp[i + 1, j] = Math.Max(dp[i + 1, j], dp[i, j]);
                    }
                }

                if (j < m)
                {
                    if (b[j] == 0)
                    {
                        dp[i, j + 1] = Math.Max(dp[i, j + 1], dp[i, j] + 1);
                    }
                    else if (dp[i, j] >= b[j])
                    {
                        dp[i, j + 1] = Math.Max(dp[i, j + 1], dp[i, j]);
                    }
                }
            }
        }

        if (dp[n, m] == int.MinValue)
        {
            Console.WriteLine("-1");
            return;
        }

        int[] ans = new int[n + m];
        int nn = n;
        int mm = m;
        for (int i = n + m; i > 0; i--)
        {
            if (nn > 0)
            {
                if (a[nn - 1] == 0)
                {
                    if (dp[nn - 1, mm] + 1 == dp[nn, mm])
                    {
                        ans[i - 1] = 0;
                        nn--;
                        continue;
                    }
                }
                else
                {
                    if (dp[nn - 1, mm] >= a[nn - 1])
                    {
                        ans[i - 1] = a[nn - 1];
                        nn--;
                        continue;
                    }
                }
            }
            if (mm > 0)
            {
                if (b[mm - 1] == 0)
                {
                    if (dp[nn, mm - 1] + 1 == dp[nn, mm])
                    {
                        ans[i - 1] = 0;
                        mm--;
                        continue;
                    }
                }
                else
                {
                    if (dp[nn, mm - 1] >= b[mm - 1])
                    {
                        ans[i - 1] = b[mm - 1];
                        mm--;
                        continue;
                    }
                }
            }
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