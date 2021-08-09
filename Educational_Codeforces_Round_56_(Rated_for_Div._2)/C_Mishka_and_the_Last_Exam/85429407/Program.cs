using System;
using CompLib.Util;

public class Program
{
    private int N;
    private long[] B;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        B = sc.LongArray();

        /*
         * nクラスある
         *
         * B 長さn/2
         *
         * 
         */

        /*
         * aは広義単調増加
         *
         * b_i = a_i + a_{n-i-1}
         *
         * aを復元
         */

        /*
         * 5 6
         *
         * 0 1 5 5
         */

        long[] a = new long[N];
        a[0] = 0;
        a[N - 1] = B[0];
        for (int i = 1; i < N / 2; i++)
        {
            if (B[i - 1] >= B[i])
            {
                a[i] = a[i - 1];
                a[N - i - 1] = B[i] - a[i];
            }
            else
            {
                a[N - i - 1] = a[N - i];
                a[i] = B[i] - a[N - i - 1];
            }
        }

        Console.WriteLine(string.Join(" ", a));
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
                string s = Console.ReadLine();
                while (s.Length == 0)
                {
                    s = Console.ReadLine();
                }

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