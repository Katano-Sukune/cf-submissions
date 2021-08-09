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
        int[,] dist = new int[n, n];
        for (int i = 0; i < n; i++)
        {
            string s = sc.Next();
            for (int j = 0; j < n; j++)
            {
                dist[i, j] = s[j] == '1' ? 1 : (int) 1e9;
            }

            dist[i, i] = 0;
        }

        for (int k = 0; k < n; k++)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    dist[i, j] = Math.Min(dist[i, j], dist[i, k] + dist[k, j]);
                }
            }
        }

        int m = sc.NextInt();
        int[] p = new int[m];
        for (int i = 0; i < m; i++)
        {
            p[i] = sc.NextInt() - 1;
        }


        var dp = new int[m];
        var prev = new int[m];
        Array.Fill(dp, int.MaxValue);
        Array.Fill(prev, -1);
        dp[0] = 1;

        for (int i = 1; i < m; i++)
        {
            for (int j = i - 1; j >= 0; j--)
            {
                if (i - j == dist[p[j], p[i]])
                {
                    if (dp[j] + 1 < dp[i])
                    {
                        dp[i] = dp[j] + 1;
                        prev[i] = j;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        int kk = dp[m - 1];
        int[] ans = new int[kk];
        ans[kk - 1] = p[m - 1] + 1;
        int idx = m - 1;
        for (int i = kk - 2; i >= 0; i--)
        {
            idx = prev[idx];
            ans[i] = p[idx] + 1;
        }

        Console.WriteLine(kk);
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