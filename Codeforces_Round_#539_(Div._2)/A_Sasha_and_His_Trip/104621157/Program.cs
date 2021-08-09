using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int v = sc.NextInt();

        int[,] dp = new int[n, v + 1];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j <= v; j++)
            {
                dp[i, j] = int.MaxValue;
            }
        }
        dp[0, 0] = 0;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j <= v; j++)
            {
                if (dp[i, j] == int.MaxValue) continue;
                for (int k = 0; j + k <= v; k++)
                {
                    if (j + k > 0) dp[i + 1, j + k - 1] = Math.Min(dp[i + 1, j + k - 1], dp[i, j] + (i + 1) * k); ;
                }
            }
        }

        int ans = int.MaxValue;
        for (int i = 0; i <= v; i++)
        {
            ans = Math.Min(ans, dp[n - 1, i]);
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
