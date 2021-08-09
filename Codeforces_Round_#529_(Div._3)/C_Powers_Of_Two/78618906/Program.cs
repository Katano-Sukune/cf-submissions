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
        int k = sc.NextInt();
        if (n < k)
        {
            Console.WriteLine("NO");
            return;
        }
        var l = new LinkedList<int>();
        int t = 1;
        int tmp = n;
        while (tmp > 0)
        {
            if (tmp % 2 == 1) l.AddFirst(t);
            tmp /= 2;
            t *= 2;
        }

        if (l.Count > k)
        {
            Console.WriteLine("NO");
            return;
        }

        while (l.Count < k)
        {
            var f = l.First.Value;
            l.RemoveFirst();
            if (f == 2)
            {
                l.AddLast(1);
                l.AddLast(1);
            }
            else
            {
                l.AddFirst(f / 2);
                l.AddFirst(f / 2);
            }
        }

        Console.WriteLine("YES");
        Console.WriteLine(string.Join(" ", l));
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
