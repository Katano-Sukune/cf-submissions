using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private const int L = 10;
    private int[][] A;

    public void Solve()
    {
        var sc = new Scanner();
        A = new int[L][];
        for (int i = 0; i < L; i++)
        {
            A[i] = sc.IntArray();
        }

        // ここからゴールまでの時間期待値, 初手はしご使用可 or 不可 
        double[,] dp = new double[L * L, 2];
        for (int p = L * L - 2; p >= 0; p--)
        {
            (int i, int j) = F(p);
            // はしご
            // さいころ
            double dice = 0;

            int m = Math.Min(6, L * L - 1 - p);
            double t = (double) 6 / m;
            for (int d = 1; d <= m; d++)
            {
                dice += (dp[p + d, 1] + t) / m;
            }

            // はしご
            double ladder = A[i][j] == 0 ? double.MaxValue : dp[G(i - A[i][j], j), 0];

            dp[p, 1] = Math.Min(dice, ladder);
            dp[p, 0] = dice;
        }

        Console.WriteLine(dp[0, 1]);
    }

    (int i, int j) F(int num)
    {
        int i = L - 1 - num / L;
        int j = i % 2 == 1 ? num % L : L - 1 - num % L;

        return (i, j);
    }

    int G(int i, int j)
    {
        return (L - 1 - i) * L + (i % 2 == 1 ? j : L - 1 - j);
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