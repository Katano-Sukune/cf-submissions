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
            sb.AppendLine(Q(sc.NextLong()));
        }
        Console.Write(sb);
    }

    string Q(long n)
    {
        // i桁最大
        long[] dp1 = new long[61];

        // n 下i桁以下
        long[] dp2 = new long[61];
        dp1[0] = 0;
        dp2[0] = 0;
        for (int i = 1; i <= 60; i++)
        {
            // 桁が変わる +i
            // 今まで dp1[i-1]*2
            dp1[i] = dp1[i - 1] * 2 + i;

            if (n % 2 == 1)
            {
                dp2[i] = dp1[i - 1] + dp2[i - 1] + i;
            }
            else
            {
                dp2[i] = dp2[i - 1];
            }

            n /= 2;
        }

        return dp2[60].ToString();
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
