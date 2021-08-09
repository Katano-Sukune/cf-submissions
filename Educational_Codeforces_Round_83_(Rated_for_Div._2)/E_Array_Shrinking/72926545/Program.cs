using System;
using CompLib.Util;

public class Program
{
    private int N;
    private int[] A;

    // 右端 i 左端 jの長さが1にできる 値


    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();

        var dp = new int[N][];
        for (int i = 0; i < N; i++)
        {
            dp[i] = new int[N + 1];
            for (int j = 0; j <= N; j++)
            {
                dp[i][j] = -1;
            }
        }

        for (int i = N - 1; i >= 0; i--)
        {
            dp[i][i + 1] = A[i];
            for (int j = i + 1; j < N; j++)
            {
                if (dp[i][j] == -1) continue;
                for (int k = j + 1; k <= N; k++)
                {
                    if (dp[i][j] == dp[j][k])
                    {
                        dp[i][k] = dp[i][j] + 1;
                    }
                }
            }
        }

        // i文字目までの長さの最小値
        var ans = new int[N + 1];
        for (int i = 1; i <= N; i++) ans[i] = int.MaxValue;
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j <= N; j++)
            {
                if (dp[i][j] == -1) continue;
                ans[j] = Math.Min(ans[j], ans[i] + 1);
            }
        }

        Console.WriteLine(ans[N]);
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