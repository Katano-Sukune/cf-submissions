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
            sb.AppendLine(Q(sc.NextInt(), sc.Next()));
        }

        Console.Write(sb.ToString());
    }

    private string Q(int n, string s)
    {
        // ロボット
        /*
         * lrud
         * 
         * 結果が変わらないように [l,r]を削除
         * 最短
         */

        int r = 1000000;
        int l = 1;
        bool f = false;
        var map = new Dictionary<long, int>();

        int y = 0;
        int x = 0;

        map[0] = 0;

        for (int i = 0; i < n; i++)
        {
            switch (s[i])
            {
                case 'U':
                    y++;
                    break;
                case 'D':
                    y--;
                    break;
                case 'L':
                    x--;
                    break;
                case 'R':
                    x++;
                    break;
            }

            long next = y * 200001 + x;
            int o;
            if (map.TryGetValue(next, out o))
            {
                int len = i - o + 1;
                if (r - l + 1 > len)
                {
                    l = o + 1;
                    r = i + 1;
                    f = true;
                }
            }

            map[next] = i + 1;
        }

        return f ? $"{l} {r}" : "-1";
    }

    public static void Main(string[] args) => new Program().Solve();
}

struct S
{
    public int Y, X;
    public S(int y, int x)
    {
        Y = y;
        X = x;
    }
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