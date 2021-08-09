using System;
using System.IO;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
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
        int z = sc.NextInt();
        int[] a = sc.IntArray();

        // 最初1にいてa_iスコア持ってる
        // xにいるときx+1,x-1に移動 スコアa_{x+1}, a_{x-1}加算
        // 連続して -1を2回できない
        // k回移動-1はz回　最高得点

        // i回移動、左j回 最後左移動か?
        var dp = new long[k + 1, z + 1, 2];
        for (int i = 0; i <= k; i++)
        {
            for (int j = 0; j <= z; j++)
            {
                for (int l = 0; l < 2; l++)
                {
                    dp[i, j, l] = long.MinValue;
                }
            }
        }

        dp[0, 0, 0] = a[0];
        for (int i = 0; i < k; i++)
        {
            for (int j = 0; j <= z; j++)
            {
                int cur = i - 2*j;
                if(cur < 0) continue;
                for (int l = 0; l < 2; l++)
                {
                    if (dp[i, j, l] == long.MinValue) continue;
                    // 右
                    dp[i + 1, j, 0] = Math.Max(dp[i + 1, j, 0], dp[i, j, l] + a[cur + 1]);
                    if(cur -1 < 0) continue;
                    if(l == 1) continue;
                    if(j+1 > z) continue;
                    dp[i + 1, j + 1, 1] = Math.Max(dp[i + 1, j + 1, 1], dp[i, j, l] + a[cur - 1]);
                }
            }
        }

        long ans = long.MinValue;
        for (int i = 0; i <= z; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                ans = Math.Max(ans, dp[k, i, j]);
            }
        }

        Console.WriteLine(ans);
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