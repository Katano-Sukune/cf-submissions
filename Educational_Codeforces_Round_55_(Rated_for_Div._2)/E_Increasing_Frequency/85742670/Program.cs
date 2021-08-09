using System;
using CompLib.Util;

public class Program
{
    private int N, C;
    private int[] A;
    private const int MaxA = 500000;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        C = sc.NextInt();
        A = sc.IntArray();

        int[] last = new int[500001];

        var dp = new long[N + 1];
        var dp2 = new long[N + 1];
        dp[0] = 0;
        dp2[0] = 0;
        for (int i = 0; i < N; i++)
        {
            dp2[i + 1] = dp2[i];
            if (A[i] == C)
            {
                dp2[i + 1]++;
            }

            // dp[i + 1] = dp[i];
            dp[i + 1] = Math.Max(dp[i + 1], dp2[i] + 1);
            dp[i + 1] = Math.Max(dp[i + 1], dp[last[A[i]]] + 1);
            last[A[i]] = i + 1;
        }

        long ans = dp[N];

        int cnt = 0;
        for (int i = N - 1; i >= 0; i--)
        {
            if (A[i] == C) cnt++;
            ans = Math.Max(ans, cnt + dp[i]);
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