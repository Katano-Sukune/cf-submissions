using CompLib.Util;
using System;
using System.Collections.Generic;
using System.Text;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < t; i++)
        {
            string s = sc.Next();
            var list = new List<P>();
            int x = 0;
            int y = 0;
            foreach (var c in s)
            {
                int nx = x;
                int ny = y;
                if (c == 'N') ny++;
                if (c == 'S') ny--;
                if (c == 'E') nx++;
                if (c == 'W') nx--;
                var p = new P(x, y, nx, ny);
                list.Add(p);
                x = nx;
                y = ny;
            }
            list.Sort((l, r) =>
            {
                if (l.X != r.X) return l.X.CompareTo(r.X);
                if (l.Y != r.Y) return l.Y.CompareTo(r.Y);
                if (l.X2 != r.X2) return l.X2.CompareTo(r.X2);
                return l.Y2.CompareTo(r.Y2);
            });

            int ans = 5;
            for (int j = 1; j < list.Count; j++)
            {
                bool equal = list[j].X == list[j - 1].X && list[j].Y == list[j - 1].Y && list[j].X2 == list[j - 1].X2 && list[j].Y2 == list[j - 1].Y2;
                ans += equal ? 1 : 5;
            }

            sb.AppendLine(ans.ToString());

        }
        Console.Write(sb);
    }

    public static void Main(string[] args) => new Program().Solve();
}

struct P
{
    public int X, Y, X2, Y2;
    public P(int x, int y, int x2, int y2)
    {
        if (x * 1000000 + y <= x2 * 1000000 + y2)
        {
            X = x;
            Y = y;
            X2 = x2;
            Y2 = y2;
        }
        else
        {
            X2 = x;
            Y2 = y;
            X = x2;
            Y = y2;
        }
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
