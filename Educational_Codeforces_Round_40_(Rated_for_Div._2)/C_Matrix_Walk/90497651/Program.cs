using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    int[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();

        /*
         * x*y行列A
         * 
         * A_i_j = y(i-1)+j
         * 
         * a 
         * a_1から開始して隣接する要素に移動
         * 
         * 可能か? X,Y
         * 
         */

        /*
         * 右 + 1
         * 左 - 1
         * 
         * 下 + y
         * 上 - y
         */

        /*
         * 差が1かyだけか?
         * 
         * x yで割った余り最大
         */

        if (N == 1)
        {
            Console.WriteLine("YES");
            Console.WriteLine($"1 {A[0]}");
            return;
        }

        long y = long.MaxValue;

        for (int i = 1; i < N; i++)
        {
            long d = Math.Abs(A[i] - A[i - 1]);
            if (d != 1)
            {
                if (y == long.MaxValue)
                {
                    y = d;
                }
                else if (y != d)
                {
                    Console.WriteLine("NO");
                    return;
                }
            }
        }

        if (y == long.MaxValue)
        {
            y = (long)1e9;
        }

        if (y == 0)
        {
            Console.WriteLine("NO");
            return;
        }


        for (int i = 1; i < N; i++)
        {
            if (Math.Abs(A[i] - A[i - 1]) == 1)
            {
                long d1 = (A[i] + y - 1) / y;
                long d2 = (A[i-1] + y - 1) / y;
                if (d1 != d2)
                {
                    Console.WriteLine("NO");
                    return;
                }
            }
        }

        Console.WriteLine("YES");
        Console.WriteLine($"{(long)1e9} {y}");
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
