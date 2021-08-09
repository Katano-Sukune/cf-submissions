using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{
    long N, B;
    public void Solve()
    {
        checked
        {
            var sc = new Scanner();
            N = sc.NextLong();
            B = sc.NextLong();

            long ans = long.MaxValue;
            for (long p = 2; p * p <= B; p++)
            {
                if (B % p == 0)
                {
                    int cnt = 0;
                    while (B % p == 0)
                    {
                        cnt++;
                        B /= p;
                    }

                    long tmp = N;
                    long cnt2 = 0;
                    while (tmp > 0)
                    {
                        cnt2 += tmp / p;
                        tmp /= p;
                    }
                    ans = Math.Min(ans, cnt2 / cnt);
                }
            }

            if (B != 1)
            {
                long tmp = N;
                long cnt2 = 0;
                while (tmp > 0)
                {
                    cnt2 += tmp / B;
                    tmp /= B;
                }

                ans = Math.Min(ans, cnt2);
            }

            Console.WriteLine(ans);
        }
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
