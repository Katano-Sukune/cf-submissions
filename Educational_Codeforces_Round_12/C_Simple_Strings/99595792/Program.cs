using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        string s = sc.Next();
        int n = s.Length;
        int[,] dp = new int[n, 26];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < 26; j++)
            {
                dp[i, j] = int.MaxValue;
            }
        }

        for (int i = 0; i < 26; i++)
        {
            dp[0, i] = s[0] - 'a' == i ? 0 : 1;
        }

        for (int i = 1; i < n; i++)
        {
            // 前
            for (int j = 0; j < 26; j++)
            {
                // 今
                for (int k = 0; k < 26; k++)
                {
                    if (j == k) continue;
                    dp[i, k] = Math.Min(dp[i, k], dp[i - 1, j] + (s[i] - 'a' == k ? 0 : 1));
                }
            }
        }

        int idx = -1;
        int min = int.MaxValue;
        for (int i = 0; i < 26; i++)
        {
            if (dp[n - 1, i] < min)
            {
                min = dp[n - 1, i];
                idx = i;
            }
        }

        char[] ans = new char[n];
        ans[n - 1] = (char) (idx + 'a');
        for (int i = n - 2; i >= 0; i--)
        {
            for (int j = 0; j < 26; j++)
            {
                if (idx == j) continue;
                if (dp[i, j] + (s[i + 1] - 'a' == idx ? 0 : 1) == dp[i + 1, idx])
                {
                    ans[i] = (char) (j + 'a');
                    idx = j;
                    break;
                }
            }
        }

        Console.WriteLine(new string(ans));
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