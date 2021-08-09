using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, M;
    private string A, B;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        A = sc.Next();
        B = sc.Next();

        // LCS (a,b) a,bの最長共通部分列

        // S(c,d) = 4 * LCS(c,d) - |c| - |d|
        //        = 
        // Aの連続部分列C, B D
        // S(C,D)の最大

        // bbbaba
        // bbbaba
        // 24 - 

        // Aのi文字目、Bのjが左端の部分列
        // Sの最大
        int[,] dp = new int[N + 1, M + 1];

        for (int i = 0; i <= N; i++)
        {
            for (int j = 0; j <= M; j++)
            {
                if (i == 0 || j == 0)
                {
                    dp[i, j] = 0;
                    continue;
                }

                dp[i, j] = Math.Max(dp[i, j], dp[i - 1, j] - 1);
                dp[i, j] = Math.Max(dp[i, j], dp[i, j - 1] - 1);
                if (A[i - 1] == B[j - 1])
                {
                    dp[i, j] = Math.Max(dp[i, j], dp[i - 1, j - 1] + 2);
                }
            }
        }

        int ans = 0;
        for (int i = 0; i <= N; i++)
        {
            for (int j = 0; j <= M; j++)
            {
                ans = Math.Max(ans, dp[i, j]);
            }
        }

        Console.WriteLine(ans);
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