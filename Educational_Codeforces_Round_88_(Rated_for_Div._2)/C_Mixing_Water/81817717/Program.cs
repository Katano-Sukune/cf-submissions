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
            sb.AppendLine(Q(sc.NextLong(), sc.NextLong(), sc.NextLong()));
        }

        Console.Write(sb);
    }

    string Q(long h, long c, long t)
    {
        long ans = 2;
        decimal diff = Math.Abs(t - (decimal)(h + c) / 2);

        // 両方混ぜる i回
        long high = 0;
        long low = int.MaxValue;

        while (low - high > 1)
        {
            long mid = (high + low) / 2;
            // mid回のときの温度

            decimal tmp = (decimal)(h * (mid + 1) + c * mid) / (2 * mid + 1);

            if (tmp > t) high = mid;
            else low = mid;
        }

        decimal hh = (decimal)(h * (high + 1) + c * high) / (2 * high + 1);
        if (Math.Abs(t - hh) < diff || (Math.Abs(t - hh) <= diff && high * 2 + 1 < ans))
        {
            diff = Math.Abs(t - hh);
            ans = high * 2 + 1;
        }
        decimal ll = (decimal)(h * (low + 1) + c * low) / (2 * low + 1);

        // Console.WriteLine($"{high} {low} {hh} {ll}");
        if (Math.Abs(t - ll) < diff || (Math.Abs(t - ll) <= diff && low * 2 + 1 < ans))
        {
            diff = Math.Abs(t - ll);
            ans = low * 2 + 1;
        }

        return ans.ToString();
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
