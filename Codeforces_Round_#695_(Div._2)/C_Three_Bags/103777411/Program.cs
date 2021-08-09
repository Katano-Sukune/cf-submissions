using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{
    int[] N;
    long[][] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.IntArray();
        A = new long[3][];
        for (int i = 0; i < 3; i++)
        {
            A[i] = sc.LongArray();
        }

        var ls = new List<long>();
        for (int i = 0; i < 3; i++)
        {
            ls.AddRange(A[i]);
        }


        ls.Sort();

        long sum = 0;
        foreach (var i in ls)
        {
            sum += i;
        }

        long[] s = new long[3];
        long[] mn = new long[3];
        for (int i = 0; i < 3; i++)
        {
            mn[i] = long.MaxValue;
            foreach (var j in A[i])
            {
                mn[i] = Math.Min(mn[i], j);
                s[i] += j;
            }
        }

        long ans = long.MinValue;
        for (int i = 0; i < 3; i++)
        {
            for (int j = i + 1; j < 3; j++)
            {
                ans = Math.Max(ans, sum - 2 * (mn[i] + mn[j]));
            }
            ans = Math.Max(ans, sum - 2 * s[i]);
        }

        Console.WriteLine(ans);
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
