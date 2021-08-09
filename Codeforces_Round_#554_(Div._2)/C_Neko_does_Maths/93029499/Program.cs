using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int a = sc.NextInt();
        int b = sc.NextInt();
        /*
         * a+k, b+kの最小公倍数が最小になるk
         * 
         * そのうち最小
         */

        long t = Math.Abs(a - b);
        if (t == 0)
        {
            Console.WriteLine(0);
            return;
        }

        long minLCM = long.MaxValue;
        long minK = long.MaxValue;

        for (long div = 1; div * div <= t; div++)
        {
            if (t % div == 0)
            {
                // a,bの公約数がdiv
                long m = a % div;
                long k = m == 0 ? 0 : div - m;
                long lcm = (a + k) * (b + k) / div;

                if (lcm < minLCM || (lcm == minLCM && k < minK))
                {
                    minLCM = lcm;
                    minK = k;
                }

                long div2 = t / div;
                long m2 = a % div2;
                long k2 = m2 == 0 ? 0 : div2 - m2;
                long lcm2 = (a + k2) * (b + k2) / div2;

                if (lcm2 < minLCM || (lcm2 == minLCM && k2 < minK))
                {
                    minLCM = lcm2;
                    minK = k2;
                }
            }
        }

        Console.WriteLine(minK);
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
