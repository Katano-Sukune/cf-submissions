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
        int[] p = sc.IntArray();
        string s = sc.Next();

        /*
         * i番目の駒
         * 強さp_i
         *
         * 接頭辞 or 接尾辞を反転
         * Bの和最大
         */

        long[] sumA = new long[n + 1];
        long[] sumB = new long[n + 1];
        for (int i = 0; i < n; i++)
        {
            sumA[i + 1] = sumA[i];
            sumB[i + 1] = sumB[i];

            if (s[i] == 'A') sumA[i + 1] += p[i];
            else sumB[i + 1] += p[i];
        }

        long ans = long.MinValue;
        for (int l = 0; l <= n; l++)
        {
            long tmp = sumA[l] + (sumB[n] - sumB[l]);
            ans = Math.Max(ans, tmp);

            long tmp2 = sumB[l] + (sumA[n] - sumA[l]);
            ans = Math.Max(ans, tmp2);
        }

        Console.WriteLine(ans);
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