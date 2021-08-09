using System;
using System.Text;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int T = sc.NextInt();
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < T; i++)
        {
            sb.AppendLine(Q(sc.NextInt(), sc.NextInt(), sc.NextInt(), sc.NextInt(), sc.NextInt(), sc.NextInt(),
                sc.NextInt(), sc.NextInt(), sc.NextInt(), sc.NextInt()));
        }

        Console.Write(sb.ToString());
    }

    string Q(int a, int b, int c, int d, int x, int y, int x1, int y1, int x2, int y2)
    {
        if (a > 0 && b > 0 && x == x1 && x == x2) return "NO";
        if (c > 0 && d > 0 && y == y1 && y == y2) return "NO";
        if (x1 > x + b - a || x + b - a > x2) return "NO";
        if (y1 > y + d - c || y + d - c > y2) return "NO";
        return "YES";
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