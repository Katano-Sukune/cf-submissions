using System;
using System.Collections.Generic;
using CompLib.Util;

public class Program
{
    private int N;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        long ans = 0;

        bool[] f = new bool[N + 1];
        f[N] = true;

        for (int i = 2; i <= N; i++)
        {
            long p = 0;
            bool flag = false;
            for (int j = i + i; j <= N; j += i)
            {
                p += (j / i) * 4;
                flag |= f[N];
            }

            if (flag)
            {
                ans += p;
                for (int j = i + i; j <= N; j += i)
                {
                    f[j] = true;
                }
            }
        }

        Console.WriteLine(ans);
    }


    public static void Main(string[] args) => new Program().Solve();
}

struct Edge
{
    public int To;
    public int X;

    public Edge(int t, int x)
    {
        To = t;
        X = x;
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