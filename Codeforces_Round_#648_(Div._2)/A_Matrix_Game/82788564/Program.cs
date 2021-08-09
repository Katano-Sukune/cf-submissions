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
            var a = new int[n][];
            for (int j = 0; j < n; j++)
            {
                a[j] = sc.IntArray();
            }
            sb.AppendLine(Q(n, m, a));
        }

        Console.Write(sb);
    }

    string Q(int n, int m, int[][] a)
    {
        // a[i][j] = 0かつ i行、　j列に 1が無い

        // a[i][j] を1にできる

        // できるとこ無い　負け

        // 先手 Ashish

        // 後手 Vivek

        int rr = 0;
        int cc = 0;

        for (int i = 0; i < n; i++)
        {
            bool f = true;
            for (int j = 0; j < m; j++)
            {
                f &= a[i][j] == 0;
            }

            if (f) rr++;
        }

        for (int j = 0; j < m; j++)
        {
            bool f = true;
            for (int i = 0; i < n; i++)
            {
                f &= a[i][j] == 0;
            }

            if (f) cc++;
        }

        return Math.Min(rr, cc) % 2 == 0 ? "Vivek" : "Ashish";
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
