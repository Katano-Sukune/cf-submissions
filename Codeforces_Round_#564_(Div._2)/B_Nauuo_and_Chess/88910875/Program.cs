using System;
using System.Text;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        /*
         * m*mチェス盤にn個駒を置く
         *
         * 駒iは (r_i, c_i)にある
         *
         * |r_i - r_j| + |c_i - c_j| >= |i-j|を満たすように置く
         *
         * 最小のmと置き方
         */

        // (1,1) (1,2) (2,2) (2,3)

        var m = 0;
        var sb = new StringBuilder();
        for (int i = 0; i < n; i++)
        {
            int r = (i + 2) / 2;
            int c = (i + 3) / 2;
            m = Math.Max(r, c);
            sb.AppendLine($"{r} {c}");
        }

        Console.WriteLine(m);
        Console.Write(sb);
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