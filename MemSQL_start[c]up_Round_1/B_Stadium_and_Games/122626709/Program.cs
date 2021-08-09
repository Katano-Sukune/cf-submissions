using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.Collections.Generic;
using System.Numerics;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        BigInteger n = sc.NextLong();
        var ans = new List<BigInteger>();
        for (long b = 1; b <= 1L << 60; b *= 2)
        {
            // m*b チーム
            // m*(m-1)/2 + m*(b-1)

            // m
            BigInteger ng = 0;
            BigInteger ok = long.MaxValue;

            while (ok - ng > 1)
            {
                var mid = (ok + ng) / 2;
                var tmp = mid * (mid - 1) / 2 + mid * (b - 1);
                if (tmp >= n) ok = mid;
                else ng = mid;
            }

            if (n != ok * (ok - 1) / 2 + ok * (b - 1)) continue;
            if (ok % 2 == 0) continue;

            ans.Add(ok * b);
        }
        if (ans.Count == 0)
        {
            Console.WriteLine("-1");
            return;
        }
        ans.Sort();
        Console.WriteLine(string.Join('\n', ans));
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
