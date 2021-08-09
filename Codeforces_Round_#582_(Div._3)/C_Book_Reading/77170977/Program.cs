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
            sb.AppendLine(Q(sc.NextLong(), sc.NextLong()));
        }
        Console.Write(sb);
    }

    string Q(long n, long m)
    {
        long cnt = n / m;
        // m*1~m*cnt

        // (m*cnt*(cnt+1)/2)%10

        // 最初10回
        var c = new long[10];
        long sum = 0;
        for (int i = 0; i < 10; i++)
        {
            c[i] = (m * i) % 10;
            sum += c[i];
        }
        long ans = sum * (cnt / 10);
        var mm = cnt % 10;
        for (int i = 0; i <= mm; i++)
        {
            ans += c[i];
        }
        // Console.WriteLine($"{m}\n{string.Join(" ",c)}\n{sum}");
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
