using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int R, N;
    private int[] T, X, Y;

    public void Solve()
    {
        var sc = new Scanner();
        R = sc.NextInt();
        N = sc.NextInt();

        T = new int[N + 1];
        X = new int[N + 1];
        Y = new int[N + 1];
        T[0] = 0;
        X[0] = 1;
        Y[0] = 1;
        for (int i = 1; i <= N; i++)
        {
            T[i] = sc.NextInt();
            X[i] = sc.NextInt();
            Y[i] = sc.NextInt();
        }

        /*
         * 
         * 
         * 有名人iは t_i分後に (x_i, y_i)にいる
         * 現在 (1,1)にいる
         *
         * 撮れる有名人最大
         */

        int[] max = new int[N + 1];
        int[] dp = new int[N + 1];

        for (int i = 1; i <=N ; i++)
        {
            max[i] = int.MinValue;
            dp[i] = int.MinValue;
        }
        
        for (int i = 1; i <= N; i++)
        {
            int j = i - 1;
            while (j >= 0 && T[i] - T[j] < 2 * R)
            {
                if (Math.Abs(X[i] - X[j]) + Math.Abs(Y[i] - Y[j]) <= T[i] - T[j])
                {
                    dp[i] = Math.Max(dp[i], dp[j] + 1);
                }

                j--;
            }

            if (j >= 0) dp[i] = Math.Max(dp[i], max[j] + 1);
            max[i] = Math.Max(max[i - 1], dp[i]);
        }

        Console.WriteLine(max[N]);
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