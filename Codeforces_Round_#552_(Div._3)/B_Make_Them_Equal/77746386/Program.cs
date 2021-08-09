using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int[] a = sc.IntArray();

        var hs = new HashSet<int>();
        foreach (var i in a)
        {
            hs.Add(i);
        }
        var ar = hs.ToArray();
        Array.Sort(ar);
        switch (ar.Length)
        {
            case 1:
                Console.WriteLine(0);
                return;
            case 2:
                int d = ar[1] - ar[0];
                Console.WriteLine(d % 2 == 0 ? d / 2 : d);
                return;
            case 3:
                if (ar[2] - ar[1] != ar[1] - ar[0])
                {
                    Console.WriteLine("-1");
                }
                else
                {
                    Console.WriteLine(ar[1] - ar[0]);
                }
                return;
            default:
                Console.WriteLine("-1");
                return;
        }
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
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(Separator);
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
            _line = Console.ReadLine().Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}
