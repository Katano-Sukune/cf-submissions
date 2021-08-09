using System;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        long[] a = sc.LongArray();

        Array.Sort(a, (l, r) =>
        {
            int l2 = 0;
            int r2 = 0;
            int l3 = 0;
            int r3 = 0;

            for (; l % 2 == 0; l /= 2, l2++) ;
            for (; l % 3 == 0; l /= 3, l3++) ;
            for (; r % 2 == 0; r /= 2, r2++) ;
            for (; r % 3 == 0; r /= 3, r3++) ;

            // 2の昇順、3の降順
            if (r3 != l3) return r3.CompareTo(l3);
            else return l2.CompareTo(r2);
        });
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
