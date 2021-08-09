using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int m = sc.NextInt();
        int[] a = sc.IntArray();
        int[] c = sc.IntArray();

        int[] sorted = new int[n];
        for (int i = 0; i < n; i++)
        {
            sorted[i] = i;
        }

        Array.Sort(sorted, (l, r) => c[l] != c[r] ? c[l].CompareTo(c[r]) : l.CompareTo(r));

        int ptr = 0;
        long[] ans = new long[m];
        for (int i = 0; i < m; i++)
        {
            int t = sc.NextInt() - 1;
            int d = sc.NextInt();

            int tmp1 = Math.Min(d, a[t]);
            d -= tmp1;
            a[t] -= tmp1;
            ans[i] += (long) tmp1 * c[t];

            while (ptr < n && d > 0)
            {
                int tmp2 = Math.Min(d, a[sorted[ptr]]);
                d -= tmp2;
                a[sorted[ptr]] -= tmp2;
                ans[i] += (long)c[sorted[ptr]] * tmp2;
                if (a[sorted[ptr]] == 0) ptr++;
            }

            if (d > 0)
            {
                ans[i] = 0;
                break;
            }
        }


        Console.WriteLine(string.Join("\n", ans));
        /*
         * 種類i
         * コストc_i
         * a_i個
         *
         * m人
         * t_jをd_j個注文
         * 足りないとき
         * 一番安いやつ
         *
         * 
         * 
         */
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