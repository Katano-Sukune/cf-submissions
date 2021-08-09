using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int m = sc.NextInt();

        List<(int r, int c)> noru = new List<(int r, int c)>();
        for (int i = 0; i < n; i++)
        {
            noru.Add((i, 0));
            noru.Add((i, 3));
        }

        for (int i = 0; i < n; i++)
        {
            noru.Add((i, 1));
            noru.Add((i, 2));
        }

        int[,] num = new int[n, 4];
        for (int i = 1; i <= m; i++)
        {
            (int r, int c) = noru[i - 1];
            num[r, c] = i;
        }

        List<(int r, int c)> oriru = new List<(int r, int c)>();
        for (int i = 0; i < n; i++)
        {
            oriru.Add((i, 1));
            oriru.Add((i, 0));
            oriru.Add((i, 2));
            oriru.Add((i, 3));
        }

        List<int> ans = new List<int>(m);
        foreach ((int r, int c) in oriru)
        {
            if (num[r, c] == 0) continue;
            ans.Add(num[r, c]);
        }

        Console.WriteLine(string.Join(" ", ans));
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