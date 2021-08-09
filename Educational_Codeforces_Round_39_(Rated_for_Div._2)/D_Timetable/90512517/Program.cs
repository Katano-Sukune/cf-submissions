using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N, M, K;
    string[] S;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        K = sc.NextInt();
        S = new string[N];
        for (int i = 0; i < N; i++)
        {
            S[i] = sc.Next();
        }

        /*
         * n日
         * 
         * 1日m時間
         * 
         * 最初の時間i 最後j
         * j-i+1時間過ごす
         * 
         * 授業が無いなら0時間
         * 
         * k以下サボる
         * 
         * 過ごす時間最小
         */

        // i日目、K時間サボった 最小
        var dp = new long[N + 1, K + 1];
        for (int i = 0; i <= N; i++)
        {
            for (int j = 0; j <= K; j++)
            {
                dp[i, j] = long.MaxValue;
            }
        }

        dp[0, 0] = 0;

        for (int i = 0; i < N; i++)
        {
            // 授業
            var ls = new List<int>();
            for (int j = 0; j < M; j++)
            {
                if (S[i][j] == '1') ls.Add(j);
            }

            // j時間サボったとき時間
            var t = new long[ls.Count + 1];
            for (int j = 0; j < ls.Count; j++)
            {
                t[j] = long.MaxValue;
            }
            for (int j = 0; j < ls.Count; j++)
            {
                // 出る時間
                int tt = ls.Count - j;
                for (int b = 0; b + tt <= ls.Count; b++)
                {
                    t[j] = Math.Min(t[j], ls[b + tt - 1] - ls[b] + 1);
                }
            }

            for (int j = 0; j <= K; j++)
            {
                if (dp[i, j] == long.MaxValue) continue;
                for (int l = 0; l <= ls.Count && j + l <= K; l++)
                {
                    // l時間サボる
                    dp[i + 1, j + l] = Math.Min(dp[i + 1, j + l], dp[i, j] + t[l]);
                }
            }
        }

        long ans = long.MaxValue;
        for (int i = 0; i <= K; i++)
        {
            ans = Math.Min(ans, dp[N, i]);
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
