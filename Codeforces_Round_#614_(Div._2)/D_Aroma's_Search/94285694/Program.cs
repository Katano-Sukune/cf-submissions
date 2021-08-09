using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using CompLib.Util;
using System.Threading;

public class Program
{
    private long X0, Y0, AX, AY, BX, BY;
    private long XS, YS, T;

    public void Solve()
    {
        checked
        {
            var sc = new Scanner();
            X0 = sc.NextLong();
            Y0 = sc.NextLong();
            AX = sc.NextLong();
            AY = sc.NextLong();
            BX = sc.NextLong();
            BY = sc.NextLong();
            XS = sc.NextLong();
            YS = sc.NextLong();
            T = sc.NextLong();

            /*
             * ノード
             * 0の位置 (x0,y0)
             *
             * i > 0
             *
             * (ax * x_{i-1} + bx, ay * y_{i-1} + by)
             *
             * (xs, ys)にいる
             * t秒でできるだけnode集める
             * 
             */

            // 最初に取りに行くノード
            var x = new List<BigInteger>();
            var y = new List<BigInteger>();
            x.Add(X0);
            y.Add(Y0);
            while (true)
            {
                BigInteger nx = AX * x[x.Count - 1] + BX;
                BigInteger ny = AY * y[y.Count - 1] + BY;

                if (XS < nx && YS < ny && (nx - XS) + (ny - YS) > T)
                {
                    break;
                }

                x.Add(nx);
                y.Add(ny);
            }

            int ans = 0;
            for (int f = 0; f < x.Count; f++)
            {
                BigInteger dist = BigInteger.Abs(XS - x[f]) + BigInteger.Abs(YS - y[f]);
                if (dist > T) continue;

                for (int e = f; e >= 0; e--)
                {
                    BigInteger dist2 = BigInteger.Abs(x[f] - x[e]) + BigInteger.Abs(y[f] - y[e]);
                    if (dist + dist2 > T) break;
                    ans = Math.Max(ans, f - e + 1);
                }

                for (int e = f + 1; e < x.Count; e++)
                {
                    BigInteger dist2 = BigInteger.Abs(x[f] - x[e]) + BigInteger.Abs(y[f] - y[e]);
                    if (dist + dist2 > T) break;
                    ans = Math.Max(ans, e - f + 1);
                }
            }

            Console.WriteLine(ans);
        }
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