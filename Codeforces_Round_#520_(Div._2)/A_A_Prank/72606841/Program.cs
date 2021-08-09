using System;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int[] a = sc.IntArray();

        // 差が1で連続するとこ

        int l = 0;
        int cnt = 1;
        int ans = 0;
        for (int i = 1; i < n; i++)
        {
            if (a[i - 1] + 1 == a[i])
            {
                cnt++;
            }
            else
            {
                if (a[l] == 1)
                {
                    ans = Math.Max(ans, cnt - 1);
                }
                else
                {
                    ans = Math.Max(ans, cnt - 2);
                }

                l = i;
                cnt = 1;
            }
        }

        if (a[n - 1] == 1000 || a[l] == 1)
        {
            ans = Math.Max(ans, cnt - 1);
        }
        else
        {
            ans = Math.Max(ans, cnt - 2);
        }

        Console.WriteLine(ans);
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