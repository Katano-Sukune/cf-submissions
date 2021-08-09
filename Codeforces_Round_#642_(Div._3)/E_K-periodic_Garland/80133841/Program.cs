using System;
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
            Console.WriteLine(Q(sc.NextInt(), sc.NextInt(), sc.Next()));
        }
        Console.Out.Flush();
    }

    string Q(int n, int k, string s)
    {
        // もともとついてるやつ
        int all = s.Count(c => c == '1');
        long ans = long.MaxValue;
        for (int i = 0; i < k; i++)
        {
            // 余りがi

            // l, r
            // l,rについてないやつ + 範囲外でついてるやつ 最小
            int len = i < n % k ? n / k + 1 : n / k;

            // 0 範囲外左、1範囲内、2範囲外右
            var dp = new long[len + 1, 3];
            for (int j = 0; j <= len; j++)
            {
                for (int l = 0; l < 3; l++)
                {
                    dp[j, l] = int.MaxValue;
                }
            }
            dp[0, 0] = 0;
            int cnt = 0;
            for (int j = 0; j < len; j++)
            {
                int index = j * k + i;
                if (s[index] == '1')
                {
                    cnt++;
                    dp[j + 1, 0] = Math.Min(dp[j + 1, 0], dp[j, 0] + 1);
                    dp[j + 1, 1] = Math.Min(dp[j + 1, 1], Math.Min(dp[j, 0], dp[j, 1]));
                    dp[j + 1, 2] = Math.Min(dp[j + 1, 2], Math.Min(dp[j, 2], dp[j, 1]) + 1);
                }
                else
                {
                    dp[j + 1, 0] = Math.Min(dp[j + 1, 0], dp[j, 0]);
                    dp[j + 1, 1] = Math.Min(dp[j + 1, 1], Math.Min(dp[j, 0], dp[j, 1]) + 1);
                    dp[j + 1, 2] = Math.Min(dp[j + 1, 2], Math.Min(dp[j, 2], dp[j, 1]));
                }
            }

            ans = Math.Min(ans, all - cnt + Math.Min(dp[len, 0], Math.Min(dp[len, 1], dp[len, 2])));
        }
        return ans.ToString();
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
