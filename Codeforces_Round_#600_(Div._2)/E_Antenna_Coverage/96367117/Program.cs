using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, M;
    private (int X, int S)[] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        A = new (int X, int S)[N];
        for (int i = 0; i < N; i++)
        {
            A[i] = (sc.NextInt(), sc.NextInt());
        }

        Array.Sort(A, (l, r) => l.X.CompareTo(r.X));

        // i個目まで見る jまでカバーするときコスト
        var dp = new long[N + 1, M + 1];
        for (int i = 0; i <= N; i++)
        {
            for (int j = 0; j <= M; j++)
            {
                dp[i, j] = int.MaxValue;
            }
        }

        dp[0, 0] = 0;
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j <= M; j++)
            {
                dp[i + 1, j] = dp[i, j];
            }

            (int x, int s) = A[i];
            // コスト0
            int l = Math.Max(1, x - s);
            int r = Math.Min(M, x + s);
            long min = int.MaxValue;
            for (int j = l; j <= r; j++)
            {
                min = Math.Min(min, dp[i, j - 1]);
            }

            dp[i + 1, r] = Math.Min(dp[i + 1, r], min);
            for (int j = 0; l >= 1 || r <= M; j++)
            {
                if (l >= 1)
                {
                    min = Math.Min(min, dp[i, l - 1]);
                }

                if (r <= M)
                {
                    min = Math.Min(min, dp[i, r - 1]);
                }

                dp[i + 1, Math.Min(M, r)] = Math.Min(dp[i + 1, Math.Min(M, r)], min + j);
                l--;
                r++;
            }
        }

        Console.WriteLine(dp[N, M]);
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