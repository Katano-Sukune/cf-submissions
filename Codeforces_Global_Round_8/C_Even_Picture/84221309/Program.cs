using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{
    private int N;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();

        /*
         * 灰色のセル 連結
         * 周囲4マスが灰のセル N個
         *
         * 各セル 灰セルが偶数個 連結
         */

        List<(int x, int y)> ls = new List<(int x, int y)>();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (i == 1 && j == 1) continue;
                ls.Add((i, j));
            }
        }

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (j == 1 && k == 1) continue;
                    if (i % 2 == 0)
                    {
                        if (j == 0 && k == 0) continue;
                        int x = j + 2 + 2 * i;
                        int y = k + 2;
                        ls.Add((x, y));
                    }
                    else
                    {
                        if (j == 0 && k == 2) continue;
                        int x = j + 2 + 2 * i;
                        int y = k;
                        ls.Add((x, y));
                    }
                }
            }
        }

        var sb = new StringBuilder();
        sb.AppendLine(ls.Count.ToString());
        foreach ((int x, int y) p in ls)
        {
            sb.AppendLine($"{p.x} {p.y}");
        }

        Console.Write(sb);

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