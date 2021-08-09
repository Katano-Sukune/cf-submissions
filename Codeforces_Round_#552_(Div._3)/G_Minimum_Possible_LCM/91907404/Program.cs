using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    const int MaxA = 10000000;
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int[] a = sc.IntArray();

        int[] cnt = new int[MaxA + 1];
        foreach (var i in a)
        {
            cnt[i]++;
        }

        long lcm = long.MaxValue;
        long x = -1;
        long y = -1;

        for (long cd = 1; cd <= MaxA; cd++)
        {
            var ls = new List<long>();
            for (long j = cd; j <= MaxA && ls.Count < 2; j += cd)
            {
                for (int k = 0; k < cnt[j] && ls.Count < 2; k++)
                {
                    ls.Add(j);
                }
            }

            if (ls.Count >= 2)
            {
                long t = ls[0] * ls[1] / cd;
                if (t < lcm)
                {
                    lcm = t;
                    x = ls[0];
                    y = ls[1];
                }
            }
        }

        int ii = -1;
        int jj = -1;
        for (int i = 0; i < n; i++)
        {
            if (ii == -1 && a[i] == x)
            {
                ii = i;
            }
            else if (jj == -1 && a[i] == y)
            {
                jj = i;
            }
        }
        // Console.WriteLine($"{x} {y}");
        if (ii > jj)
        {
            int t = ii;
            ii = jj;
            jj = t;
        }

        Console.WriteLine($"{ii + 1} {jj + 1}");
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
