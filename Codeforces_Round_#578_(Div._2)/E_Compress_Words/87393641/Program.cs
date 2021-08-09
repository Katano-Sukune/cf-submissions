using System;
using System.Collections.Generic;
using CompLib.Util;

public class Program
{
    private int N;
    private string[] S;
    private const long Mod1 = (int) 1e9 + 7;
    private const long Mod2 = (119 << 23) + 1;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        S = sc.Array();
        var ls = new List<char>();
        foreach (string s in S)
        {
            int t = Math.Min(s.Length, ls.Count);
            long[] f1 = new long[t + 1];
            long[] f2 = new long[t + 1];
            long d1 = 1;
            long d2 = 1;
            for (int i = 0; i < t; i++)
            {
                f1[i + 1] = (f1[i] + d1 * ls[ls.Count - 1 - i]) % Mod1;
                f2[i + 1] = (f2[i] + d2 * ls[ls.Count - 1 - i]) % Mod2;

                d1 = (d1 * 256) % Mod1;
                d2 = (d2 * 256) % Mod2;
            }

            long[] b1 = new long[t + 1];
            long[] b2 = new long[t + 1];
            for (int i = 0; i < t; i++)
            {
                b1[i + 1] = (b1[i] * 256 + s[i]) % Mod1;
                b2[i + 1] = (b2[i] * 256 + s[i]) % Mod2;
            }

            int index = -1;
            for (int i = t; i >= 0; i--)
            {
                if (f1[i] == b1[i] && f2[i] == b2[i])
                {
                    index = i;
                    break;
                }
            }

            for (int i = index; i < s.Length; i++)
            {
                ls.Add(s[i]);
            }
        }

        Console.WriteLine(new string(ls.ToArray()));
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