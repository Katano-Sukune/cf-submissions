using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private long X, Y, L, R;

    public void Solve()
    {
        checked
        {
            var sc = new Scanner();
            X = sc.NextLong();
            Y = sc.NextLong();
            L = sc.NextLong();
            R = sc.NextLong();

            /*
             * n = xa + yb
             * n年は不幸
             *
             * [l,r]内の不幸じゃない最大の区間
             */

            // 可能
            // 2 3
            // 5 6
            // 5 6 10 11 12 15 16 17 18 20 
            // 10 11 12 13 14 15 

            var hs = new HashSet<long>();
            long xa = 1;
            for (int a = 0; xa <= R; a++)
            {
                long yb = 1;
                for (int b = 0; xa + yb <= R; b++)
                {
                    if (xa + yb >= L) hs.Add(xa + yb);
                    if ((double) yb * Y > R - xa) break;
                    yb *= Y;
                }

                if ((double) xa * X > R) break;
                xa *= X;
            }

            hs.Add(L - 1);
            hs.Add(R + 1);

            var ar = hs.ToArray();
            Array.Sort(ar);

            long ans = 0;
            for (int i = 1; i < ar.Length; i++)
            {
                ans = Math.Max(ans, ar[i] - ar[i - 1] - 1);
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