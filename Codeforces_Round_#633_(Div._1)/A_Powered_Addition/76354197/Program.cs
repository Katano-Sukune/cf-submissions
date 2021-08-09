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
            int[] a = sc.IntArray();
            sb.AppendLine(Q(n, a));
        }

        Console.Write(sb);
    }

    string Q(int n, int[] a)
    {
        int max = 0;
        int min = int.MaxValue;
        for (int i = n - 1; i >= 0; i--)
        {
            if (min < a[i])
            {
                max = Math.Max(max, a[i] - min);
            }

            min = Math.Min(min, a[i]);
        }

        for (int i = 30; i >= 0; i--)
        {
            if ((max & (1 << i)) > 0)
            {
                return (i + 1).ToString();
            }
        }

        return "0";
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