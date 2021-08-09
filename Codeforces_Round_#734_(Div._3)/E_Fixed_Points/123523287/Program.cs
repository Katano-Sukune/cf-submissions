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
        int n = sc.NextInt();
        int k = sc.NextInt();
        int[] a = sc.IntArray();

        int[] last = new int[n + 1];
        Array.Fill(last, n);
        int[,] next = new int[n, n + 1];
        for (int i = n - 1; i >= 0; i--)
        {
            last[a[i]] = i;
            for (int j = 1; j <= n; j++)
            {
                next[i, j] = last[j];
            }
        }

        // i個一致
        // 長さ1~j
        // 達成できる最短
        var dp = new int[k + 1, n + 1];
        for (int i = 0; i <= k; i++)
        {
            for (int j = 0; j <= n; j++)
            {
                dp[i, j] = int.MaxValue;
            }
        }
        dp[0, 0] = 0;
        for (int i = 0; i < k; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (dp[i, j] >= n) continue;
                // iを無視
                dp[i, j + 1] = Math.Min(dp[i, j + 1], dp[i, j] + 1);
                // j+1を探す
                if (next[dp[i, j], j + 1] < n) dp[i + 1, j + 1] = Math.Min(dp[i + 1, j + 1], next[dp[i, j], j + 1] + 1);
            }
        }

        // if (n == 8)
        // {
        //     Console.WriteLine(dp[4, 5]);
        // }
        int ans = int.MaxValue;
        for (int i = 0; i <= n; i++)
        {
            if (dp[k, i] == int.MaxValue) continue;
            ans = Math.Min(ans, dp[k, i] - i);
        }

        if (ans == int.MaxValue)
        {
            Console.WriteLine("-1");
        }
        else
        {
            Console.WriteLine(ans);
        }
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