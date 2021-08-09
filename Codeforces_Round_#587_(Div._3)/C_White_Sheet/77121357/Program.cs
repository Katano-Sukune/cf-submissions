using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int x1 = sc.NextInt();
        int y1 = sc.NextInt();
        int x2 = sc.NextInt();
        int y2 = sc.NextInt();

        int x3 = sc.NextInt();
        int y3 = sc.NextInt();
        int x4 = sc.NextInt();
        int y4 = sc.NextInt();

        int x5 = sc.NextInt();
        int y5 = sc.NextInt();
        int x6 = sc.NextInt();
        int y6 = sc.NextInt();

        S a = new S(x1, y1, x2, y2);
        S b = new S(x3, y3, x4, y4);
        S c = new S(x5, y5, x6, y6);

        if (!b.I(a.X1, a.Y1) && !c.I(a.X1, a.Y1))
        {
            Console.WriteLine("YES");
            return;
        }
        if (!b.I(a.X1, a.Y2) && !c.I(a.X1, a.Y2))
        {
            Console.WriteLine("YES");
            return;
        }
        if (!b.I(a.X2, a.Y1) && !c.I(a.X2, a.Y1))
        {
            Console.WriteLine("YES");
            return;
        }
        if (!b.I(a.X2, a.Y2) && !c.I(a.X2, a.Y2))
        {
            Console.WriteLine("YES");
            return;
        }

        if(b.I(a.X1,a.Y1) && b.I(a.X2, a.Y2))
        {
            Console.WriteLine("NO");
            return;
        }

        if (c.I(a.X1, a.Y1) && c.I(a.X2, a.Y2))
        {
            Console.WriteLine("NO");
            return;
        }

        if (b.I(c.X1, c.Y1) || b.I(c.X1, c.Y2) || b.I(c.X2, c.Y1) || b.I(c.X2, c.Y2))
        {
            Console.WriteLine("NO");
            return;
        }
        if (c.I(b.X1, b.Y1) || c.I(b.X1, b.Y2) || c.I(b.X2, b.Y1) || c.I(b.X2, b.Y2))
        {
            Console.WriteLine("NO");
            return;
        }

        Console.WriteLine("YES");
    }

    public static void Main(string[] args) => new Program().Solve();
}

struct S
{
    public int X1, Y1, X2, Y2;
    public S(int x1, int y1, int x2, int y2)
    {
        X1 = x1;
        Y1 = y1;
        X2 = x2;
        Y2 = y2;
    }

    public bool I(int x, int y)
    {
        return X1 <= x && x <= X2 && Y1 <= y && y <= Y2;
    }
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
