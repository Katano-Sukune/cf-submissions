using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int q = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < q; i++)
        {
            int n = sc.NextInt();
            int m = sc.NextInt();
            var t = new int[n];
            var l = new int[n];
            var h = new int[n];
            for (int j = 0; j < n; j++)
            {
                t[j] = sc.NextInt();
                l[j] = sc.NextInt();
                h[j] = sc.NextInt();
            }

            sb.AppendLine(Q(n, m, t, l, h));
        }
        Console.Write(sb);
    }

    string Q(int n, int m, int[] t, int[] l, int[] h)
    {
        // 最初m度

        // 温度を1分で+-1度まで変えられる

        // t_i分にくる客は 温度が l_i <= t <= h_iなら満足する

        // 全員満足させられるか?

        // 時間iに可能な温度
        int min = m;
        int max = m;
        int time = 0;

        var sortedByT = new int[n];
        for (int i = 0; i < n; i++)
        {
            sortedByT[i] = i;
        }

        Array.Sort(sortedByT, (x, y) => t[x].CompareTo(t[y]));

        foreach (int i in sortedByT)
        {
            int d = t[i] - time;
            int nMin = min - d;
            int nMax = max + d;

            if (h[i] < nMin || nMax < l[i])
            {
                // Console.WriteLine($"aaaa {i} {l[i]} {h[i]}");
                return "NO";
            }

            min = Math.Max(l[i], nMin);
            max = Math.Min(h[i], nMax);

            time = t[i];
        }

        return "YES";
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
