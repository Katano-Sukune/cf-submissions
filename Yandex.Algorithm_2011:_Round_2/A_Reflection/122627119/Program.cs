using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.Collections.Generic;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();


        // n
        // 
        // m = 10^len(n) - 1
        // 
        // n * (m-n)
        string strL = sc.Next();
        string strR = sc.Next();
        long l = long.Parse(strL);
        long r = long.Parse(strR);

        if (strL.Length == strR.Length)
        {
            int len = strL.Length;
            long m = 1;
            for (int i = 0; i < len; i++) m *= 10;
            m--;



            long mid = m / 2;

            if ((l <= mid && mid <= r) || (l <= (mid + 1) && (mid + 1) <= r))
            {
                long c = mid * (m - mid);
                Console.WriteLine(c);
            }
            else
            {
                long a = l * (m - l);
                long b = r * (m - r);
                Console.WriteLine(Math.Max(a, b));
            }
            return;
        }

        {
            int len = strR.Length;
            long m = 1;
            for (int i = 0; i < len; i++) m *= 10;
            m--;

            long mid = m / 2;
            if ((l <= mid && mid <= r) || (l <= (mid + 1) && (mid + 1) <= r))
            {
                long c = mid * (m - mid);
                Console.WriteLine(c);
            }
            else
            {
                Console.WriteLine(r * (m - r));
            }
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
