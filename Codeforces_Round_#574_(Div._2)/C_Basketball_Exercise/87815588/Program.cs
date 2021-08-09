using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    private int N;
    private int[][] H;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        H = new int[2][];
        for (int i = 0; i < 2; i++)
        {
            H[i] = sc.IntArray();
        }

        // 2N人 2行に並べさせた
        // i行 j列の身長 H[i][j]

        // 任意の人数選ぶ
        // 最初に選んだ人を除き、前に選んだ人の列より大きい列の人を選ぶ

        // 直前に選んだ人と違う行の人を選ぶ

        long[] dp = new long[2];
        dp[0] = H[0][0];
        dp[1] = H[1][0];
        for (int i = 1; i < N; i++)
        {
            long[] next = new long[2];
            next[0] = Math.Max(dp[0], dp[1] + H[0][i]);
            next[1] = Math.Max(dp[1], dp[0] + H[1][i]);
            dp = next;
        }

        Console.WriteLine(dp.Max());
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