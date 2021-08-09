using System;
using CompLib.Util;

public class Program
{
    private int N;
    private string S;
    private int[] A;
    private const string T = "hard";

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        S = sc.Next();
        A = sc.IntArray();

        // sから部分列を消して部分列"hard"がないようにする

        // s_iを消すとA_i増える
        // 最小値

        // i文字目見てる hardのj文字目までの部分列ができてる
        var dp = new long[N + 1, 4];
        for (int i = 0; i <= N; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                dp[i, j] = long.MaxValue;
            }
        }

        dp[0, 0] = 0;
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (dp[i, j] == long.MaxValue) continue;
                // 使う
                if (S[i] == T[j])
                {
                    if (j + 1 < 4)
                    {
                        dp[i + 1, j + 1] = Math.Min(dp[i + 1, j + 1], dp[i, j]);
                    }
                }
                else
                {
                    dp[i + 1, j] = Math.Min(dp[i + 1, j], dp[i, j]);
                }

                // 使わない
                dp[i + 1, j] = Math.Min(dp[i + 1, j], dp[i, j] + A[i]);
            }
        }

        long ans = long.MaxValue;
        for (int i = 0; i < 4; i++)
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
                string s = Console.ReadLine();
                while (s.Length == 0)
                {
                    s = Console.ReadLine();
                }

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