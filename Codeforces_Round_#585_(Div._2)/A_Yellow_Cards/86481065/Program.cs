using System;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int a1 = sc.NextInt();
        int a2 = sc.NextInt();
        int k1 = sc.NextInt();
        int k2 = sc.NextInt();
        int n = sc.NextInt();

        // 全員退場しないギリギリ
        int tmp = a1 * (k1 - 1) + a2 * (k2 - 1);
        int min = Math.Min(a1 + a2, Math.Max(0, n - tmp));

        int max = 0;

        if (k1 <= k2)
        {
            int d1 = Math.Min(a1, n / k1);
            max += d1;
            n -= d1 * k1;

            int d2 = Math.Min(a2, n / k2);
            max += d2;
        }
        else
        {
            int d1 = Math.Min(a2, n / k2);
            max += d1;
            n -= d1 * k2;

            int d2 = Math.Min(a1, n / k1);
            max += d2;
        }

        Console.WriteLine($"{min} {max}");
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