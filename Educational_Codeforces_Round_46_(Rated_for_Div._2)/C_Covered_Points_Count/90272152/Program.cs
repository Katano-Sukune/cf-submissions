using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    long[] L, R;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        L = new long[N];
        R = new long[N];
        var ls = new List<long>();

        for (int i = 0; i < N; i++)
        {
            L[i] = sc.NextLong();
            R[i] = sc.NextLong() + 1;
            ls.Add(L[i]);
            ls.Add(R[i]);
        }

        ls.Sort();
        int p = 0;
        var map = new Dictionary<long, int>();
        var l2 = new List<long>();
        for (int i = 0; i < ls.Count; i++)
        {
            if (i == 0 || ls[i - 1] != ls[i])
            {
                l2.Add(ls[i]);
                map[ls[i]] = p++;
            }
        }

        long[] imos = new long[p];
        for (int i = 0; i < N; i++)
        {
            imos[map[L[i]]]++;
            imos[map[R[i]]]--;
        }

        for (int i = 0; i < p - 1; i++)
        {
            imos[i + 1] += imos[i];
        }

        long[] ans = new long[N];
        for (int i = 0; i < p - 1; i++)
        {
            if (imos[i] - 1 >= 0)
                ans[imos[i] - 1] += l2[i + 1] - l2[i];
        }

        Console.WriteLine(string.Join(" ", ans));
        // セグメントがN個
        // K 1~Nについて
        // K個のセグメントに覆われてる点の個数


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
