using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{
    int N;
    int[] C;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        C = sc.IntArray();

        var ls = new List<int>();
        for (int i = 0; i < N; i++)
        {
            if (i == 0 || C[i - 1] != C[i]) ls.Add(C[i]);
        }

        var dp = new int[ls.Count, ls.Count + 1];
        for (int i = 0; i < ls.Count; i++)
        {
            for (int j = 0; j <= ls.Count; j++)
            {
                dp[i, j] = int.MaxValue;
            }
        }
        for (int i = 0; i < ls.Count; i++)
        {
            dp[i, i + 1] = 0;
        }

        for (int len = 1; len < ls.Count; len++)
        {
            for (int l = 0; l + len <= ls.Count; l++)
            {
                if (l + len + 1 <= ls.Count) dp[l, l + len + 1] = Math.Min(dp[l, l + len + 1], dp[l, l + len] + 1);
                if (l > 0) dp[l - 1, l + len] = Math.Min(dp[l - 1, l + len], dp[l, l + len] + 1);
                if (l > 0 && l + len + 1 <= ls.Count && ls[l - 1] == ls[l + len]) dp[l - 1, l + len + 1] = Math.Min(dp[l - 1, l + len + 1], dp[l, l + len] + 1);
            }
        }

        Console.WriteLine(dp[0, ls.Count]);
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
