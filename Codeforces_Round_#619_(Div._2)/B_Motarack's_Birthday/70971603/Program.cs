using CompLib.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        var sb = new StringBuilder();
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            sb.AppendLine(Q(sc.NextInt(), sc.IntArray()));
        }

        Console.Write(sb.ToString());
    }

    private string Q(int n, int[] a)
    {
        int max = int.MinValue;
        int min = int.MaxValue;
        for (int i = 0; i < n; i++)
        {
            if (a[i] == -1)
            {
                if (i - 1 >= 0 && a[i - 1] != -1)
                {
                    max = Math.Max(max, a[i - 1]);
                    min = Math.Min(min, a[i - 1]);
                }


                if (i + 1 < n && a[i + 1] != -1)
                {
                    max = Math.Max(max, a[i + 1]);
                    min = Math.Min(min, a[i + 1]);
                }
            }
        }
        if (max == int.MinValue)
        {
            return "0 0";
        }
        int k = (max + min) / 2;

        int m = 0;

        for (int i = 0; i < n; i++)
        {
            if (a[i] == -1)
            {
                a[i] = k;
            }
        }

        for (int i = 0; i < n - 1; i++)
        {
            m = Math.Max(m, Math.Abs(a[i] - a[i + 1]));
        }


        return $"{m} {k}";
    }

    public static void Main(string[] args) => new Program().Solve();
}

namespace CompLib.Util
{
    using System;
    using System.Linq;
    class Scanner
    {
        private int _index;
        private string[] _line;
        const char separator = ' ';

        public Scanner()
        {
            _index = 0;
            _line = new string[0];
        }

        public string Next()
        {
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(separator);
                _index = 0;
            }
            return _line[_index++];
        }
        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            _line = Console.ReadLine().Split(separator);
            _index = _line.Length;
            return _line;
        }
        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
    }
}