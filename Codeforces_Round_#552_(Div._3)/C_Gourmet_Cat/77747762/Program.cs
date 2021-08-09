using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        long a = sc.NextLong();
        long b = sc.NextLong();
        long c = sc.NextLong();

        // 1週間で消費
        // a 3
        // b 2
        // c 2

        long week = Math.Min(a / 3, Math.Min(b / 2, c / 2));
        long ans = 0;
        int[] w = new int[7] { 0, 1, 2, 0, 2, 1, 0 };
        for (int i = 0; i < 7; i++)
        {
            long[] abc = { a - 3 * week, b - 2 * week, c - 2 * week };
            for (int j = 0; j < 7; j++)
            {
                if (abc[w[(i + j) % 7]] == 0)
                {
                    ans = Math.Max(ans, j);
                    break;
                }
                abc[w[(i + j) % 7]]--;
            }
        }
        Console.WriteLine(ans + 7 * week);
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
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(Separator);
                _index = 0;
            }

            return _line[_index++];
        }

        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public decimal NextDecimal() => decimal.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            _line = Console.ReadLine().Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}
