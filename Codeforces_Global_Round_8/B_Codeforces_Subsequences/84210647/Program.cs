using System;
using System.Collections.Generic;
using System.Text;
using CompLib.Util;

public class Program
{
    private long K;

    public void Solve()
    {
        var sc = new Scanner();
        K = sc.NextLong();

        long[] cnt = new long[10];
        for (int i = 0; i < 10; i++) cnt[i] = 1;
        for (int j = 0;; j++)
        {
            long s = 1;
            for (int i = 0; i < 10; i++)
            {
                s *= cnt[i];
            }

            if (s >= K) break;
            cnt[j % 10]++;
        }

        string t = "codeforces";
        StringBuilder ans = new StringBuilder();
        for (int i = 0; i < 10; i++)
        {
            ans.Append(t[i], (int)cnt[i]);
        }
        
        Console.WriteLine(ans);
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