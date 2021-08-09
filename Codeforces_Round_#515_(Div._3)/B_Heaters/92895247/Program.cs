using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int r = sc.NextInt();
        int[] a = sc.IntArray();

        int[] dp = new int[n + 1];
        for (int i = 1; i <= n; i++)
        {
            dp[i] = int.MaxValue;
        }

        for (int i = 0; i < n; i++)
        {
            if (a[i] == 0) continue;
            int min = int.MaxValue;
            for (int j = Math.Max(0, i - r + 1); j <= i + r - 1 && j < n; j++)
            {
                min = Math.Min(min, dp[j]);
            }

            if (min == int.MaxValue)
            {
                Console.WriteLine("-1");
                return;
            }
            dp[Math.Min(i + r, n)] = Math.Min(dp[Math.Min(i + r, n)], min + 1);
        }

        if (dp[n] == int.MaxValue)
        {
            Console.WriteLine("-1");
            return;
        }

        Console.WriteLine(dp[n]);
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
