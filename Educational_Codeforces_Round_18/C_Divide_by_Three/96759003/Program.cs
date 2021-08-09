using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private string N;
    private int L;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.Next();
        L = N.Length;

        /*
         * Nからいくつか消して
         * 
         * 先頭が非0
         * 1桁以上
         * 3の倍数
         *
         * にする
         */

        // 上位i桁、mod 3
        // 使った桁数最大
        int[,] dp = new int[L + 1, 3];
        for (int i = 0; i <= L; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                dp[i, j] = int.MinValue;
            }
        }

        dp[0, 0] = 0;
        for (int i = 0; i < L; i++)
        {
            int c = N[i] - '0';
            for (int j = 0; j < 3; j++)
            {
                int cur = dp[i, j];
                if (cur == int.MaxValue) continue;
                // 使わない
                dp[i + 1, j] = Math.Max(dp[i + 1, j], cur);
                // 使う
                if (c != 0 || cur != 0)
                {
                    dp[i + 1, (j + c) % 3] = Math.Max(dp[i + 1, (j + c) % 3], cur + 1);
                }
            }
        }

        if (dp[L, 0] == int.MinValue || dp[L, 0] == 0)
        {
            Console.WriteLine(N.Contains('0') ? "0" : "-1");
            return;
        }

        List<char> ans = new List<char>();
        int p = 0;
        for (int i = L - 1; i >= 0; i--)
        {
            int num = N[i] - '0';
            int prev = (p - num) % 3;
            if (prev < 0) prev += 3;

            if (dp[i, prev] + 1 == dp[i + 1, p])
            {
                p = prev;
                ans.Add(N[i]);
            }
        }

        ans.Reverse();
        Console.WriteLine(new string(ans.ToArray()));
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