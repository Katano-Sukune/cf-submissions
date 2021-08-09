using System;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int x = sc.NextInt();

        /*
         * m*mの部分行列すべてが少なくとも1つの0を含む
         * m-free
         * 
         * n,m
         * 1の個数最大
         * n*n行列がm-free
         * 1の個数xになるようなn,m
         * 
         */

        /*
         * 01111
         * 11111
         * 11111
         * 11111
         * 11111
         * 
         * n,m
         * (m*m-1)*floor(n/m)^2 + n*n - (m*floor(n/m))^2
         * floor(n/m) = t
         * 
         * = m^2*t^2 - t^2 + n^2 - m^2*t^2
         * = n^2 - t^2
         * 
         * = (n+t)(n-t)
         */
        if (x == 0)
        {
            Console.WriteLine("1 1");
            return;
        }
        for (long r = 1; r * r <= x; r++)
        {
            if (x % r == 0)
            {
                long l = x / r;
                if ((l + r) % 2 != 0) continue;
                long n = (l + r) / 2;
                long t = l - n;
                if (t <= 0) continue;
               
                long m = n / t;
                if (m <= 0 || n / m != t) continue;
                Console.WriteLine($"{n} {m}");
                return;
            }
        }

        Console.WriteLine("-1");
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
