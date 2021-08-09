using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        /*
         * a,b文字列 s 長さn
         *
         * 
         * tを唱える
         * 抵抗 tとsの編集距離
         *
         * n+2回まで
         *
         * s
         */

        // ? a
        // ? b
        // 同じ n -1
        // 違う 小さい方 長さ n+1 全部一致
        int b = GetScore("b");
        if (b == 0)
        {
            return;
        }


        int a = GetScore(new string('a', b));
        if (a == 0)
        {
            return;
        }

        int n = b + 1;

        var ans = new char[n];
        Array.Fill(ans, 'a');
        int best = a;
        for (int i = 0; i < n && best > 0; i++)
        {
            ans[i] = 'b';
            int res = GetScore(new string(ans));
            if (res < best)
            {
                best = res;
            }
            else
            {
                ans[i] = 'a';
            }
        }
    }

    int GetScore(string s)
    {
#if DEBUG
        int score = Score(s, ans);

        Console.WriteLine($"{s}:{score}");
        return score;
#else
        Console.WriteLine(s);
        return int.Parse(Console.ReadLine());
#endif
    }


    private const string ans = "ababab";

    int Score(string s, string t)
    {
        int[,] dp = new int[s.Length + 1, t.Length + 1];
        for (int i = 0; i <= s.Length; i++)
        {
            for (int j = 0; j <= t.Length; j++)
            {
                dp[i, j] = int.MaxValue - 1;
            }
        }

        for (int i = 0; i <= s.Length; i++)
        {
            dp[i, 0] = i;
        }

        for (int i = 0; i <= t.Length; i++)
        {
            dp[0, i] = i;
        }

        for (int i = 1; i <= s.Length; i++)
        {
            for (int j = 1; j <= t.Length; j++)
            {
                if (s[i - 1] == t[j - 1])
                {
                    dp[i, j] = Math.Min(dp[i, j], dp[i - 1, j - 1]);
                }
                else
                {
                    dp[i, j] = Math.Min(dp[i, j], dp[i - 1, j - 1] + 1);
                }

                dp[i, j] = Math.Min(dp[i, j], dp[i - 1, j] + 1);
                dp[i, j] = Math.Min(dp[i, j], dp[i, j - 1] + 1);
            }
        }

        return dp[s.Length, t.Length];
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