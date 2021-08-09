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
        int[] a = sc.IntArray();

        long[] t = new long[n + 1];
        for (int i = 0; i < n; i++)
        {
            t[i + 1] = t[i] + a[i];
        }

        // delim1がiのとき、最大になるdelim0
        long max = long.MinValue;
        int delim0 = -1, delim1 = -1, delim2 = -1;
        for (int d1 = 0; d1 <= n; d1++)
        {
            long fMax = long.MinValue;
            int tmpDelim0 = -1;
            for (int d0 = 0; d0 <= d1; d0++)
            {
                long score = t[d0] - (t[d1] - t[d0]);
                if (fMax < score)
                {
                    fMax = score;
                    tmpDelim0 = d0;
                }
            }

            long bMax = long.MinValue;
            int tmpDelim2 = -1;
            for (int d2 = d1; d2 <= n; d2++)
            {
                long score = (t[d2] - t[d1]) - (t[n] - t[d2]);
                if (bMax < score)
                {
                    bMax = score;
                    tmpDelim2 = d2;
                }
            }

            if (max < fMax + bMax)
            {
                max = fMax + bMax;
                delim0 = tmpDelim0;
                delim1 = d1;
                delim2 = tmpDelim2;
            }
        }

        Console.WriteLine($"{delim0} {delim1} {delim2}");
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