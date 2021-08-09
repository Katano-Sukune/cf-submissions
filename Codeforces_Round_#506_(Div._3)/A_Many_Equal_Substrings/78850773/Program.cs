using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int k = sc.NextInt();
        string t = sc.Next();

        // sの部分文字列にk個tがある
        // 最小のs
        int dd = -1;
        for (int d = 1; d <= n; d++)
        {
            bool f = true;
            // d文字ずらす
            for (int i = 0; i < n - d; i++)
            {
                f &= t[i] == t[i + d];
            }

            if (f)
            {
                dd = d;
                break;
            }
        }

        var ans = new List<char>();
        // t + dd * (k-1)
        for (int i = 0; i < n; i++)
        {
            ans.Add(t[i]);
        }
        for (int i = 0; i < k - 1; i++)
        {
            for(int j = n -dd; j < n; j++)
            {
                ans.Add(t[j]);
            }
        }
        Console.WriteLine(new string(ans.ToArray()));
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
