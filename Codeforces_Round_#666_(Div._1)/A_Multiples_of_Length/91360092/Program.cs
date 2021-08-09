using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    long[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.LongArray();

        /*
         * 配列A
         * 
         * 3回
         * セグメントを選んで各数に長さの倍数加算
         * 
         * 全部0にする
         */

        /*
         * 1 3 2 4
         * 
         * 全部 Nの倍数
         * 一個のこし (N-1)の倍数
         */

        if (N == 1)
        {
            Console.WriteLine("1 1");
            Console.WriteLine(-A[0]);
            Console.WriteLine("1 1");
            Console.WriteLine("0");
            Console.WriteLine("1 1");
            Console.WriteLine("0");
            return;
        }

        Console.WriteLine($"{1} {N}");
        var f = new long[N];
        for (int i = 0; i < N - 1; i++)
        {
            f[i] = -A[i] * N;
        }
        Console.WriteLine(string.Join(" ", f));
        Console.WriteLine($"{1} {N - 1}");
        f = new long[N - 1];
        for (int i = 0; i < N - 1; i++)
        {
            f[i] = A[i] * (N - 1);
        }
        Console.WriteLine(string.Join(" ", f));
        Console.WriteLine($"{N} {N}");
        Console.WriteLine(-A[N - 1]);
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
