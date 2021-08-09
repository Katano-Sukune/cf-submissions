using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private long N, K;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextLong();
        K = sc.NextLong();

        /*
         * f(x) = x/2 if x is even
         *      = x-1 otherwise
         *
         *
         * y 偶数 上位桁 y, y+1に一致
         *
         *   奇数 yに一致
         */

        // 最大候補

        // 偶数になるまで (N/(2^i),(N/2^i)-1)

        List<long> x = new List<long>();

        long tmpX = N;
        while (tmpX > 0)
        {
            if (tmpX % 2 == 1)
            {
                x.Add(tmpX);
                if (tmpX - 1 > 0)
                    x.Add(tmpX - 1);
                tmpX /= 2;
            }
            else
            {
                x.Add(tmpX);
                if (tmpX - 2 > 0)
                    x.Add(tmpX - 2);
                tmpX /= 2;
                if (tmpX % 2 == 1 && tmpX > 1) tmpX--;
            }
        }

        foreach (long l in x)
        {
            if (Calc(l) >= K)
            {
                Console.WriteLine(l);
                return;
            }
        }
    }

    long Calc(long x)
    {
        long ans = 0;
        long tmpX = x;
        long p = 1;
        while (tmpX <= N)
        {
            ans += Math.Min(p, N - tmpX + 1);
            p *= 2;
            tmpX *= 2;
        }

        if (x % 2 == 0)
        {
            tmpX = x + 1;
            p = 1;
            while (tmpX <= N)
            {
                ans += Math.Min(p, N - tmpX + 1);
                p *= 2;
                tmpX *= 2;
            }
        }

        return ans;
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