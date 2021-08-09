using System;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int k = sc.NextInt();
        int[] a = sc.IntArray();
        Array.Sort(a);
        // iまで見る　チーム数 参加者最大
        int[,] dp = new int[n + 1, k + 1];
        for (int i = 0; i <= n; i++)
        {
            for (int j = 0; j <= k; j++)
            {
                dp[i, j] = int.MinValue;
            }
        }

        dp[0, 0] = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j <= i && j <= k; j++)
            {
                if (dp[i, j] == int.MinValue) continue;
                // iはチームに入れない
                dp[i + 1, j] = Math.Max(dp[i + 1, j], dp[i, j]);
                if (j + 1 <= k)
                {
                    // iがチーム最小
                    int ok = i;
                    int ng = n;
                    while (ng - ok > 1)
                    {
                        int mid = (ok + ng) / 2;
                        if (a[mid] <= a[i] + 5) ok = mid;
                        else ng = mid;
                    }

                    dp[ng, j + 1] = Math.Max(dp[ng, j + 1], dp[i, j] + ng - i);
                }
            }
        }

        int ans = int.MinValue;
        for (int i = 0; i <= k; i++)
        {

            ans = Math.Max(ans, dp[n,i]);
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
