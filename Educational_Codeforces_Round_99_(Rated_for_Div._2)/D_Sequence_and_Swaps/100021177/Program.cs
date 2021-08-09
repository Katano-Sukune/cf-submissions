using System;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
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
        int x = sc.NextInt();
        int[] a = sc.IntArray();

        // x超過の要素を選んでxと入れ替える
        const int Max = 500;
        var dp = new int[n, Max + 1, Max + 1];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j <= Max; j++)
            {
                for (int k = 0; k <= Max; k++)
                {
                    dp[i, j, k] = int.MaxValue;
                }
            }
        }

        dp[0, a[0], x] = 0;
        if (a[0] > x) dp[0, x, a[0]] = 1;
        for (int i = 1; i < n; i++)
        {
            // そのまま追加
            for (int j = 0; j <= a[i]; j++)
            {
                for (int k = 0; k <= Max; k++)
                {
                    if (dp[i - 1, j, k] == int.MaxValue) continue;
                    dp[i, a[i], k] = Math.Min(dp[i, a[i], k], dp[i - 1, j, k]);
                }
            }

            // xと入れ替え
            for (int j = 0; j <= Max; j++)
            {
                for (int k = j; k < a[i]; k++)
                {
                    if (dp[i - 1, j, k] == int.MaxValue) continue;
                    dp[i, k, a[i]] = Math.Min(dp[i, k, a[i]], dp[i - 1, j, k] + 1);
                }
            }
        }

        int ans = int.MaxValue;
        for (int i = 0; i <= Max; i++)
        {
            for (int j = 0; j <= Max; j++)
            {
                ans = Math.Min(ans, dp[n - 1, i, j]);
            }
        }

        if (ans == int.MaxValue)
        {
            Console.WriteLine("-1");
            return;
        }

        Console.WriteLine(ans);
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