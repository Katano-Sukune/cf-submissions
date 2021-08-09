using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int[] a = sc.IntArray();

        var negative = new List<int>();
        var zero = new List<int>();
        for (int i = 0; i < n; i++)
        {
            if (a[i] == 0) zero.Add(i);
            else if (a[i] < 0) negative.Add(i);
        }

        // 0
        // まとめて1個にしてから消す

        // 負が奇数個
        // 絶対値が最小のやつを消す

        // のこりてきとう

        var sb = new StringBuilder();
        bool[] rm = new bool[n];

        var ls = new List<int>();
        ls.AddRange(zero);
        if (negative.Count % 2 != 0)
        {
            int max = int.MinValue;
            int idx = -1;
            foreach (int i in negative)
            {
                if (max < a[i])
                {
                    max = a[i];
                    idx = i;
                }
            }

            ls.Add(idx);
        }

        if (ls.Count > 0 && ls.Count < n)
        {
            for (int t = 1; t < ls.Count; t++)
            {
                sb.AppendLine($"1 {ls[t] + 1} {ls[0] + 1}");
            }

            sb.AppendLine($"2 {ls[0] + 1}");
            foreach (int i in ls)
            {
                rm[i] = true;
            }
        }

        int j = -1;
        for (int i = 0; i < n; i++)
        {
            if (rm[i]) continue;
            if (j == -1)
            {
                j = i;
                continue;
            }

            sb.AppendLine($"1 {i + 1} {j + 1}");
        }

        Console.Write(sb);
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