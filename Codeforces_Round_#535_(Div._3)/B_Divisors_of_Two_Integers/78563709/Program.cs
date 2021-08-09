using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int[] d = sc.IntArray();

        // x,yを受け取った
        // xの約数、yの約数列挙、シャッフル　...d

        // x,yいくつか?
        Array.Sort(d);
        int x = d[n - 1];
        var divX = new HashSet<int>();
        for (int i = 1; i * i <= x; i++)
        {
            if (x % i == 0)
            {
                if (x / i != i) divX.Add(x / i);
                divX.Add(i);
            }
        }
        var divY = new HashSet<int>();
        foreach (int i in d)
        {
            if (divX.Contains(i))
            {
                divX.Remove(i);
            }
            else
            {
                divY.Add(i);
            }
        }

        Console.WriteLine($"{x} {divY.Max()}");
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
