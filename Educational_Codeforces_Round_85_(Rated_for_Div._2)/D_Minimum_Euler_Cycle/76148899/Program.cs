using System;
using System.Collections.Generic;
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
            sb.AppendLine(Q(sc.NextInt(), sc.NextLong(), sc.NextLong()));
        }

        Console.Write(sb.ToString());
    }

    string Q(int n, long l, long r)
    {
        // 1,2,1.... 1,n-1,1,n,2,3,2....2,n-1,2,n,3,4,3.... n-1,n,1

        List<int> ls = new List<int>();
        long cnt = 0;
        long ll = -1;
        for (int i = 1; i < n; i++)
        {
            long t = 2 * (n - i);

            if (l <= cnt + t)
            {
                if (ll == -1) ll = cnt;
                for (int j = i + 1; j <= n; j++)
                {
                    ls.Add(i);
                    ls.Add(j);
                }

                if (cnt > r) break;
            }

            cnt += t;
        }

        if (r == (long) n * (n - 1) + 1) ls.Add(1);
        if (ll == -1) ll = (long) n * (n - 1);
        int[] ans = new int[r - l + 1];
        for (int i = 0; i <= r - l; i++)
        {
            ans[i] = ls[i + (int) (l - ll) - 1];
        }

        return string.Join(" ", ans);
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