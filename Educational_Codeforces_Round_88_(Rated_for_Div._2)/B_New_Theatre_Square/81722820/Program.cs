using System;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < t; i++)
        {
            int n = sc.NextInt();
            int m = sc.NextInt();
            int x = sc.NextInt();
            int y = sc.NextInt();
            string[] s = new string[n];
            for (int j = 0; j < n; j++)
            {
                s[j] = sc.Next();
            }
            sb.AppendLine(Q(n, m, x, y, s));
        }
        Console.Write(sb);
    }

    string Q(int n, int m, int x, int y, string[] s)
    {
        // コストx 1*1 or コストy 1*2 (横長)

        long ans = 0;

        foreach (string t in s)
        {
            bool f = false;
            int cnt = 0;
            foreach (char c in t)
            {
                if (f)
                {
                    if (c == '*')
                    {
                        ans += Math.Min(x * cnt, y * (cnt / 2) + x * (cnt % 2));
                        f = false;
                    }
                    else
                    {
                        cnt++;
                    }
                }
                else
                {
                    if (c == '.')
                    {
                        f = true;
                        cnt = 1;
                    }
                }
            }
            if (f) ans += Math.Min(x * cnt, y * (cnt / 2) + x * (cnt % 2));
        }

        return ans.ToString();
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