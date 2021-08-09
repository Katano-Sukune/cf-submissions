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
        int t = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < t; i++)
        {
            sb.AppendLine(Q(sc.NextInt(), sc.NextInt(), sc.NextInt()));
        }
        Console.Write(sb);
    }

    string Q(int n0, int n1, int n2)
    {
        // 長さ2の部分文字列について
        // n0個 00
        // n1個 01 or 10
        // n2個 11

        // 01の境目が n1個

        // 1111が n2+1個
        // 0000が n0+1個

        var sb = new StringBuilder();


        if (n1 == 0)
        {
            if (n0 > 0) sb.Append('0',n0 + 1);
            if (n2 > 0) sb.Append('1',n2 + 1);
        }
        else
        {
            sb.Append('1', n2 + 1);
            sb.Append('0', n0 + 1);
            for (int i = 1; i < n1; i++)
            {
                sb.Append(i % 2);
            }
        }
        return sb.ToString();
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
