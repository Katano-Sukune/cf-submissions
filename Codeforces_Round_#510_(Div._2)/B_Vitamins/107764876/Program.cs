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
        int[] c = new int[n];
        int[] s = new int[n];
        for (int i = 0; i < n; i++)
        {
            c[i] = sc.NextInt();
            string tmp = sc.Next();
            foreach (var ch in tmp)
            {
                s[i] |= (1 << (ch - 'a'));
            }
        }

        var dp = new int[n + 1, 8];
        for (int i = 0; i <= n; i++)
        {
            for (int j = 0; j <= 7; j++)
            {
                dp[i, j] = int.MaxValue;
            }
        }

        dp[0, 0] = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (dp[i, j] == int.MaxValue) continue;
                dp[i + 1, j] = Math.Min(dp[i + 1, j], dp[i, j]);
                dp[i + 1, j | s[i]] = Math.Min(dp[i + 1, j | s[i]], dp[i, j] + c[i]);
            }
        }


        if (dp[n, 7] == int.MaxValue)
        {
            Console.WriteLine("-1");
            return;
        }

        Console.WriteLine(dp[n, 7]);
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