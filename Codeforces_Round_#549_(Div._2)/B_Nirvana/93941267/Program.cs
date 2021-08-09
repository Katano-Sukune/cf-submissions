using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        var n = sc.Next();
        long ans = 0;
        long tmp = 1;
        for (int i = 0; i < n.Length; i++)
        {
            if (n[i] == '0')
            {
                tmp = 0;
                break;
            }

            if (i == 0 && n[i] == '1')
            {
                long tmp2 = 1;
                for (int j = 1; j < n.Length; j++)
                {
                    tmp2 *= 9;
                }

                ans = Math.Max(ans, tmp2);
            }
            else
            {
                long tmp2 = tmp * (n[i] - '1');
                for (int j = i + 1; j < n.Length; j++)
                {
                    tmp2 *= 9;
                }

                ans = Math.Max(ans, tmp2);
            }

            tmp *= (n[i] - '0');
        }

        ans = Math.Max(ans, tmp);
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