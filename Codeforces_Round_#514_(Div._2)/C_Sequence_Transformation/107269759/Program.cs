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
         * 1,2,3,...N 数列a
         * gcdをbに追加
         * aから1つ削除
         *
         * bの辞書順最大
         * 
         */

        int p = 1;
        int[] b = new int[N];
        int ptr = 0;
        while (ptr < b.Length)
        {
            if (N / 3 >= N / 2)
            {
                for (int i = 0; i < N - N / 3; i++)
                {
                    b[ptr++] = p;
                }
                p *= 3;
                N /= 3;
            }
            else
            {
                for (int i = 0; i < N - N / 2; i++)
                {
                    b[ptr++] = p;
                }
                p *= 2;
                N /= 2;
            }
        }

        Console.WriteLine(string.Join(" ", b));
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