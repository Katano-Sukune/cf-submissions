using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, M, K;
    private int[][] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        K = sc.NextInt();
        A = new int[N][];
        for (int i = 0; i < N; i++)
        {
            A[i] = sc.IntArray();
        }

        var dp = new int[N + 1, K];
        for (int i = 0; i <= N; i++)
        {
            for (int j = 0; j < K; j++)
            {
                dp[i, j] = int.MinValue;
            }
        }

        dp[0, 0] = 0;
        for (int i = 0; i < N; i++)
        {
            var dp2 = Calc(i);
            // Console.WriteLine(i);
            // Console.WriteLine(string.Join(" ", dp2));
            for (int j = 0; j < K; j++)
            {
                int cur = dp[i, j];
                if (cur == int.MinValue) continue;
                for (int k = 0; k < K; k++)
                {
                    if (dp2[k] == int.MinValue) continue;
                    dp[i + 1, (j + k) % K] = Math.Max(dp[i + 1, (j + k) % K], cur + dp2[k]);
                }
            }
        }


        Console.WriteLine(dp[N, 0]);
    }

    int[] Calc(int r)
    {
        int[,,] dp = new int[M + 1, M / 2 + 1, K];
        for (int i = 0; i <= M; i++)
        {
            for (int j = 0; j <= M / 2; j++)
            {
                for (int k = 0; k < K; k++)
                {
                    dp[i, j, k] = int.MinValue;
                }
            }
        }

        dp[0, 0, 0] = 0;
        for (int i = 0; i < M; i++)
        {
            for (int j = 0; j <= M / 2; j++)
            {
                for (int k = 0; k < K; k++)
                {
                    if (dp[i, j, k] == int.MinValue) continue;
                    dp[i + 1, j, k] = Math.Max(dp[i + 1, j, k], dp[i, j, k]);
                    if (j + 1 <= M / 2)
                    {
                        dp[i + 1, j + 1, (k + A[r][i]) % K] =
                            Math.Max(dp[i + 1, j + 1, (k + A[r][i]) % K], dp[i, j, k] + A[r][i]);
                    }
                }
            }
        }

        int[] ans = new int[K];
        for (int i = 0; i < K; i++)
        {
            ans[i] = int.MinValue;
        }

        for (int i = 0; i <= M / 2; i++)
        {
            for (int j = 0; j < K; j++)
            {
                ans[j] = Math.Max(ans[j], dp[M, i, j]);
            }
        }

        return ans;
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