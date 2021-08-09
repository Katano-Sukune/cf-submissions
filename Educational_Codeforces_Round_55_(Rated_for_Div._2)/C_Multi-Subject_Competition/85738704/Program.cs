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
        int m = sc.NextInt();

        int[] s = new int[n];
        int[] r = new int[n];
        for (int i = 0; i < n; i++)
        {
            s[i] = sc.NextInt();
            r[i] = sc.NextInt();
        }

        var ls = new List<int>[m];
        for (int i = 0; i < m; i++)
        {
            ls[i] = new List<int>();
        }

        for (int i = 0; i < n; i++)
        {
            ls[s[i] - 1].Add(r[i]);
        }

        long[] ans = new long[100001];
        for (int i = 0; i < m; i++)
        {
            ls[i].Sort((x, y) => y.CompareTo(x));
            long sum = 0;
            for (int j = 0; j < ls[i].Count; j++)
            {
                sum += ls[i][j];
                if (sum > 0)
                {
                    ans[j + 1] += sum;
                }
                else
                {
                    break;
                }
            }
        }

        Console.WriteLine(ans.Max());
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