using System;
using System.Linq;
using System.Security.Cryptography;
using CompLib.Util;

public class Program
{
    int N, K;
    string S;
    string T;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        S = sc.Next();
        T = sc.Next();

        /*
         * sと2文字t
         * 
         * sの任意の文字をえらんで別の文字に置き換える
         * 
         * k回まで操作
         * tの出現回数最大
         */

        if (T[0] == T[1])
        {
            int cnt = 0;
            foreach (var c in S)
            {
                if (c == T[0]) cnt++;
            }

            int max = Math.Min(cnt + K, N);

            Console.WriteLine((long)max * (max - 1) / 2);
        }
        else
        {

            // iまで見る
            // T[0]の個数
            // 変更回数
            int[,,] dp = new int[N + 1, N + 1, K + 1];
            for (int i = 0; i <= N; i++)
            {
                for (int j = 0; j <= N; j++)
                {
                    for (int k = 0; k <= K; k++)
                    {
                        dp[i, j, k] = int.MinValue;
                    }
                }
            }

            dp[0, 0, 0] = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j <= N; j++)
                {
                    for (int k = 0; k <= K; k++)
                    {
                        if (dp[i, j, k] == int.MinValue) continue;
                        if (S[i] == T[0])
                        {
                            dp[i + 1, j + 1, k] = Math.Max(dp[i + 1, j + 1, k], dp[i, j, k]);
                        }
                        else if (k + 1 <= K)
                        {
                            dp[i + 1, j + 1, k + 1] = Math.Max(dp[i + 1, j + 1, k + 1], dp[i, j, k]);
                        }

                        if (S[i] == T[1])
                        {
                            dp[i + 1, j, k] = Math.Max(dp[i + 1, j, k], dp[i, j, k] + j);
                        }
                        else if (k + 1 <= K)
                        {
                            dp[i + 1, j, k + 1] = Math.Max(dp[i + 1, j, k + 1], dp[i, j, k] + j);
                        }

                        dp[i + 1, j, k] = Math.Max(dp[i + 1, j, k], dp[i, j, k]);
                    }
                }
            }

            int ans = int.MinValue;
            for (int j = 0; j <= N; j++)
            {
                for (int k = 0; k <= K; k++)
                {
                    ans = Math.Max(ans, dp[N, j, k]);
                }
            }

            Console.WriteLine(ans);
        }
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
