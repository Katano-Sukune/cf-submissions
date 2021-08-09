using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N, M, K;
    int[] A;

    int[] X, Y;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        K = sc.NextInt();
        A = sc.IntArray();
        Array.Sort(A);
        long[] sum = new long[K + 1];
        for (int i = 0; i < K; i++)
        {
            sum[i + 1] = sum[i] + A[i];
        }
        // XがKより大きいやつは使えない
        // Xが同じなのにKが小さいやつは使う必要ない
        int[] offer = new int[K + 1];
        for (int i = 0; i <= K; i++)
        {
            offer[i] = -1;
        }
        for (int i = 0; i < M; i++)
        {
            int x = sc.NextInt();
            int y = sc.NextInt();
            if (x > K) continue;
            offer[x] = Math.Max(offer[x], y);
        }

        // K個買うのに必要な費用
        long[] dp = new long[K + 1];
        for (int i = 0; i <= K; i++)
        {
            dp[i] = long.MaxValue;
        }

        dp[0] = 0;
        for (int i = 0; i < K; i++)
        {
            dp[i + 1] = Math.Min(dp[i + 1], dp[i] + A[i]);
            for (int j = 0; i + j <= K; j++)
            {
                if (offer[j] == -1) continue;
                dp[i + j] = Math.Min(dp[i + j], dp[i] + sum[i + j] - sum[i + offer[j]]);
            }
        }
        Console.WriteLine(dp[K]);
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
