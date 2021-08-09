using CompLib.Util;
using System;
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
            sb.AppendLine(Q(sc.NextInt()));
        }
        Console.Write(sb);
    }

    string Q(int n)
    {

        // 二等辺三角形 
        // 角度 
        double r = Math.PI / n;

        // 対角線
        // sin r/2 = 0.5/h
        double h = 0.5 / Math.Sin(r / 2);

        // 短い方
        double rx = r * (n / 2);
        // sin (rx/2) = (x/2)/h

        double x = 2 * Math.Sin(rx / 2) * h;

        // 長い方
        double ry = Math.PI - rx;
        double y = 2 * Math.Sin(ry / 2) * h;
        // Console.WriteLine($"{n} {x} {y}");
        return $"{(x + y)/Math.Sqrt(2)}";
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
