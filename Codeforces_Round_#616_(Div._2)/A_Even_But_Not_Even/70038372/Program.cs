using CompLib.Util;
using System;
using System.Collections.Generic;
using System.Text;

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
            string s = sc.Next();

            sb.AppendLine(Q(n, s));
        }
        Console.Write(sb.ToString());
    }

    private string Q(int n, string s)
    {
        // sのいくつか消して奇数で各桁合計が偶数
        // 奇数が2つ
        // 最後、最後2つめの奇数のこすそれ以外

        bool f = false;
        bool g = false;
        int right = -1, left = -1;
        for (int i = n - 1; i >= 0; i--)
        {
            if ((s[i] - '0') % 2 == 1)
            {
                if (f)
                {
                    left = i;
                    g = true;
                    break;
                }
                else
                {
                    right = i;
                    f = true;
                }
            }
        }

        if (!g) return "-1";

        var l = new List<char>();
        for(int i = left;i<= right; i++)
        {
            l.Add(s[i]);
        }

        return new string(l.ToArray());
    }

    public static void Main(string[] args) => new Program().Solve();
}

namespace CompLib.Util
{
    using System;
    using System.Linq;
    class Scanner
    {
        private int _index;
        private string[] _line;
        const char separator = ' ';

        public Scanner()
        {
            _index = 0;
            _line = new string[0];
        }

        public string Next()
        {
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(separator);
                _index = 0;
            }
            return _line[_index++];
        }
        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            _line = Console.ReadLine().Split(separator);
            _index = _line.Length;
            return _line;
        }
        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
    }
}