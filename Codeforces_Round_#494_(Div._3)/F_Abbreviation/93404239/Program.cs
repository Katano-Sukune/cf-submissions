using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.Collections.Generic;

public class Program
{
    int N;
    string[] S;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        S = sc.Array();


        int ans = N - 1;
        for (int i = 0; i < N; i++)
        {
            ans += S[i].Length;
        }

        for (int b = 0; b < N; b++)
        {
            for (int e = b + 1; e <= N; e++)
            {
                int l = e - b;
                int len = 0;
                for (int i = 0; i < b; i++)
                {
                    len += S[i].Length;
                    len++;
                }
                len += l;

                bool f2 = false;
                for (int b2 = e; b2 < N;)
                {
                    len++;
                    if (b2 + l <= N)
                    {
                        bool f = true;
                        for (int i = 0; i < l && f; i++)
                        {
                            f &= S[b + i] == S[b2 + i];
                        }
                        if (f)
                        {
                            f2 = true;
                            len += l;
                            b2 += l;
                            continue;
                        }
                    }
                    len += S[b2].Length;
                    b2++;
                }

                if (f2) ans = Math.Min(ans, len);
            }
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
