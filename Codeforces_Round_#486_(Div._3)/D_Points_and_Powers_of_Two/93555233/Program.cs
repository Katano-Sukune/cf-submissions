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
        int n = sc.NextInt();
        long[] x = sc.LongArray();

        /*
         * iはx_iにいる
         * 
         * 部分集合のペアの距離が2冪になるように選ぶ
         * 最大
         */
        var hs = new HashSet<long>(x);
        Array.Sort(x);

        bool twoF = false;
        long[] two = new long[2];
        foreach (var p in x)
        {
            for (int e = 0; e <= 30; e++)
            {
                long o = p - (1 << e);
                long q = p + (1 << e);
                if (hs.Contains(o))
                {
                    two[0] = o;
                    two[1] = p;
                    twoF = true;
                    if (hs.Contains(q))
                    {
                        Console.WriteLine(3);
                        Console.WriteLine($"{o} {p} {q}");
                        return;
                    }
                }

            }
        }

        if (twoF)
        {
            Console.WriteLine(2);
            Console.WriteLine($"{two[0]} {two[1]}");
            return;
        }

        Console.WriteLine(1);
        Console.WriteLine(x[0]);
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
