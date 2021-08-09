using System;
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
            var a = new long[n];
            var b = new long[n];
            for (int j = 0; j < n; j++)
            {
                a[j] = sc.NextLong();
                b[j] = sc.NextLong();
            }

            sb.AppendLine(Q(n, a, b));
        }

        Console.Write(sb.ToString());
    }

    string Q(int n, long[] a, long[] b)
    {
        // 最初にiを倒す -> b[i-1]ダメージを与えられない
        long sum = 0;
        long min = long.MaxValue;
        for (int i = 0; i < n; i++)
        {
            // iを普通に倒す
            // aターン
            // 前を爆破してから倒す
            long t = Math.Max(0, a[i] - b[(i + n - 1) % n]);
            sum += t;
            min = Math.Min(min, a[i] - t);
        }

        return (sum + min).ToString();
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