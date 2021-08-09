using System;
using CompLib.Util;

public class Program
{
    int N;
    int[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();

        var dp = new long[N + 1, N + 1];
        for (int i = 0; i <= N; i++)
        {
            for (int j = 0; j <= N; j++)
            {
                dp[i, j] = long.MinValue;
            }
        }

        dp[0, 0] = 0;
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                if (dp[i, j] < 0) continue;
                dp[i + 1, j] = Math.Max(dp[i + 1, j], dp[i, j]);
                if (dp[i, j] + A[i] < 0) continue;
                dp[i + 1, j + 1] = Math.Max(dp[i + 1, j + 1], dp[i, j] + A[i]);
            }
        }

        for (int i = N; i >= 0; i--)
        {
            if (dp[N, i] >= 0)
            {
                Console.WriteLine(i);
                return;
            }
        }
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
