using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    long M;
    long[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        A = sc.LongArray();

        /*
         * Aからいくつか選んで Mod Mが最大になるようにする
         */

        var left = Calc(0, N / 2);
        var right = Calc(N / 2, N);
        long ans = ((left.Last() + right.Last()) % M);
        foreach (int i in left)
        {
            // i + right[j] がM未満最大
            // 
            int ok = 0;
            int ng = right.Count;
            while(ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                if (i + right[mid] < M) ok = mid;
                else ng = mid;
            }
            ans = Math.Max(ans, i + right[ok]);
        }

        Console.WriteLine(ans);
    }

    List<long> Calc(int b, int e)
    {
        var ls = new List<long>();
        ls.Add(0);

        for (int i = b; i < e; i++)
        {
            int len = ls.Count;
            for (int j = 0; j < len; j++)
            {
                ls.Add((ls[j] + A[i]) % M);
            }
        }

        var result = new List<long>();
        ls.Sort();
        for (int i = 0; i < ls.Count; i++)
        {
            if (i == 0 || ls[i - 1] != ls[i])
            {
                result.Add(ls[i]);
            }
        }

        return result;
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
