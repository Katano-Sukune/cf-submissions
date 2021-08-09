using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();

        /*
         * 1~Nが2個ずつ
         *
         * iを
         * x_i, y_iに置く
         *
         * d_i = y_i - x_i
         *
         * 
         */

        /*
         * 1 3 1 3 2 2
         *
         * n-1 
         */


        int[] ans = new int[2 * N];


        /*
         * n-1
         * n-3
         * n-5
         * ... 
         */

        int a = N / 2;
        for (int i = 0; i < a; i++)
        {
            ans[a + i] = ans[a - i - 1] = N - i * 2 - 1;
        }

        int b = N - a - 1;
        for (int i = 0; i < b; i++)
        {
            ans[2 * a + b + i + 1] = ans[2 * a + b - i - 1] = N - i * 2 - 2;
        }

        ans[2 * a + b] = ans[^1] = N;


        Console.WriteLine(string.Join(" ", ans));
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