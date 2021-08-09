using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private long N, M, K;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        K = sc.NextInt();

        // (x1,y1) = (0,0)
        // (x1,y1),(x2,y2),(x3,y3)

        // A = (x2*y3 - x3*y2)/2
        // A = N*M/K
        if (N * M * 2 % K != 0)
        {
            Console.WriteLine("NO");
            return;
        }

        long A2 = N * M * 2 / K;

        long x2 = N;
        long y3 = (A2 + x2 - 1) / x2;

        long x3 = x2 * y3 - A2;
        long y2 = 1;

        Console.WriteLine("YES");
        Console.WriteLine("0 0");
        Console.WriteLine($"{x2} {y2}");
        Console.WriteLine($"{x3} {y3}");
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