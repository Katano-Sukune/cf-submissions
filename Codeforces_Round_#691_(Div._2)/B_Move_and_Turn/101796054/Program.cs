using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();

        // 1 4
        // 2 4
        // 3 12
        // 4 9
        // 5 24
        // 6 16
        // 7 40
        // 8 25
        // 9 60
        // 11 84
        // 13

        if (n % 2 == 0)
        {
            int m = n / 2 + 1;
            Console.WriteLine(m * m);
        }
        else
        {
            int a = n / 2 + 1;
            int b = n + 3;
            Console.WriteLine(a*b);
        }
        //var hs = new HashSet<(int x, int y)>[n + 1, 2];
        //for (int i = 0; i <= n; i++)
        //{
        //    hs[i, 0] = new HashSet<(int x, int y)>();
        //    hs[i, 1] = new HashSet<(int x, int y)>();
        //}
        //hs[0, 0].Add((0, 0));
        //hs[0, 1].Add((0, 0));
        //for (int i = 0; i < n; i++)
        //{
        //    foreach ((int x, int y) in hs[i, 0])
        //    {
        //        hs[i + 1, 1].Add((x + 1, y));
        //        hs[i + 1, 1].Add((x - 1, y));
        //    }

        //    foreach ((int x, int y) in hs[i, 1])
        //    {
        //        hs[i + 1, 0].Add((x, y + 1));
        //        hs[i + 1, 0].Add((x, y - 1));
        //    }
        //}

        //var hs2 = new HashSet<(int r, int c)>();
        //foreach ((int x, int y) t in hs[n, 0])
        //{
        //    hs2.Add(t);
        //}
        //foreach ((int x, int y) t in hs[n, 1])
        //{
        //    hs2.Add(t);
        //}

        //Console.WriteLine(hs2.Count);
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
