using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        long n = sc.NextLong();
        long k = sc.NextLong();

        // 和 n
        // 要素k個 単調増加
        // 最大公約数最大

        List<long> d = new List<long>();
        for (long x = 1; x * x <= n; x++)
        {
            if (n % x == 0)
            {
                d.Add(x);
                d.Add(n / x);
            }
        }

        d.Sort((l, r) => r.CompareTo(l));

        foreach (var l in d)
        {
            double t = (double) l * (k + 1) * k / 2;
            if (t > 1e16) continue;
            long min = l * (k + 1) * k / 2;
            if (min > n) continue;

            long m = n - min;
            long[] ans = new long[k];
            for (int i = 0; i < k; i++)
            {
                ans[i] = l * (i + 1);
            }

            ans[k - 1] += m;

            Console.WriteLine(string.Join(" ", ans));
            return;
        }

        Console.WriteLine("-1");
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