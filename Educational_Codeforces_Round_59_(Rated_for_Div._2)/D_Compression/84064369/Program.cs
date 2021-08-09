using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
// N*N行列　値は 0,1

        int n = sc.NextInt();
        List<int> d = new List<int>();
        for (int i = 1; i * i <= n; i++)
        {
            if (n % i == 0)
            {
                d.Add(i);
                if (i != n / i) d.Add(n / i);
            }
        }

        d.Sort((l, r) => r.CompareTo(l));

        ulong[][] rr = new ulong[n][];
        for (int i = 0; i < n; i++)
        {
            string s = sc.Next();
            rr[i] = new ulong[(s.Length + 15) / 16];
            int dd = s.Length / 16;
            int mm = s.Length % 16;
            if (mm != 0)
            {
                ulong t = 0;
                for (int j = 0; j < mm; j++)
                {
                    t *= 16;
                    t += ToInt(s[j]);
                }

                rr[i][dd] = t;
            }

            for (int j = 0; j < dd; j++)
            {
                ulong t = 0;
                for (int k = 0; k < 16; k++)
                {
                    t *= 16;
                    t += ToInt(s[j * 16 + k + mm]);
                }

                rr[i][dd - 1 - j] = t;
            }
        }

        ulong[][] cc = new ulong[n][];
        for (int i = 0; i < n; i++)
        {
            cc[i] = new ulong[(n + 63) / 64];
            int mm = n % 64;
            int dd = n / 64;
            if (mm != 0)
            {
                ulong t = 0;
                for (int j = mm - 1; j >= 0; j--)
                {
                    t *= 2;
                    bool b = (rr[dd * 64 + j][i / 64] & (1UL << (i % 64))) > 0;
                    if (b) t++;
                }

                cc[i][dd] = t;
            }

            for (int j = dd - 1; j >= 0; j--)
            {
                ulong t = 0;
                for (int k = 64 - 1; k >= 0; k--)
                {
                    t *= 2;
                    bool b = (rr[j * 64 + k][i / 64] & (1UL << (i % 64))) > 0;
                    if (b) t++;
                }

                cc[i][j] = t;
            }
        }


        foreach (int i in d)
        {
            bool flag = true;
            for (int j = 0; j < n && flag; j += i)
            {
                for (int k = 1; k < i && flag; k++)
                {
                    for (int l = 0; l < rr[j].Length && flag; l++)
                    {
                        flag &= rr[j][l] == rr[j + k][l] && cc[j][l] == cc[j + k][l];
                    }
                }
            }

            if (flag)
            {
                Console.WriteLine(i);
                return;
            }
        }
    }

    uint ToInt(char c)
    {
        return (uint) ('0' <= c && c <= '9' ? (c - '0') : (c - 'A' + 10));
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
                string s = Console.ReadLine();
                while (s.Length == 0)
                {
                    s = Console.ReadLine();
                }

                _line = s.Split(Separator);
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