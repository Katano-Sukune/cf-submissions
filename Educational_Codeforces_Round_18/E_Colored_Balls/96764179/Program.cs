using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private long[] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.LongArray();

        /*
         * 箱iには色iのボール a_i個
         *
         * 分ける
         *
         * 各グループには 1色のみ
         * 個数最大 - 最小 <= 1
         *
         * グループの個数 最小
         */


        long ans = long.MaxValue;
        for (long i = 1; i * i <= A[0]; i++)
        {
            ans = Math.Min(ans, Calc(i));
            ans = Math.Min(ans, Calc(A[0] / i));
            if (A[0] % i == 0)
            {
                ans = Math.Min(ans, Calc(i - 1));
                ans = Math.Min(ans, Calc(A[0] / i - 1));
            }
        }

        Console.WriteLine(ans);
    }

    long Calc(long min)
    {
        checked
        {
            if (min <= 0) return long.MaxValue;
            long res = 0;
            foreach (long i in A)
            {
                long d = (i + min) / (min + 1);
                if (i % d != 0)
                {
                    if (i / d != min) return long.MaxValue;
                }
                else
                {
                    if (min != i / d && min + 1 != i / d) return long.MaxValue;
                }

                res += (i + min) / (min + 1);
            }

            // Console.WriteLine($"{min} {res}");
            return res;
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