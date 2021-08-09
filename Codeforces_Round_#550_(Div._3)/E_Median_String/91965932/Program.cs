using CompLib.Util;
using System;
using System.Collections.Generic;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int k = sc.NextInt();
        string s = sc.Next();
        string t = sc.Next();

        var c = new List<int>();

        // 繰り上がり
        bool f = false;
        for (int i = 0; i < k; i++)
        {
            int d = s[k - i - 1] + t[k - i - 1] - 2 * 'a';
            if (f) d++;
            f = false;
            if (d >= 26)
            {
                f = true;
                d -= 26;
            }
            c.Add(d);
        }
        if (f)
        {
            c[k - 1] += 26;
        }

        // 繰り下がり
        f = false;
        var ans = new List<char>();
        for (int i = 0; i < c.Count; i++)
        {
            int d = c[k - i - 1];
            if (f) d += 26;
            f = false;
            int e = d / 2;

            ans.Add((char)(e + 'a'));
            f = d % 2 == 1;
        }

        Console.WriteLine(string.Join("", ans));
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
